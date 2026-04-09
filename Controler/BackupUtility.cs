using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.Controler
{
    /// <summary>
    /// FASE 4 - Task 39: Automatic Database Backup Utility
    /// Provides backup/restore functionality with automatic cleanup
    /// 
    /// Features:
    /// - Full database backup to .bak file
    /// - Maintains last 30 days of backups automatically
    /// - Restore from any backup point
    /// - Logging of all operations
    /// - Integrable with UI for manual or scheduled triggers
    /// 
    /// Usage:
    ///   BackupUtility.ExecuteBackupNow()  // Manual backup
    ///   BackupUtility.RestoreFromBackup(filePath)  // Restore
    /// </summary>
    public static class BackupUtility
    {
        private static readonly string BACKUP_FOLDER = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "Backups");
        private static readonly string LOG_FILE = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "backup_log.txt");

        /// <summary>
        /// Initialize backup system (call from principal.cs Form_Load)
        /// Only runs for Admin users
        /// </summary>
        public static void InitializeBackupSchedule(int hourOfDay = 2, int minuteOfDay = 0)
        {
            try
            {
                // Only admin can access backup
                if (UserSession.Role != "Admin")
                {
                    return; // Silent exit for non-admin users
                }

                // Create backup folder if it doesn't exist
                if (!Directory.Exists(BACKUP_FOLDER))
                {
                    Directory.CreateDirectory(BACKUP_FOLDER);
                    LogMessage("Backup folder created at: " + BACKUP_FOLDER);
                }

                LogMessage("Backup system initialized. Ready for backup/restore operations.");
                LogMessage($"Note: Schedule backup at {FormatTime(hourOfDay, minuteOfDay)} via Windows Task Scheduler if automatic backups needed.");
                
                // Perform cleanup of old backups on startup
                CleanupOldBackups();
            }
            catch (Exception ex)
            {
                LogMessage($"ERROR initializing backup: {ex.Message}");
            }
        }

        /// <summary>
        /// Execute immediate backup (manual trigger from UI)
        /// </summary>
        public static bool ExecuteBackupNow()
        {
            try
            {
                LogMessage("Starting manual backup...");

                string backupPath = GenerateBackupPath();
                ExecuteSqlBackup(backupPath);

                LogMessage($"Backup completed successfully: {Path.GetFileName(backupPath)}");
                CleanupOldBackups();

                return true;
            }
            catch (Exception ex)
            {
                LogMessage($"ERROR during backup: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Execute SQL Server backup command
        /// Uses integrated security - must run as domain user with SQL permissions
        /// </summary>
        private static void ExecuteSqlBackup(string backupPath)
        {
            try
            {
                string connectionString = "Server=.;Database=master;Integrated Security=true;";
                
                // SQL command to backup database
                string sqlCommand = $@"
                    BACKUP DATABASE [dbProyecto] 
                    TO DISK = N'{backupPath}' 
                    WITH NOFORMAT, NOINIT, 
                    NAME = N'{Path.GetFileNameWithoutExtension(backupPath)}', 
                    SKIP, REWIND, NOUNLOAD, STATS = 10;
                ";

                using (System.Data.SqlClient.SqlConnection conn = 
                    new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    conn.Open();
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqlCommand, conn))
                    {
                        cmd.CommandTimeout = 3600; // 1 hour timeout
                        cmd.ExecuteNonQuery();
                    }
                }

                LogMessage($"SQL Server backup completed: {Path.GetFileName(backupPath)} ({GetFileSizeGB(backupPath)} GB)");
            }
            catch (Exception ex)
            {
                LogMessage($"ERROR executing SQL backup: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Generate timestamped backup file path
        /// Format: Backups\dbProyecto_backup_20260408_020000.bak
        /// </summary>
        private static string GenerateBackupPath()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string filename = $"dbProyecto_backup_{timestamp}.bak";
            return Path.Combine(BACKUP_FOLDER, filename);
        }

        /// <summary>
        /// Clean up backups older than 30 days
        /// Keep maximum of 30 backup files
        /// </summary>
        private static void CleanupOldBackups()
        {
            try
            {
                if (!Directory.Exists(BACKUP_FOLDER))
                    return;

                DirectoryInfo backupDir = new DirectoryInfo(BACKUP_FOLDER);
                FileInfo[] backupFiles = backupDir.GetFiles("*.bak");

                DateTime thirtyDaysAgo = DateTime.Now.AddDays(-30);

                // Delete backups older than 30 days
                foreach (FileInfo backupFile in backupFiles)
                {
                    if (backupFile.LastWriteTime < thirtyDaysAgo)
                    {
                        try
                        {
                            backupFile.Delete();
                            LogMessage($"Old backup deleted (30+ days): {backupFile.Name}");
                        }
                        catch (Exception ex)
                        {
                            LogMessage($"WARNING: Could not delete old backup {backupFile.Name}: {ex.Message}");
                        }
                    }
                }

                // Keep only last 30 backups if more exist
                if (backupFiles.Length > 30)
                {
                    var oldestBackups = backupDir.GetFiles("*.bak")
                        .OrderBy(f => f.LastWriteTime)
                        .Take(backupDir.GetFiles("*.bak").Length - 30)
                        .ToList();

                    foreach (FileInfo oldBackup in oldestBackups)
                    {
                        try
                        {
                            oldBackup.Delete();
                            LogMessage($"Excess backup deleted (>30 files): {oldBackup.Name}");
                        }
                        catch (Exception ex)
                        {
                            LogMessage($"WARNING: Could not delete excess backup {oldBackup.Name}: {ex.Message}");
                        }
                    }
                }

                var remainingBackups = backupDir.GetFiles("*.bak");
                LogMessage($"Backup cleanup completed. Files retained: {remainingBackups.Length}");
            }
            catch (Exception ex)
            {
                LogMessage($"ERROR during cleanup: {ex.Message}");
            }
        }

        /// <summary>
        /// Get list of available backups for restore
        /// Returns array of full file paths, newest first
        /// </summary>
        public static string[] GetAvailableBackups()
        {
            try
            {
                if (!Directory.Exists(BACKUP_FOLDER))
                    return new string[] { };

                DirectoryInfo backupDir = new DirectoryInfo(BACKUP_FOLDER);
                FileInfo[] backupFiles = backupDir.GetFiles("*.bak")
                    .OrderByDescending(f => f.LastWriteTime)
                    .ToArray();

                return backupFiles.Select(f => f.FullName).ToArray();
            }
            catch (Exception ex)
            {
                LogMessage($"ERROR getting backup list: {ex.Message}");
                return new string[] { };
            }
        }

        /// <summary>
        /// Get display names of backups (filename + timestamp info)
        /// </summary>
        public static string[] GetAvailableBackupNames()
        {
            try
            {
                if (!Directory.Exists(BACKUP_FOLDER))
                    return new string[] { };

                DirectoryInfo backupDir = new DirectoryInfo(BACKUP_FOLDER);
                FileInfo[] backupFiles = backupDir.GetFiles("*.bak")
                    .OrderByDescending(f => f.LastWriteTime)
                    .ToArray();

                return backupFiles.Select(f => 
                    $"{Path.GetFileNameWithoutExtension(f.Name)} ({GetFileSizeGB(f.FullName)} GB - {f.LastWriteTime:yyyy-MM-dd HH:mm})"
                ).ToArray();
            }
            catch (Exception ex)
            {
                LogMessage($"ERROR getting backup names: {ex.Message}");
                return new string[] { };
            }
        }

        /// <summary>
        /// Restore database from backup file
        /// WARNING: This will overwrite the current production database!
        /// Only available for Admin users
        /// </summary>
        public static bool RestoreFromBackup(string backupFilePath)
        {
            try
            {
                if (UserSession.Role != "Admin")
                {
                    LogMessage("ERROR: Only Admin users can restore backups");
                    return false;
                }

                if (!File.Exists(backupFilePath))
                {
                    LogMessage($"ERROR: Backup file not found: {backupFilePath}");
                    return false;
                }

                LogMessage($"RESTORE INITIATED: {Path.GetFileName(backupFilePath)}");
                LogMessage("WARNING: This will overwrite the current database!");

                string connectionString = "Server=.;Database=master;Integrated Security=true;";
                
                string sqlCommand = $@"
                    ALTER DATABASE [dbProyecto] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    RESTORE DATABASE [dbProyecto] 
                    FROM DISK = N'{backupFilePath}' 
                    WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 10;
                    ALTER DATABASE [dbProyecto] SET MULTI_USER;
                ";

                using (System.Data.SqlClient.SqlConnection conn = 
                    new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    conn.Open();
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqlCommand, conn))
                    {
                        cmd.CommandTimeout = 3600; // 1 hour timeout
                        cmd.ExecuteNonQuery();
                    }
                }

                LogMessage($"DATABASE RESTORED successfully from {Path.GetFileName(backupFilePath)}");
                return true;
            }
            catch (Exception ex)
            {
                LogMessage($"ERROR restoring database: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Get backup statistics
        /// </summary>
        public static (int TotalBackups, decimal TotalSizeGB, DateTime LatestBackup) GetBackupStats()
        {
            try
            {
                if (!Directory.Exists(BACKUP_FOLDER))
                    return (0, 0, DateTime.MinValue);

                DirectoryInfo backupDir = new DirectoryInfo(BACKUP_FOLDER);
                FileInfo[] backupFiles = backupDir.GetFiles("*.bak");

                int count = backupFiles.Length;
                long totalBytes = backupFiles.Sum(f => f.Length);
                decimal totalGB = decimal.Round((decimal)totalBytes / (1024 * 1024 * 1024), 2);
                DateTime latest = backupFiles.Length > 0 
                    ? backupFiles.OrderByDescending(f => f.LastWriteTime).First().LastWriteTime 
                    : DateTime.MinValue;

                return (count, totalGB, latest);
            }
            catch
            {
                return (0, 0, DateTime.MinValue);
            }
        }

        /// <summary>
        /// Helper: Get file size in GB
        /// </summary>
        private static decimal GetFileSizeGB(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    FileInfo fi = new FileInfo(filePath);
                    return decimal.Round((decimal)fi.Length / (1024 * 1024 * 1024), 2);
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Helper: Format time as HH:MM
        /// </summary>
        private static string FormatTime(int hour, int minute)
        {
            return $"{hour:D2}:{minute:D2}";
        }

        /// <summary>
        /// Helper: Log backup operations to file with timestamp
        /// </summary>
        private static void LogMessage(string message)
        {
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
                File.AppendAllText(LOG_FILE, logEntry + Environment.NewLine);
            }
            catch { /* ignore logging errors */ }
        }
    }
}

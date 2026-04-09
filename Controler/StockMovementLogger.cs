using System;
using System.Collections.Generic;
using Proyecto.Model;

namespace Proyecto.Controler
{
    /// <summary>
    /// Task 31: Logs all inventory movements (stock changes)
    /// Records: who, when, which product, quantity, type (IN/OUT)
    /// </summary>
    public static class StockMovementLogger
    {
        public enum MovementType
        {
            IN,      // Stock entry / purchase
            OUT,     // Stock removal / sale
            ADJUSTMENT,  // Manual adjustment
            RETURN,  // Return from customer
            DAMAGED  // Damaged/Waste
        }

        /// <summary>
        /// Log a stock movement to the StockMovements table
        /// </summary>
        public static bool Log(int productId, int quantity, MovementType type, string notes = "")
        {
            try
            {
                string username = UserSession.Username ?? "sistema";
                return LogToDatabase(productId, quantity, type.ToString(), notes, username);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "stock-error.log"),
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Error logging stock movement: {ex.Message}\n"
                );
                return false;
            }
        }

        private static bool LogToDatabase(int productId, int quantity, string movementType, string notes, string username)
        {
            try
            {
                conexionModel conexion = new conexionModel();
                
                const string query = @"
                    INSERT INTO StockMovements (ProductId, MovementDate, Quantity, Type, Notes)
                    VALUES (@productId, GETDATE(), @quantity, @type, @notes)";

                var parameters = new Dictionary<string, object>
                {
                    ["@productId"] = productId,
                    ["@quantity"] = quantity,
                    ["@type"] = movementType,
                    ["@notes"] = $"[{username}] {notes}".Trim()
                };

                return conexion.ejecutarComandoParametrizado(query, parameters) > 0;
            }
            catch
            {
                return false;
            }
        }

        // Helper methods for common movements
        public static bool LogStockEntry(int productId, int quantity, string notes = "")
        {
            return Log(productId, quantity, MovementType.IN, notes);
        }

        public static bool LogStockRemoval(int productId, int quantity, string notes = "")
        {
            return Log(productId, quantity, MovementType.OUT, notes);
        }

        public static bool LogStockAdjustment(int productId, int quantity, string notes = "")
        {
            return Log(productId, quantity, MovementType.ADJUSTMENT, notes);
        }

        public static bool LogStockReturn(int productId, int quantity, string notes = "")
        {
            return Log(productId, quantity, MovementType.RETURN, notes);
        }

        public static bool LogStockDamaged(int productId, int quantity, string notes = "")
        {
            return Log(productId, quantity, MovementType.DAMAGED, notes);
        }
    }
}

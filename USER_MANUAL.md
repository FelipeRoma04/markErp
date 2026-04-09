# MarkERP User Manual & Quick Start Guide

## Table of Contents
1. Installation & Initial Setup
2. System Overview
3. User Management
4. Human Resources Module
5. Accounting & Financial Statements
6. Sales & Customer Management
7. Inventory & Asset Management
8. Payroll Processing
9. Generating Reports
10. System Maintenance & Backup
11. Troubleshooting
12. FAQ & Support

---

## 1. Installation & Initial Setup

### System Requirements
- **Operating System:** Windows 10/11 or Windows Server 2016+
- **Processor:** Intel i5 / AMD Ryzen 5 or equivalent (2 GHz minimum)
- **RAM:** 4 GB minimum (8 GB recommended)
- **Storage:** 500 MB for application + database space
- **SQL Server:** 2019 Express/Standard or SQL Server 2022
- **Network:** Internet access for optional cloud features (if enabled)

### Installation Steps

#### Option 1: Automated Installer (Recommended)
1. Download `MarkERP_Installer_v1.0.exe`
2. Double-click the installer
3. Click "Next" through the setup wizard
4. Accept default paths or customize: `C:\Program Files\MarkERP\`
5. Choose "Configure SQL Server database" if this is first installation
6. Complete installation
7. System will create desktop shortcut automatically

#### Option 2: PowerShell Deployment (IT/Advanced Users)
```powershell
# Run PowerShell as Administrator
CD C:\MarkERP\directory
.\deploy.ps1
```

### Initial Configuration

After installation, launch MarkERP:

1. **Login Screen**
   - Username: `admin`
   - Password: `admin123`
   - Click "Iniciar Sesión" (Sign In)

2. **Change Default Password** (IMPORTANT!)
   - Click Settings ⚙️ button (admin only)
   - Recommended: Create strong password (min. 12 characters)
   - Store securely in password manager

3. **Company Configuration**
   - Go to Settings ⚙️
   - Enter Company Name
   - Enter NIT (Tax ID)
   - Upload Company Logo (optional)
   - Enter Address and Contact Information
   - Click "Guardar" (Save)

4. **Create Initial Users**
   - See Section 3: User Management

---

## 2. System Overview

### Dashboard (Main Menu)
The MarkERP dashboard is organized into 4 main sections:

#### RECURSOS HUMANOS (Human Resources)
- Departamentos - Manage organizational departments
- Empleados - Employee records and contracts
- Contratos HR - Employment agreements
- Control de Asistencia - Attendance tracking
- Nómina y Cálculos - Payroll processing

#### VENTAS Y CRM (Sales & Customers)
- Clientes - Manage customer database
- Cotizaciones - Create quotes for customers
- Facturación - Issue invoices
- Pagos - Record customer payments

#### INVENTARIO Y ACTIVOS (Inventory & Assets)
- Productos - Product/SKU management
- Activos - Fixed asset tracking
- Depreciación - Asset depreciation tracking

#### CONTABILIDAD Y AUDITORÍA (Accounting & Audit)
- Contabilidad - Journal entry posting
- Auditoría - View audit logs
- Reportes - Generate financial reports
- Configuración ⚙️ - Admin settings

### User Roles & Permissions
- **Admin** - Full system access, database backup/restore, user management
- **Editor** - Create/edit records, post transactions
- **HR** - Employees, contracts, attendance, payroll
- **Finance** - Accounting, financial statements, financial reports
- **Sales** - Customers, invoices, quotes, payments
- **Usuario** - View-only access to assigned modules

---

## 3. User Management

### Creating New Users (Admin Only)

1. From main dashboard, click **Configuración** ⚙️
2. Select "User Management"
3. Click **New User** button
4. Fill in fields:
   - Username (unique, lowercase recommended)
   - Full Name
   - Email
   - Select Role: Admin / Editor / HR / Finance / Sales / Usuario
   - Temporary Password (user must change on first login)
5. Click **Guardar** (Save)

### User Login
1. Launch MarkERP.exe
2. Enter username and password
3. Click "Iniciar Sesión"
4. Dashboard displays modules based on user role

### Password Reset (Admin)
1. Go to **Configuración** ⚙️
2. Select User from list
3. Click **Reset Password**
4. Provide temporary password
5. User must change password on next login

---

## 4. Human Resources Module

### Creating Departments
1. Click **Departamentos** button on dashboard
2. Click **New Department**
3. Enter department name (e.g., "Sales", "Engineering")
4. Optional: Add description
5. Click **Guardar**

### Managing Employees
1. Click **Empleados** from dashboard
2. Click **New Employee** or select existing to edit

**Required Information:**
- First Name (Nombre)
- Last Name (Apellido)
- ID Number (Cedula) - must be unique
- Department
- Position (Puesto)
- Hire Date (Fecha Ingreso)
- Salary (Salario)
- Benefits: EPS (Health), AFP (Pension), ARL (Workers Comp)

3. Click **Guardar**

### Creating Employment Contracts
1. Click **Contratos HR**
2. Click **New Contract**
3. Select employee
4. Enter contract dates and type:
   - Indefinido (Permanent)
   - Fijo (Fixed-term)
   - Temporal (Temporary)
   - Practicante (Internship)
5. Specify salary and terms
6. Click **Guardar**

### Tracking Attendance
1. Click **Control de Asistencia**
2. Select date and employee
3. Record: Present / Absent / Late / Half-Day / Leave
4. Click **Guardar**
5. Attendance used for payroll calculations

---

## 5. Accounting & Financial Statements

### Chart of Accounts (Plan Único de Cuentas - PUC)
MarkERP includes Colombian standard accounting structure with accounts organized by category:
- **1000-1999:** ASSETS (Circulante, Fijos)
- **2000-2999:** LIABILITIES (Cuentas por Pagar, Impuestos)
- **3000-3999:** EQUITY (Capital, Resultados)
- **4000-4999:** REVENUE (Ventas, Servicios)
- **5000-5999:** EXPENSES (Sueldos, Gastos Administrativos)

### Recording Journal Entries
1. Click **Contabilidad** from dashboard
2. Click **New Entry**
3. Fill in:
   - Entry Date
   - Description
   - Account lines (Debit/Credit)
4. Debits must equal credits
5. Click **Guardar**
6. Status changes to "Draft" - review before posting
7. Click **Post** to finalize entry

### Generating Financial Statements
1. Click **Reportes** from dashboard
2. Select report type:
   - **Estado Financiero - Balance** (Balance Sheet)
   - **Estado Financiero - Resultados** (Income Statement)
3. Select period date range
4. Click **Generate**
5. View HTML report or export to Excel

---

## 6. Sales & Customer Management

### Creating Customer Records
1. Click **Clientes** from dashboard
2. Click **New Customer**
3. Fill in:
   - Full Name / Company Name
   - Document Type (Cedula/NIT/Passport)
   - Document Number (must be unique)
   - Email & Phone
   - Address, City, Department
   - Customer Type: Minorista / Mayorista / Distribuidor
   - Credit Limit (optional)
   - Payment Terms (days)
4. Click **Guardar**

### Creating Quotes
1. Click **Cotizaciones**
2. Click **New Quote**
3. Select customer
4. Add line items with quantity and price
5. System calculates subtotal + IVA (19%)
6. Add notes if needed
7. Click **Send** to email quote
8. Status: Draft → Sent → Accepted/Rejected

### Creating Invoices
1. Click **Facturación**
2. Click **New Invoice**
3. Select customer and quote (if from quote) or manually add items
4. Verify subtotal, IVA, total
5. Set due date for payment
6. Click **Post** to finalize
7. Invoice auto-numbers sequentially
8. Can print or email invoice

### Recording Payments
1. Click **Pagos**
2. Select invoice to record payment for
3. Enter payment date and amount
4. Select payment method:
   - Efectivo (Cash)
   - Cheque (Check)
   - Transferencia (Bank Transfer)
   - Tarjeta (Credit Card)
5. Optional: Enter check/transfer reference number
6. Click **Guardar**
7. Invoice status updates to "Paid" when fully paid

---

## 7. Inventory & Asset Management

### Managing Products
1. Click **Productos** from dashboard
2. Click **New Product**
3. Enter:
   - Product Code (unique, e.g., "PROD-001")
   - Product Name
   - Category (e.g., "Electronics")
   - Purchase Price & Sale Price
   - Stock quantities
   - Minimum Stock Level (triggers alerts)
   - Unit of Measure (units, dozen, meters, etc.)
4. Click **Guardar**

### Inventory Stock Alerts
Dashboard displays orange badge with critical stock count when inventory falls below minimum levels:
- Click badge to see list of low-stock items
- Auto-report generated for reordering

### Recording Stock Movements
1. Click **Productos**
2. Select product and click **Stock Movements**
3. Record movement type:
   - ENTRADA (Receive/Restock)
   - SALIDA (Sale)
   - AJUSTE (Inventory Adjustment)
   - DEVOLUCIÓN (Return)
4. Enter quantity and optional reference
5. Click **Guardar**
6. Stock levels update automatically

### Fixed Asset Management
1. Click **Activos** from dashboard
2. Click **New Asset**
3. Enter:
   - Asset Code/Serial Number
   - Description
   - Category (Equipment, Furniture, etc.)
   - Purchase Date & Cost
   - Useful Life (years)
   - Location
   - Responsible Employee
4. System calculates monthly depreciation
5. Click **Guardar**

---

## 8. Payroll Processing

### Colombian Payroll Features
MarkERP includes Colombian-specific calculations:
- Salario Mínimo Legal Vigente (SMLV) integration
- EPS (Health Insurance) deductions
- AFP (Pension Fund) contributions
- ARL (Workers Compensation) insurance
- Fondo de Solidaridad (Solidarity Fund) - 1% above 4 SMLV
- Overtime calculations
- Bonus and commission tracking

### Processing Monthly Payroll
1. Click **Nómina y Cálculos** from dashboard
2. Click **New Payroll**
3. Select period (start/end dates)
4. For each employee, enter:
   - Base Salary
   - Bonuses/Commissions
   - Overtime hours (auto-multiplied by 1.25)
   - Deductions (if any)
5. System auto-calculates:
   - EPS (12.5%)
   - AFP (12.4%)
   - ARL (0.52%)
   - Fondo Solidaridad (1% if > 4 SMLV)
   - Net Salary
6. Review calculations and click **Approve**
7. Status changes to "Aprobada" (Approved)
8. Click **Pay** to mark as paid
9. Optional: Generate PDF payroll slips

### Generating Payroll Slips (PDF)
1. After payroll approved, click **Generate Slips**
2. System creates individual PDF for each employee
3. Includes:
   - Company header with logo
   - Employee details
   - Salary breakdown
   - Deductions
   - Net salary
4. Can be printed or emailed to employees

---

## 9. Generating Reports

### Available Reports

#### Sales Reports
1. Click **Reportes** from dashboard
2. Select "Reporte Ventas por Período"
3. Choose date range
4. Click **Generate**
5. View/export to Excel:
   - Invoice numbers
   - Customer names
   - Amounts
   - Payment status
   - Sales person

#### Payroll Reports
1. Select "Reporte Nómina del Período"
2. Choose period
3. Generate Excel report showing:
   - All employee salaries
   - Deductions breakdown
   - Net amounts
   - Useful for payroll reconciliation

#### Critical  Stock Report (PDF)
1. Select "Inventario Crítico (PDF)"
2. Generates color-coded alert report:
   - 🔴 RED: Stock at 0 (urgent reorder)
   - 🟠 ORANGE: Below minimum level
   - 🟢 GREEN: Adequate stock
3. Print or save for procurement planning

#### Financial Statements
1. Select "Estado Financiero - Balance" or "... - Resultados"
2. Choose reporting period
3. View HTML formatted report with:
   - Multi-level account structure
   - Totals and subtotals
   - Year-over-year comparison (if data available)
4. Print or export to Excel

---

## 10. System Maintenance & Backup

### Automatic Daily Backups
MarkERP automatically creates database backups:
- **Time:** 2:00 AM daily
- **Location:** `C:\Program Files\MarkERP\Backups\`
- **Retention:** Keeps last 30 days of backups
- **Naming:** `dbProyecto_backup_YYYYMMDD_HHMMSS.bak`

### Manual Backup (Admin Only)
1. Click **Configuración** ⚙️
2. Click **Database Backup**
3. Click **Backup Now**
4. Backup completes in background
5. Log entry created with timestamp

### Database Restore (Admin Only)
**WARNING: Restore overwrites current database!**

1. Click **Configuración** ⚙️
2. Click **Restore Database**
3. Select backup file from list (most recent shown first)
4. Click **Restore**
5. Confirm you want to proceed
6. System restores and restarts

### Backup Log
- Location: `C:\Program Files\MarkERP\backup_log.txt`
- Shows all backup operations with timestamps
- Useful for compliance and audit trails

---

## 11. Troubleshooting

### Common Issues

#### "Cannot connect to SQL Server"
**Solution:**
- Ensure SQL Server is running
- Check SQL Server is set to listen on default pipes
- Verify Windows Authentication enabled
- Try connecting with SQL Server Management Studio first

#### "Login failed" after password change
**Solution:**
- Verify CAPS LOCK is off
- Check for extra spaces in password
- Admin can reset password from User Management
- Restart MarkERP after password change

#### "Invoice number conflict"
**Solution:**
- System auto-increments invoice numbers sequentially
- If error occurs, contact Admin
- Admin can manually set next invoice number

#### "Database backup failed"
**Solution:**
- Check disk space (backups need 500 MB minimum free)
- Verify SQL Server backup permissions
- See `backup_log.txt` for detailed error
- Try manual backup from Settings

#### "Slow inventory search"
**Solution:**
- Clear product search filter and restart
- Avoid searching on very large result sets (>10,000 items)
- Index optimization: Admin can run maintenance task

#### "Reports not generating"
**Solution:**
- Ensure date range is valid (From < To)
- Check that data exists for selected period
- Try simpler report first (high transaction periods may be slow)
- Allow up to 2 minutes for large reports

---

## 12. FAQ & Support

### Q: Can multiple users work simultaneously?
**A:** Yes, MarkERP supports concurrent users. Each gets independent session. Data is protected by SQL Server transaction locking.

### Q: How do I export data to Excel?
**A:** Most modules have **Export** button. Reports can be exported when viewing. Use CSV export to Excel manually if needed.

### Q: What's the maximum number of employees?
**A:** System tested with 10,000+ employees. Performance may degrade significantly beyond 50,000.

### Q: Can I access MarkERP from multiple computers?
**A:** Yes, ensure all computers can connect to SQL Server instance over network. Install MarkERP on each computer that needs it.

### Q: How do I change the company name after setup?
**A:** Click Settings ⚙️, modify Company Name, click Save. Changes immediately applied to all future invoices/reports.

### Q: Is there a mobile app?
**A:** No native mobile app currently. Web access available via future version.

### Q: How do I handle currency exchange rates?
**A:** Currently configured for Colombian Pesos (COP). Multi-currency support planned for future release.

### Q: What's included in backups?
**A:** All database data (employees, customers, invoices, financial records). Does NOT include installed application files.

### Q: Can I restore to a different computer?
**A:** Yes, copy .bak file to different computer's Backups folder and use Restore function.

### Q: How do I contact support?
**A:** 
- Email: soporte@markchp.com
- Phone: +57-XXX-XXXX-XXXX
- Hours: Monday-Friday 9 AM - 5 PM COT
- Response time: Typically within 24 hours

### Q: Is there a training course?
**A:** Yes, contact sales for training packages:
- Online Training: 2-3 hours
- On-site Training: Full day available
- Includes all modules with practice exercises

### Q: Is MarkERP IFRS compliant?
**A:** Yes, uses Colombian accounting standards (PUC). IFRS mappings available.

---

## System Information

**Version:** 1.0.0  
**Release Date:** April 2026  
**Copyright:** MarkCHP Enterprise 2026  
**License:** Commercial License - See License Agreement  

---

### Document Version Information
- Edition 1.0 - Initial Release
- Last Updated: April 2026
- Pages: 15

**For additional help, training, or feature requests, contact our support team.**

---

# Appendix A: Keyboard Shortcuts

| Shortcut | Function |
|----------|----------|
| F5 | Refresh current list |
| Ctrl+S | Save current record |
| Ctrl+N | Create new record |
| Ctrl+F | Search/Filter |
| Ctrl+P | Print |
| Ctrl+E | Export |
| Ctrl+Q | Quit application |
| Escape | Cancel/Close dialog |

---

# Appendix B: Support Contact Information

**Main Office:**
MarkCHP Enterprise
Bogotá, Colombia
Phone: +57-1-XXXXXXX
Email: info@markchp.com

**Technical Support:**
Phone: +57-XXX-XXXX-XXXX
Email: soporte@markchp.com
Hours: Mon-Fri 9 AM - 5 PM COT

**Customer Portal:**
Visit: https://portal.markchp.com
For ticket submission and knowledge base

---

**END OF USER MANUAL**

Thank you for choosing MarkERP! We're committed to your business success.

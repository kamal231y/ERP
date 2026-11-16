# School Management System (Desktop Application)

A Windows desktop application built with **Visual Studio 2008** (.NET Framework 3.5, WinForms) and backed by **Microsoft SQL Server** (compatible with SQL Server 2008 through 2022).

---

## Table of Contents

1. [Overview](#overview)
2. [Modules](#modules)
3. [Technology Stack](#technology-stack)
4. [System Requirements](#system-requirements)
5. [Project Structure](#project-structure)
6. [Database Setup](#database-setup)
7. [Configuration](#configuration)
8. [Building and Running](#building-and-running)
9. [Default Login](#default-login)
10. [Backup and Restore](#backup-and-restore)
11. [Troubleshooting](#troubleshooting)
12. [Version History](#version-history)
13. [License](#license)

---

## Overview

This application helps schools and institutes manage day-to-day operations in one place: master data, student records, fee collection, transport routes, inventory, staff payroll, stock, and bulk SMS communication with parents and staff.

## Modules

### 1. Master
Central setup for reference data used across all other modules.
- Class, section, and academic session setup
- Subjects and departments
- Designations and staff categories
- Fee heads and fee categories
- Routes, vehicles, and drivers (shared with Transport)
- Item categories and units of measure (shared with Inventory and Stock)
- User roles, users, and permissions

### 2. Student
Complete student lifecycle management.
- New admission with photo and document upload
- Student profile (personal, family, and contact details)
- Class and section allocation, promotion, and transfer certificate (TC)
- Attendance tracking
- Student search and printable ID cards

### 3. Fees
Fee structure definition, collection, and reporting.
- Fee plans per class and session
- Monthly, quarterly, or annual fee schedules
- Fee collection with receipt printing
- Concession, discount, and fine handling
- Due list, defaulter list, and collection reports
- Fee ledger per student

### 4. Transport
Management of school bus or van services.
- Route and stop management
- Vehicle and driver assignment
- Student transport enrollment with stop and fare
- Transport fee linking with the Fees module
- Route-wise student lists

### 5. Inventory
Tracking of non-consumable school assets.
- Asset registration with category, location, and condition
- Asset issue and return to staff or departments
- Maintenance and repair log
- Asset status and depreciation reports

### 6. Payroll
Staff salary processing.
- Employee salary structure (basic, allowances, deductions)
- Monthly salary generation
- Leave and attendance based deductions
- Salary slip printing
- Payroll summary and bank statement export

### 7. Stock
Consumable stock management (stationery, lab items, cleaning supplies, and similar).
- Purchase entry with supplier details
- Stock issue to departments or classes
- Minimum stock level alerts
- Stock ledger and current stock report

> Stock and Inventory are kept separate: **Inventory** is for reusable assets, and **Stock** is for items that are consumed.

### 8. SMS
Bulk messaging through an SMS gateway.
- SMS templates with placeholders (student name, fee due, date, and so on)
- Send to all students, class-wise, section-wise, individual, or staff
- Automatic alerts for fee due, absence, and general notices
- Delivery log and failed message retry
- Configurable gateway provider settings

---

## Technology Stack

| Layer         | Technology                                              |
|---------------|---------------------------------------------------------|
| IDE           | Microsoft Visual Studio 2008                            |
| Language      | C# (or VB.NET, depending on the project)                |
| UI            | Windows Forms, .NET Framework 3.5                       |
| Database      | Microsoft SQL Server 2008 / 2008 R2 / 2012 / 2014 / 2016 / 2017 / 2019 / 2022 |
| Data Access   | ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter)     |
| Reports       | Crystal Reports / RDLC (based on the project)           |
| SMS           | HTTP API of the configured SMS gateway                  |

## System Requirements

**Development machine**
- Windows 7 / 8 / 10 / 11
- Visual Studio 2008 SP1 (or later with .NET 3.5 targeting)
- SQL Server 2008 or later (Express edition is sufficient)
- SQL Server Management Studio (SSMS) recommended

**Client machine**
- Windows 7 or later
- .NET Framework 3.5 SP1 (or 4.x runtime)
- Network access to the SQL Server instance (if the database is on another machine)

## Project Structure

```
SchoolManagement/
├── SchoolManagement.sln
├── README.md
├── Database/
│   ├── 01_CreateDatabase.sql
│   ├── 02_Tables.sql
│   ├── 03_StoredProcedures.sql
│   └── 04_SeedData.sql
├── Source/
│   ├── Common/          (DB helper, utilities, constants)
│   ├── Master/          (Master module forms)
│   ├── Student/
│   ├── Fees/
│   ├── Transport/
│   ├── Inventory/
│   ├── Payroll/
│   ├── Stock/
│   ├── SMS/
│   └── Login/           (Login and main menu)
├── Reports/             (RDLC / Crystal Reports files)
├── Resources/           (Icons, images, logos)
└── Docs/                (User manual, screenshots)
```

## Database Setup

The database is compatible with **SQL Server 2008 through 2022**. The scripts avoid features introduced after 2008 wherever possible, so they run on any supported version.

1. Open **SQL Server Management Studio** and connect to your instance.
2. Run the scripts in this order:
   1. `Database/01_CreateDatabase.sql`
   2. `Database/02_Tables.sql`
   3. `Database/03_StoredProcedures.sql`
   4. `Database/04_SeedData.sql` (optional, adds sample master data)
3. Verify that the database `SchoolDB` exists with all tables.

> **Compatibility note:** Use the 2008-compatible syntax only. Avoid `CONCAT`, `IIF`, `TRY_CONVERT`, `FORMAT`, and `STRING_AGG` (these need SQL 2012 or later). If the database is on SQL Server 2022, the scripts still run without changes.

## Configuration

The connection string and SMS settings are stored in `App.config`. Do not hard-code them in forms.

```xml
<configuration>
  <connectionStrings>
    <add name="SchoolDB"
         connectionString="Data Source=SERVER_NAME\INSTANCE;Initial Catalog=SchoolDB;Integrated Security=True;"
         providerName="System.Data.SqlClient" />
  </connectionStrings>

  <appSettings>
    <add key="SchoolName"    value="Your School Name" />
    <add key="SessionYear"   value="2026-2027" />
    <add key="SmsApiUrl"     value="https://your-sms-gateway/api" />
    <add key="SmsApiKey"     value="YOUR_API_KEY" />
    <add key="SmsSenderId"   value="SCHOOL" />
  </appSettings>
</configuration>
```

For SQL authentication, replace `Integrated Security=True` with `User ID=sa;Password=YourPassword;`.

## Building and Running

1. Open `SchoolManagement.sln` in Visual Studio 2008.
2. Restore or verify references (System.Data, System.Drawing, and any report library).
3. Set **Login** as the startup project.
4. Press **F5** to run in debug mode, or use **Build > Build Solution** to create a release build.
5. The release output is in `bin\Release\`. Copy the `.exe`, `App.config`, and required DLLs to the client machine.

## Default Login

| Username | Password  | Role           |
|----------|-----------|----------------|
| admin    | admin@123 | Administrator  |

**Change the default password immediately after the first login.**

## Backup and Restore

**Backup (recommended daily):**
```sql
BACKUP DATABASE SchoolDB
TO DISK = 'D:\Backups\SchoolDB_YYYYMMDD.bak'
WITH INIT, COMPRESSION;
```

**Restore:**
```sql
RESTORE DATABASE SchoolDB
FROM DISK = 'D:\Backups\SchoolDB_YYYYMMDD.bak'
WITH REPLACE;
```

> `COMPRESSION` is available in SQL Server 2008 Enterprise and later, and in all editions from 2016 SP1. Remove it if your edition does not support it.

## Troubleshooting

| Problem | Solution |
|---------|----------|
| "Cannot open database" error | Check the instance name in `App.config` and confirm the database exists. |
| "Named pipes / TCP provider" error | Enable TCP/IP in SQL Server Configuration Manager and restart the SQL service. |
| SMS not sending | Verify `SmsApiUrl` and `SmsApiKey`, and check your SMS balance with the provider. |
| Report not printing | Make sure the report files are in the `Reports` folder and the report runtime is installed. |
| Slow student search | Confirm the index on `Student.AdmissionNo` and `Student.Name` exists (see `02_Tables.sql`). |

## Version History

| Version | Date       | Changes                                  |
|---------|------------|------------------------------------------|
| 1.0.0   | YYYY-MM-DD | Initial release with all eight modules   |

## License

Proprietary. All rights reserved by [Your Name / Organization].
Contact: [email or phone]

# 🚗 Car Rental System API

## Overview

This project is a **Car Rental System API** built using **.NET 8.0**, **Entity Framework Core**, and **SQL Server** for database management. The API handles car rentals, calculates prices, tracks customer loyalty points, and manages car availability.

---

## 🎯 Features

- 📅 Rent one or several cars and calculate the rental price.
- 🔄 Return a car and calculate surcharges (if applicable).
- ⚙️ Update car availability after a rental is created.
- ⭐ Track and update customer loyalty points based on the rented car type.

---

## 🛠 Prerequisites

Ensure the following are installed on your machine:

- **.NET 8 SDK**: [Download here](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- **SQL Server**: [Download here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or use SQL Server Express)
- **Entity Framework Core CLI**: Install using the command:
  ```bash
  dotnet tool install --global dotnet-ef

🔧 Step 1: Clone the Project
1.Open your terminal or command prompt.

2.Run the following command to clone the repository:
```bash
https://github.com/EberthCastro/CarReSys.git

3.Navigate into the project directory:
```bash
cd CarRentalSystem

📦 Step 2: Install Dependencies
Once inside the project directory, restore the necessary NuGet packages by running:
```bash
dotnet restore

This command will install all the required dependencies such as:

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools


🗄 Step 3: Configure the Database Connection
1.Open the project in your preferred editor (Visual Studio, VS Code, etc.).

2.Navigate to the appsettings.json file.

3.Update the DefaultConnection string to point to your SQL Server instance. Replace YourServerName with your SQL Server name and YourDatabaseName with your desired database name.
```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YourServerName;Database=YourDatabaseName;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}


🏗 Step 4: Migrate the Database
1.Run the following command to create the database schema:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update

This will create the necessary tables in your database ( Cars and Customers).

👏 Conclusion
You've now set up the Car Rental System API on your local machine. From here, you can extend the functionality or integrate it with a front-end for a complete car rental management system. 🚀



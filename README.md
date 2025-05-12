# Agri-Energy Connect

GitHub repository: [https://github.com/JoshuaWood13/JoshuaWood_ST10296167_PROG7311_POE.git](https://github.com/JoshuaWood13/JoshuaWood_ST10296167_PROG7311_POE.git)

## Setting Up the Development Environment

1. Install Visual Studio 2022 or later with the following workloads:
   - .NET 8.0 SDK or later
   - ASP.NET Core

2. Clone the project code:
   ```bash
   git clone https://github.com/JoshuaWood13/JoshuaWood_ST10296167_PROG7311_POE.git
Or extract the project `.zip` file to a folder of your choosing

3. Navigate to the folder and open the `.sln` file using Visual Studio

> **Note:** There is no need to manually create or setup the database. A local SQLite database file will be created and populated when first running the application.

## Building and Running the Prototype

Once you have opened the project in Visual Studio:

1. Navigate to the Solution Explorer (View -> Solution Explorer)
2. Right-click the solution file
3. Click **Restore NuGet Packages**
4. Verify the project includes the following NuGet packages:
   - Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
   - Microsoft.AspNetCore.Identity.EntityFrameworkCore
   - Microsoft.AspNetCore.Identity.UI
   - Microsoft.EntityFrameworkCore.Sqlite
   - Microsoft.EntityFrameworkCore.Tools
   - Microsoft.VisualStudio.Web.CodeGeneration.Design
5. Build the solution: **Build -> Build Solution**
6. Run the application

## User Roles and Default Accounts

This prototype includes two distinct user roles: **Employee** and **Farmer**.

The following accounts are automatically created in the local database when first running the app:

### Employee Account
- **Email:** employee@agrienergy.com
- **Password:** Employee123!

### Farmer Accounts
1. **Farmer 1**
   - **Email:** farmer1@agrienergy.com
   - **Password:** Farmer1!

2. **Farmer 2**
   - **Email:** farmer2@agrienergy.com
   - **Password:** Farmer2!

3. **Farmer 3**
   - **Email:** farmer3@agrienergy.com
   - **Password:** Farmer3!

> **Important:** You must log in to an account to access the application's functionality.

## Application Features

### Farmer Functionality

After logging in with a farmer account, users can:

#### Add Product
- Click the "Add Product" button on the home page or navigation bar
- Fill in product details (name, price, category, description)
- Create the product by clicking "Create Product" after entering valid data

#### View Products
- Access a list of their added products by clicking "Your Products" in the navigation bar
- View product details including:
  - Product ID
  - Name
  - Price
  - Category
  - Description
  - Date added

#### Logout
- Click the "Logout" button in the navigation bar to end the session

### Employee Functionality

After logging in with the employee account, users can:

#### Add Farmer
- Create new farmer accounts by clicking "Add Farmer" in the navigation bar
- Enter the new farmer's details:
  - First name
  - Last name
  - Email
  - Password (with confirmation)
- The system automatically generates and assigns a farmer code
- Create the account by clicking "Create Farmer" after entering valid data

#### View Products
- Access a complete list of all farmer products by clicking "View Products"
- View comprehensive product details:
  - Farmer code
  - Product code
  - Name
  - Price
  - Category
  - Description
  - Date added
- Filter products by:
  - Farmer
  - Category
  - Date range
- Apply filters with the "Apply" button
- Reset all filters with the "Reset" button

#### Logout
- Click the "Logout" button in the navigation bar to end the session
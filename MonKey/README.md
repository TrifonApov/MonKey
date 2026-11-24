# Mon(K)ey

A personal finance web app for tracking accounts, purchases and spending by product and category.

> **Status:** early development. The features below are the planned scope and will be checked off as they are built.

## About

Mon(K)ey helps you see where your money goes. Each user can have one or more accounts (cash, bank, card, savings) and record purchases made up of individual products, each with a category and a price.

The name is a small pun: **Mon** + **ey** reads as *money*, and with the K it reads as *monkey*.

## Features

- [ ] User registration and login (ASP.NET Core Identity)
- [ ] Multiple accounts per user (cash, bank, card, savings)
- [ ] Purchases with multiple line items (product, category, quantity, unit price)
- [ ] Custom categories and subcategories
- [ ] Reusable product catalog with default categories
- [ ] Spending overview by account, category and period
- [ ] Budgets per category
- [ ] Income and transfers between accounts
- [ ] Recurring transactions
- [ ] Multi-currency support

## Tech stack

- **Framework:** ASP.NET MVC (.NET `<version>`)
- **ORM:** Entity Framework Core
- **Database:** Microsoft SQL Server
- **Authentication:** ASP.NET Core Identity

## Data model (overview)

| Entity | Description |
|---|---|
| `ApplicationUser` | The signed-in user |
| `Account` | A user's account, with name, type, currency and initial balance |
| `Category` | A user's spending category, optionally nested under a parent |
| `Product` | A reusable product with an optional default category |
| `Purchase` | A purchase on an account, with date, merchant and note |
| `PurchaseItem` | A line in a purchase: product, category, quantity and unit price |

Relationships: a user has many accounts, categories and products; an account has many purchases; a purchase has many items.

## Getting started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) `<version>` or later
- SQL Server (LocalDB, Express, or a full instance)
- Optional: Visual Studio or JetBrains Rider

### Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/<your-username>/<your-repo>.git
   cd <your-repo>
   ```

2. Set your connection string in `appsettings.Development.json`:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=MonKey;Trusted_Connection=True;"
     }
   }
   ```

3. Apply the database migrations:

   ```bash
   dotnet ef database update
   ```

4. Run the app:

   ```bash
   dotnet run
   ```

5. Open the URL shown in the console (usually `https://localhost:5001`).

## Project structure

```
MonKey/
├── Controllers/     # MVC controllers
├── Models/          # Entities and view models
├── Data/            # DbContext and migrations
├── Views/           # Razor views
└── wwwroot/         # Static files
```

## Roadmap

1. Core data model and migrations
2. Accounts and purchases CRUD
3. Categories and products
4. Reports and charts
5. Budgets, income, transfers and recurring transactions

## Contributing

This is a personal project, but suggestions and bug reports are welcome. Please open an issue to discuss any larger change before sending a pull request.

## License

Add a license of your choice (for example MIT) and describe it here.

---

*Made with love for M.* 🐒

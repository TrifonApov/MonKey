# MonKey

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

- **Framework:** ASP.NET MVC (.NET 10.00)
- **ORM:** Entity Framework Core
- **Database:** Microsoft SQL Server
- **Authentication:** ASP.NET Core Identity

## Getting started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB, Express, or a full instance)
- Optional: Visual Studio or JetBrains Rider

## Contributing

This is a personal project, but suggestions and bug reports are welcome. Please open an issue to discuss any larger change before sending a pull request.

## License

MIT, see LICENSE

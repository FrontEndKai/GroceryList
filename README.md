# GroceryMate

A .NET MAUI grocery list app with login, signup, product catalog, price compare, and grocery list management.

## Features
- Login and sign up (local demo auth)
- Product catalog with search and view product details
- Add new products
- Grocery list with quantity controls and totals
- Price compare across stores

## Run
- Build: `dotnet build GroceryMate.csproj`
- Windows (desktop): `dotnet build GroceryMate.csproj -t:Run -f net10.0-windows10.0.19041.0`
- Android emulator/device: `dotnet build GroceryMate.csproj -t:Run -f net10.0-android`
- iOS simulator (macOS only): `dotnet build GroceryMate.csproj -t:Run -f net10.0-ios`
- MacCatalyst (macOS only): `dotnet build GroceryMate.csproj -t:Run -f net10.0-maccatalyst`

## Demo login
- Email: demo@grocerymate.com
- Password: password123

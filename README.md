# BSDorderBook

## Description
Solution gives the user the lowest possible purchase price for a certain amount of BTC or the highest possible sell price for a certain amount of BTC.

## Running the solution

### Deploy the solution using Docker (recommended)
- Navigate to root directory of project
- Deploy using;
```bash
  docker-compose up --build -d
```
- Access the service (Swagger); http://localhost:5000/swagger/index.html
- Access the API;
    - Buy; http://localhost:5000/api/MatchOrder/buy/{amount}
    - Sell; http://localhost:5000/api/MatchOrder/sell/{amount}


### Running the .NET Core console-mode application
- Navigate to directory of console application
- Build the application using; ``` dotnet build ```
- Run the application using; ``` dotnet run ```


### Running the .NET Core web service
- Navigate to directory of OrderBookAPI
- Build the service using; ``` dotnet build ```
- Run the service using; ``` dotnet run ```


### Running the xUnit tests
- Navigate to directory of MatchOrdersServiceTests
- Run the tests using; ``` dotnet test ```


## Project Structure
```
.
├── BSDorderBook/
│   └── Program.cs # Console-mode application
├── CoreLibrary/
│   ├── Data/
│   │   └── DataRepository.cs # Logic for loading JSON files for crypto exchanges and order books
│   ├── Models/
│   │   └── ... # Classes for objects used
│   ├── Resources/
│   │   └── ... # JSON files for crypto exchanges and order book
│   └── Services/
│       └── ... # Service interface and logic implementation
├── MatchOrdersServiceTests/
│   └── UnitTestOrderBook.cs # xUnit tests
└── OrderBookAPI/
    ├── Controllers/
    │   └── MatchOrderController.cs # API controller
    ├── Dockerfile # Dockerfile for building the service
        └── Program.cs # Configuration for HTTP pipeline and routes

```

## Notes
- Mock crypto exchanges data used (Id being the same as AcqTime in order books for easy identification)
- Minimal validation used in solution
- Simple tests implemented with xUint

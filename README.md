# Factory Systems Application

A full-stack web application for managing factory systems, built with .NET and Angular. This application provides complete CRUD operations for maintaining applications of a factory.

## Technology Stack

### Backend
- **.NET 8** - Web API
- **C#** - Programming language
- **Entity Framework Core** - ORM with automatic migrations
- **SQL Lite** - Local database
- **xUnit** - Unit testing framework

### Frontend
- **Angular** - Frontend framework
- **TypeScript** - Programming language
- **TestBed** - Angular testing utility

## Prerequisites

Before running this application, ensure you have the following installed:

### Required Software
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/en/download/) (latest version)
- [npm](https://www.npmjs.com/) (comes with Node.js)

_NOTE: Once everything is downloaded and installed, the following commands should be available via cli_

```bash
dotnet --version
npm --version
node --version
```

### Recommended Tools
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## Getting Started

1. Clone the Repository
    ```bash
    git clone https://github.com/Verbinen/FactorySystems.git
    ```
### Running Backend (.NET 8 Web API)

1. Navigate to the backend web api folder
   ```bash
   cd FactorySystems/backend/WebApi
   ```
2. Execute the run command
    ```csharp
    dotnet run
    ```
    _NOTE: Once the Web Api is running, it will be accessible at: `https://localhost:7109/`._

### **Running the Frontend (Angular)**

1. Navigate to the frontend folder
    ```bash
    cd FactorySystems/frontend
    ```
2. Install Angular CLI
    ```bash
    npm install @angular/cli
    ```
3. Execute the run command
    ```bash
    ng s
    ```
    _NOTE: Once the server is running, open your browser and navigate to `http://localhost:4200/`._

### **Running the Frontend Tests**

1. Navigate to the frontend folder
    ```bash
    cd FactorySystems/frontend
    ```
2. Execute the test command
    ```bash
    ng test
    ```
   _NOTE: A browser page will open with the test information_

### **Running the Backend Tests**

1. Navigate to the backend test folder
   ```bash
   cd FactorySystems/backend/UnitTests
   ```
2. Run the test command
   ```bash
   dotnet test
   ```
Alternatively, you can also:

1. Open the backend solution on Visual Studio
2. Open the Solution Explorer (default command: Ctrl + Alt + L)
3. Right click the UnitTests project
4. Select `Run Tests`
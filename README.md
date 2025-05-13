# Log Analyser .NET Core app

.NET Core app that processes log data and outputs a json report.

## About 

The Log Analyser is an app that reads log data from a server and returns a proper json report.

## Running locally in development mode

To get started, just clone the repository and run `dotnet restore & dotnet build & dotnet run`:

    git clone https://github.com/cristencean/cristencean-log-analysis.git
    dotnet restore
    dotnet build
    dotnet run
    testing example: http://localhost:5211/api/loganalysis

## Project structure

    .
    ├── Controllers/            # App Controllers including API ones
    ├── Models/                 # Models directory
    │   └── Api                 # Api response models
    ├── logs/                   # Mock log data
    ├── Services/               # Business logic
    └── Program.cs/             # App entry point file
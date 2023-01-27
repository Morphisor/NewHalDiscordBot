FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY ./app .
COPY ./Config ./Config
ENTRYPOINT ["dotnet", "HalDiscrodBot.Console.dll"]

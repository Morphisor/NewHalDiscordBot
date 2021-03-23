FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
COPY ./app .
COPY ./Config ./Config
ENTRYPOINT ["dotnet", "HalDiscrodBot.Console.dll"]

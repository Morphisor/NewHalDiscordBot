FROM mcr.microsoft.com/dotnet/core/sdk:2.1.808
WORKDIR /app
COPY ./bin/Debug/netcoreapp2.0/publish/ .
COPY ./Config ./Config
COPY ./ffmpeg ./ffmpeg
ENTRYPOINT ["dotnet", "HalDiscrodBot.Console.dll"]

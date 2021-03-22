FROM mcr.microsoft.com/dotnet/core/sdk:2.1.808
WORKDIR /app
COPY ./app .
COPY ./Config ./Config
COPY ./ffmpeg ./ffmpeg
COPY ./SQLite.Interop.dll ./SQLite.Interop.dll
ENTRYPOINT ["dotnet", "HalDiscrodBot.Console.dll"]

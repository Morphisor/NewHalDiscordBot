FROM mcr.microsoft.com/dotnet/core/sdk:2.1.808
WORKDIR /app
COPY ./app .
COPY ./Config ./Config

WORKDIR /
RUN mv /app/runtimes/linux-x64/native/SQLite.Interop.dll /lib/SQLite.Interop.dll

WORKDIR /app
ENTRYPOINT ["dotnet", "HalDiscrodBot.Console.dll"]

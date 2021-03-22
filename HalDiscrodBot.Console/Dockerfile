FROM mcr.microsoft.com/dotnet/core/sdk:2.1.808
WORKDIR /app
COPY ./app .
COPY ./Config ./Config

WORKDIR /
RUN mv /app/SQLite.Interop.dll /lib/SQLite.Interop.dll

WORKDIR /app
RUN apt-get update && apt-get install -y libunwind8
ENTRYPOINT ["dotnet", "HalDiscrodBot.Console.dll"]

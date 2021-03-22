FROM mcr.microsoft.com/dotnet/core/sdk:2.1.808
WORKDIR /app
COPY ./app .
COPY ./Config ./Config
RUN apt-get update \
    apt-get install -y libunwind8
ENTRYPOINT ["dotnet", "HalDiscrodBot.Console.dll"]

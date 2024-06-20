FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY ./app .
COPY ./Config ./Config
RUN apt-get update
RUN	apt-get install -y libsodium-dev
RUN	apt-get install opus-tools
ENTRYPOINT ["dotnet", "HalDiscrodBot.Console.dll"]

FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY ./app .
COPY ./Config ./Config
RUN apt-get update \
	apt-get install -y libsodium-dev \
	apt-get install opus-tools
ENTRYPOINT ["dotnet", "HalDiscrodBot.Console.dll"]

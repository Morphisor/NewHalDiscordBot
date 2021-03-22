FROM mcr.microsoft.com/dotnet/core/sdk:2.1.808
WORKDIR /app
RUN apt-get update
RUN apt-get install -y sqlite3
COPY ./app .
COPY ./Config ./Config
COPY ./ffmpeg ./ffmpeg
ENTRYPOINT ["dotnet", "HalDiscrodBot.Console.dll"]

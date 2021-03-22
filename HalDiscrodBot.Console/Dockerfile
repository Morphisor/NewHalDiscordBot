FROM mcr.microsoft.com/dotnet/core/sdk:2.1.808
WORKDIR /app
RUN apt-get install sqlite3
COPY ./app .
COPY ./Config ./Config
COPY ./ffmpeg ./ffmpeg
ENTRYPOINT ["dotnet", "HalDiscrodBot.Console.dll"]

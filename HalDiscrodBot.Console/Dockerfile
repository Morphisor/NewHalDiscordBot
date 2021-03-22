FROM mcr.microsoft.com/dotnet/core/sdk:2.1.808
WORKDIR /app
COPY ./app .
COPY ./Config ./Config
COPY ./ffmpeg ./ffmpeg
COPY ./x86 ./x86
COPY ./x64 ./x64
ENTRYPOINT ["dotnet", "HalDiscrodBot.Console.dll"]

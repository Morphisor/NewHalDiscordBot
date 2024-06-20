FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY ./app .
COPY ./Config ./Config
RUN apt-get update
RUN	apt-get install -y libsodium-dev
RUn apt-get install -y libopus0
RUN	apt-get install -y opus-tools
RUN cp /usr/lib/x86_64-linux-gnu/libopus.so.0.8.0 /app/opus.so
RUN cp /usr/lib/x86_64-linux-gnu/libsodium.so /app/libsodium.so
ENTRYPOINT ["dotnet", "HalDiscrodBot.Console.dll"]

FROM mcr.microsoft.com/dotnet/sdk:3.1
COPY . /tmp/.
WORKDIR /tmp/RabbitProducerCore
RUN dotnet build
WORKDIR /tmp/RabbitProducerCore/RabbitProducerCore
ENTRYPOINT ["dotnet", "/tmp/RabbitProducerCore/RabbitProducerCore/bin/Debug/netcoreapp3.1/RabbitProducerCore.dll"]

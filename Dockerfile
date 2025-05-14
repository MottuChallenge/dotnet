FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./

ENV ASPNETCORE_ENVIRONMENT=Development

RUN dotnet tool install --global dotnet-watch
ENV PATH="$PATH:/root/.dotnet/tools"

EXPOSE 80

CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:80"]

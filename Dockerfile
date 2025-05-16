FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./

ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 80

CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:80"]
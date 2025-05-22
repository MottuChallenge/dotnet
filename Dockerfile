FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev

WORKDIR /app

RUN useradd -m appuser

COPY *.csproj ./
RUN dotnet restore

COPY . ./

ENV ASPNETCORE_ENVIRONMENT=Development

RUN chown -R appuser:appuser /app

EXPOSE 80

USER appuser

CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:80"]

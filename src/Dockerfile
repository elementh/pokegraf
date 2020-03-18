FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY *.sln ./

COPY Pokegraf.Bot.Api/*.csproj Pokegraf.Bot.Api/
COPY Pokegraf.Common/*.csproj Pokegraf.Common/
COPY Pokegraf.Core.Domain/*.csproj Pokegraf.Core.Domain/
COPY Pokegraf.Core.Entity/*.csproj Pokegraf.Core.Entity/
COPY Pokegraf.Infrastructure.Contract/*.csproj Pokegraf.Infrastructure.Contract/
COPY Pokegraf.Infrastructure.Implementation/*.csproj Pokegraf.Infrastructure.Implementation/
COPY Pokegraf.Persistence/*.csproj Pokegraf.Persistence/
COPY Pokegraf.Persistence.Migration/*.csproj Pokegraf.Persistence.Migration/

RUN dotnet restore -r linux-musl-x64

COPY . .
WORKDIR /src/Pokegraf
RUN dotnet publish -c Release -o /app -r linux-musl-x64 --self-contained false --no-restore ../Pokegraf.sln 

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:5100
EXPOSE 5100

ENTRYPOINT ["dotnet", "Pokegraf.Bot.Api.dll"]

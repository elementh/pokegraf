FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:5100
EXPOSE 5100

FROM microsoft/dotnet:sdk as builder
ARG Configuration=Release
WORKDIR /src
COPY *.sln ./

COPY Pokegraf.Api.WebApi/*.csproj Pokegraf.Api.WebApi/
COPY Pokegraf.Application.Contract/*.csproj Pokegraf.Application.Contract/
COPY Pokegraf.Application.Implementation/*.csproj Pokegraf.Application.Implementation/
COPY Pokegraf.Common/*.csproj Pokegraf.Common/
COPY Pokegraf.Domain/*.csproj Pokegraf.Domain/
COPY Pokegraf.Domain.Entity/*.csproj Pokegraf.Domain.Entity/
COPY Pokegraf.Infrastructure.Contract/*.csproj Pokegraf.Infrastructure.Contract/
COPY Pokegraf.Infrastructure.Implementation/*.csproj Pokegraf.Infrastructure.Implementation/
COPY Pokegraf.Persistence.Contract/*.csproj Pokegraf.Persistence.Contract/
COPY Pokegraf.Persistence.Implementation/*.csproj Pokegraf.Persistence.Implementation/

RUN dotnet restore
COPY . .
WORKDIR /src/Pokegraf
RUN dotnet build -c $Configuration -o /app ../Pokegraf.sln 

FROM builder as publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app ../Pokegraf.sln

FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Pokegraf.Api.WebApi.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ecommerce-public-api-1.csproj", "."]
RUN dotnet restore "./ecommerce-public-api-1.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ecommerce-public-api-1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ecommerce-public-api-1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ecommerce-public-api-1.dll", "--server.urls", "http://+:80"]]
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FIAP_TDD/FIAP_TDD.csproj", "FIAP_TDD/"]
RUN dotnet restore "FIAP_TDD/FIAP_TDD.csproj"
COPY . .
WORKDIR "/src/FIAP_TDD"
RUN dotnet build "FIAP_TDD.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FIAP_TDD.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FIAP_TDD.dll"]
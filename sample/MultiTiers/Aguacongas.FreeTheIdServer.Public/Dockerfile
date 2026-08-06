FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Aguacongas.FreeTheIdServer/Aguacongas.FreeTheIdServer.csproj", "src/Aguacongas.FreeTheIdServer/"]
RUN dotnet restore "src/Aguacongas.FreeTheIdServer/Aguacongas.FreeTheIdServer.csproj"
COPY . .
WORKDIR "/src/src/Aguacongas.FreeTheIdServer"
RUN dotnet build "Aguacongas.FreeTheIdServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aguacongas.FreeTheIdServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aguacongas.FreeTheIdServer.dll"]
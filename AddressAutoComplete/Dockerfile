FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN mkdir /app/osmFiles
COPY osmFiles/bretagne-latest.osm.pbf /app/osmFiles/osmData.osm.pbf
WORKDIR /src
COPY ["AddressAutoComplete/AddressAutoComplete.csproj", "AddressAutoComplete/"]
RUN dotnet restore "AddressAutoComplete/AddressAutoComplete.csproj"
COPY . .
WORKDIR "/src/AddressAutoComplete"
RUN dotnet build "AddressAutoComplete.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AddressAutoComplete.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AddressAutoComplete.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/TrailApi/TrailApi.csproj", "src/TrailApi/"]
RUN dotnet restore "src/TrailApi/TrailApi.csproj"
COPY . .
WORKDIR "/src/src/TrailApi"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TrailApi.dll"]
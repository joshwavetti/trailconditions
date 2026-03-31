FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Frontend/Frontend.csproj", "src/Frontend/"]
RUN dotnet restore "src/Frontend/Frontend.csproj"
COPY . .
WORKDIR "/src/src/Frontend"
RUN dotnet publish -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .
COPY src/Frontend/nginx.conf /etc/nginx/nginx.conf
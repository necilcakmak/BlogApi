FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Blog.MonitoringService/Blog.MonitoringService.csproj", "Blog.MonitoringService/"]
RUN dotnet restore "Blog.MonitoringService/Blog.MonitoringService.csproj"
COPY . .
WORKDIR "/src/Blog.MonitoringService"
RUN dotnet build "Blog.MonitoringService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.MonitoringService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.MonitoringService.dll"]
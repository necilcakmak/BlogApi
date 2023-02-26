FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Blog.WorkerService/Blog.WorkerService.csproj", "Blog.WorkerService/"]
COPY ["Blog.Core/Blog.Core.csproj", "Blog.Core/"]
RUN dotnet restore "Blog.WorkerService/Blog.WorkerService.csproj"
COPY . .
WORKDIR "/src/Blog.WorkerService"
RUN dotnet build "Blog.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blog.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.WorkerService.dll"]
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY *.sln .
COPY ["Blog.Api/Blog.Api.csproj", "Blog.Api/"]
COPY ["Blog.Business/Blog.Business.csproj", "Blog.Business/"]
COPY ["Blog.Dto/Blog.Dto.csproj", "Blog.Dto/"]
COPY ["Blog.Core/Blog.Core.csproj", "Blog.Core/"]
COPY ["Blog.Entities/Blog.Entities.csproj", "Blog.Entities/"]
COPY ["Blog.Repository/Blog.Repository.csproj", "Blog.Repository/"]

RUN dotnet restore "Blog.Api/Blog.Api.csproj"
COPY . .
WORKDIR "/app/Blog.Api"
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/Blog.Api/out ./
ENTRYPOINT ["dotnet", "Blog.Api.dll"]
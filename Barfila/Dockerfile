
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY ["Barfila/Barfila.csproj", "Barfila/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]


RUN dotnet restore "Barfila/Barfila.csproj"


COPY . .


RUN dotnet build "Barfila/Barfila.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "Barfila/Barfila.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Barfila.dll"]
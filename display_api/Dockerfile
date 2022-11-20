#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RDOS.TMK_DisplayAPI/RDOS.TMK_DisplayAPI.csproj", "RDOS.TMK_DisplayAPI/"]
RUN dotnet restore "RDOS.TMK_DisplayAPI/RDOS.TMK_DisplayAPI.csproj"
COPY . .
WORKDIR "/src/RDOS.TMK_DisplayAPI"
RUN dotnet build "RDOS.TMK_DisplayAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RDOS.TMK_DisplayAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RDOS.TMK_DisplayAPI.dll"]
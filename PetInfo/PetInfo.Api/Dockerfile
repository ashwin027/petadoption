#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PetInfo/PetInfo.Api/PetInfo.Api.csproj", "PetInfo/PetInfo.Api/"]
COPY ["PetInfo/PetInfo.Models/PetInfo.Models.csproj", "PetInfo/PetInfo.Models/"]
COPY ["PetInfo/PetInfo.Repository/PetInfo.Repository.csproj", "PetInfo/PetInfo.Repository/"]
RUN dotnet restore "PetInfo/PetInfo.Api/PetInfo.Api.csproj"
COPY . .
WORKDIR "/src/PetInfo/PetInfo.Api"
RUN dotnet build "PetInfo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PetInfo.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetInfo.Api.dll"]
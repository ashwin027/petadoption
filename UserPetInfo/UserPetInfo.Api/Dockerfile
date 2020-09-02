#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["UserPetInfo/UserPetInfo.Api/UserPetInfo.Api.csproj", "UserPetInfo/UserPetInfo.Api/"]
COPY ["UserPetInfo/UserPetInfo.Repository/UserPetInfo.Repository.csproj", "UserPetInfo/UserPetInfo.Repository/"]
COPY ["UserPetInfo/UserPetInfo.Models/UserPetInfo.Models.csproj", "UserPetInfo/UserPetInfo.Models/"]
RUN dotnet restore "UserPetInfo/UserPetInfo.Api/UserPetInfo.Api.csproj"
COPY . .
WORKDIR "/src/UserPetInfo/UserPetInfo.Api"
RUN dotnet build "UserPetInfo.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserPetInfo.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserPetInfo.Api.dll"]
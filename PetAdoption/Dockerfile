#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM ashwin027/dotnet-3.1-buster-node:latest AS build
WORKDIR /src
COPY ["PetAdoption/PetAdoption.csproj", "PetAdoption/"]
COPY ["PetAdoption.Repository/PetAdoption.Repository.csproj", "PetAdoption.Repository/"]
COPY ["PetAdoption.Models/PetAdoption.Models.csproj", "PetAdoption.Models/"]
RUN dotnet restore "PetAdoption/PetAdoption.csproj"
COPY . .
WORKDIR "/src/PetAdoption"
RUN dotnet build "PetAdoption.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PetAdoption.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetAdoption.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#EN ESTA PARTE DEL DOCKERFILE SE CREA EL DOCKER DE LA APLICACIÓN

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Baskest/Baskest.API/Baskest.API.csproj", "Services/Baskest/Baskest.API/"]
RUN dotnet restore "Services/Baskest/Baskest.API/Baskest.API.csproj"
COPY . .
WORKDIR "/src/Services/Baskest/Baskest.API"
RUN dotnet build "Baskest.API.csproj" -c Release -o /app/build


# EN ESTA PARTE SE CORRE LA APLICACIÓN
FROM build AS publish
RUN dotnet publish "Baskest.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Baskest.API.dll"]

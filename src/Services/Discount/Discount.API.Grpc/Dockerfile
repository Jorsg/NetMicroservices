#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Discount/Discount.API.Grpc/Discount.API.Grpc.csproj", "Services/Discount/Discount.API.Grpc/"]
RUN dotnet restore "Services/Discount/Discount.API.Grpc/Discount.API.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Discount/Discount.API.Grpc"
RUN dotnet build "Discount.API.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.API.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.API.Grpc.dll"]
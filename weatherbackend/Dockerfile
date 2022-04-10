#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:lts-alpine as build-frontend
RUN mkdir -p /app
WORKDIR /app
COPY ./weatherapp/package.json /app
RUN npm install
COPY ./weatherapp /app
RUN rm src/proxy.conf.js
RUN npm run build --prod

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["weatherbackend/weatherbackend.csproj", "weatherbackend/"]
RUN dotnet restore "weatherbackend/weatherbackend.csproj"
COPY ./weatherbackend/ ./weatherbackend
COPY --from=build-frontend /app/dist/weatherapp ./weatherbackend/wwwroot
WORKDIR "/src/weatherbackend"
RUN dotnet build "weatherbackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "weatherbackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "weatherbackend.dll"]
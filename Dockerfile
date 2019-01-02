FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
COPY . .
RUN dotnet restore "BangumiProject.sln"
RUN dotnet build "BangumiProject.sln" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BangumiProject.sln" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BangumiProject.dll"]
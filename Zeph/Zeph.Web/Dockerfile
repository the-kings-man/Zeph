FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Zeph.Web/Zeph.Web.csproj", "Zeph.Web/"]
RUN dotnet restore "Zeph.Web/Zeph.Web.csproj"
COPY . .
WORKDIR "/src/Zeph.Web"
RUN dotnet build "Zeph.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Zeph.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Zeph.Web.dll"]
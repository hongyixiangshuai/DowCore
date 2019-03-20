#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk-nanoserver-1809 AS build
WORKDIR /src
COPY ["src/Dow.Core.Web.Host/Dow.Core.Web.Host.csproj", "src/Dow.Core.Web.Host/"]
RUN dotnet restore "src/Dow.Core.Web.Host/Dow.Core.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/Dow.Core.Web.Host"
RUN dotnet build "Dow.Core.Web.Host.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Dow.Core.Web.Host.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dow.Core.Web.Host.dll"]

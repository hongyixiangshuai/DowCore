#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk AS build
WORKDIR /src
COPY ["src/Dow.Core.Web.Host/Dow.Core.Web.Host.csproj", "src/Dow.Core.Web.Host/"]
COPY ["src/Dow.Core.Web.Core/Dow.Core.Web.Core.csproj", "src/Dow.Core.Web.Core/"]
COPY ["src/Dow.Core.Application/Dow.Core.Application.csproj", "src/Dow.Core.Application/"]
COPY ["src/Dow.Core.Core/Dow.Core.Core.csproj", "src/Dow.Core.Core/"]
COPY ["src/Dow.Core.EntityFrameworkCore/Dow.Core.EntityFrameworkCore.csproj", "src/Dow.Core.EntityFrameworkCore/"]
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
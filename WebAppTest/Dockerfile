FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY ["WebApp/WebApp.csproj", "WebApp/"]
COPY ["WebAppTest/WebAppTest.csproj", "WebAppTest/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "WebAppTest/WebAppTest.csproj"

COPY . .
RUN dotnet test WebAppTest/WebAppTest.csproj -c Release

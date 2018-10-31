FROM microsoft/dotnet:sdk AS build-env
WORKDIR /app

# Copy everything else and build
COPY . ./
RUN dotnet restore ./VectorCalc.sln
RUN dotnet publish ./VectorCalc.sln -c Release -o out

EXPOSE 49374

# Build runtime image
FROM microsoft/dotnet:2.0-runtime
WORKDIR /app
COPY --from=build-env /app/VectorCalc.TcpServer/out .
ENTRYPOINT ["dotnet", "VectorCalc.TcpServer.dll"]
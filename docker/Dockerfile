FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copy solution and projects
COPY ParkingLot.sln .
COPY ParkingLot.Common/ ParkingLot.Common/
COPY ParkingLot/ ParkingLot/
COPY ParkingLot.Tests/ ParkingLot.Tests/

RUN dotnet restore ParkingLot.sln
RUN dotnet build ParkingLot.sln -c Release -o /app/build

# test stage: run unit tests during image build (CI-friendly)
FROM build AS test
RUN dotnet test ParkingLot.Tests/ParkingLot.Tests.csproj -c Release --no-restore

# publish artifacts for the runtime image
FROM build AS publish
RUN dotnet publish ParkingLot/ParkingLot.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ParkingLot.dll"]
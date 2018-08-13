FROM microsoft/dotnet:2.1-sdk
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY cotb-emulator/*.csproj ./cotb-emulator/
COPY cotb-emulator-tests/*.csproj ./cotb-emulator-tests/
RUN dotnet restore

# copy everything else and build app
COPY cotb-emulator ./cotb-emulator
COPY cotb-emulator-tests ./cotb-emulator-tests
RUN dotnet publish -c Release -o out

WORKDIR /app/cotb-emulator/out
ENTRYPOINT ["dotnet", "cotb-emulator.dll"]
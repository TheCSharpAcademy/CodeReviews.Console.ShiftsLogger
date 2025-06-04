# Code Quality Analysis

- Code quality analysis is done through [SonarQube Cloud](https://sonarcloud.io/)
- It analyses the codebase for best practices along with reliability, security and maintainability issues
- Paired with coverlet.collector NuGet package we get a Test Coverage percentage which has to be >80%

## Github Actions Workflow

- Code analysis runs after push to develop branch as a github action
- Example workflow file *build.yml*

```yml
name: SonarQube Cloud
on:
  push:
    branches:
      - develop
  pull_request:
    types: [opened, synchronize, reopened]
jobs:
  build:
    name: Build and analyze
    runs-on: windows-latest
    environment: Shifts Logger
    steps:
      - name: Set up JDK 17
        uses: actions/setup-java@v4
        with:
          java-version: 17
          distribution: 'zulu' # Alternative distribution options are available.
      - uses: actions/checkout@v4
        with:
          fetch-depth: 0  # Shallow clones should be disabled for a better relevancy of analysis
      - name: Cache SonarQube Cloud packages
        uses: actions/cache@v4
        with:
          path: ~\sonar\cache
          key: ${{ runner.os }}-sonar
          restore-keys: ${{ runner.os }}-sonar
      - name: Cache SonarQube Cloud scanner
        id: cache-sonar-scanner
        uses: actions/cache@v4
        with:
          path: .\.sonar\scanner
          key: ${{ runner.os }}-sonar-scanner
          restore-keys: ${{ runner.os }}-sonar-scanner
      - name: Install SonarQube Cloud scanner
        if: steps.cache-sonar-scanner.outputs.cache-hit != 'true'
        shell: powershell
        run: |
          New-Item -Path .\.sonar\scanner -ItemType Directory
          dotnet tool update dotnet-sonarscanner --tool-path .\.sonar\scanner
      - name: Build and analyze
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}  # Needed to get PR information, if any
          SONAR_TOKEN: ${{ secrets.SONAR_TOKEN }}
        shell: powershell
        run: |
          .\.sonar\scanner\dotnet-sonarscanner begin /k:"nwdorian_ShiftsLogger" /o:"nwdorian" /d:sonar.token="${{ secrets.SONAR_TOKEN }}" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.cs.opencover.reportsPaths=coverage.xml /d:sonar.exclusions=**\Migrations\*
          dotnet tool install --global coverlet.console
          dotnet build WebApi/WebApi.sln --no-incremental
          coverlet .\WebApi\tests\ShiftsLogger.UnitTests\bin\Debug\net9.0\ShiftsLogger.UnitTests.dll --target "dotnet" --targetargs "test WebApi/WebApi.sln --no-build"
          coverlet .\WebApi\ShiftsLogger.IntegrationTests\bin\Debug\net9.0\ShiftsLogger.IntegrationTests.dll --target "dotnet" --targetargs "test WebApi/WebApi.sln --no-build" --merge-with "coverage.json" -f=opencover -o="coverage.xml"
          .\.sonar\scanner\dotnet-sonarscanner end /d:sonar.token="${{ secrets.SONAR_TOKEN }}"
```

- to combine code coverage from multiple test projects we can add `--merge-with "coverage.json"` coverlet flag

# Ourchive UI Tests

ui tests for checking if regression bugs have happened. 

## layout

test cases - template method pattern

## configuration 

test runner: nunit
using test runsettings for specific settings (https://docs.nunit.org/articles/vs-test-adapter/Tips-And-Tricks.html)


## how to run

### run via dotnet csproj/sln
`dotnet test ui-tests.csproj -s:dev.runsettings`

with filtering:
`dotnet test ui-tests.csproj -s:dev.runsettings --filter "FullyQualifiedName~CanLoginAndLogout"
`

### github actions runner

currently overriding all runsettings via cli, using github secrets/vars

${{ vars.webAppUrl }}
${{ vars.runHeadless }}
${{ secrets.webAppUserName }}
${{ secrets.webAppPassword }}
`-- TestRunParameters.Parameter(name=\"webAppUrl\", value=\"${{ vars.webAppUrl }}")
-- TestRunParameters.Parameter(name=\"webAppUserName\", value=\"${{ secrets.webAppUserName }}")
-- TestRunParameters.Parameter(name=\"webAppPassword\", value=\"${{ secrets.webAppPassword }}")
-- TestRunParameters.Parameter(name=\"runHeadless\", value=\"${{ vars.runHeadless }}")`

### azure ci build pipeline

### test runner

dotnet test path/to/your/project.csproj --logger:nunit;LogFileName=TestResults.xml -- NUnit.ShowInternalProperties=true
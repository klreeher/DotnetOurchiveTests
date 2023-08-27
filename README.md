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


dotnet pack -c Release
dotnet nuget push bin/Release/NetCoreCQRS.3.0.1.nupkg -k oy2glaf2nvi7eqoybrsqfn7ynml5zjnejjtojznxzd6zrq -s https://api.nuget.org/v3/index.json

pause
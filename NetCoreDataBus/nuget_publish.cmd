dotnet pack -c Release
dotnet nuget push bin/Release/NetCoreDataBus.2.2.0.nupkg -s http://92.53.80.148:5555/v3/index.json -k btf-nuget-server-api-key

pause
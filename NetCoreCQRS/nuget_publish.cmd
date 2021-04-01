dotnet pack -c Release
dotnet nuget push bin/Release/NetCoreCQRS.3.0.1.1.nupkg -k oy2aek6r2hy22l2wi6yaxpjm5smy6s2udfg65ocscpajcy -s https://api.nuget.org/v3/index.json

pause
dotnet pack -c Release
dotnet nuget push bin/Release/NetCoreDataBus.2.0.3.nupkg -k oy2ftcuea4qichnsk2yawfx4hkdagzienbq2yp2j3sdwsy -s https://api.nuget.org/v3/index.json

pause
dotnet pack -c Release
dotnet nuget push bin/Release/NetCoreDataBus.4.0.0.5-rc2.nupkg -k oy2ftcuea4qichnsk2yawfx4hkdagzienbq2yp2j3sdwsy -s https://api.nuget.org/v3/index.json

pause
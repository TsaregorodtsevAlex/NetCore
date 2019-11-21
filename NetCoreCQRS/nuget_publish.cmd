dotnet pack -c Release
dotnet nuget push bin/Release/NetCoreCQRS.4.0.0.1-rc1.nupkg -k oy2ftcuea4qichnsk2yawfx4hkdagzienbq2yp2j3sdwsy -s https://api.nuget.org/v3/index.json

pause
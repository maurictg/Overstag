# This script serves as a pre-build script

# PLEASE SET THESE VARIABLES FIRST!!!!
dotnet_version=3.1

#0. Build Overstag
cd ../Overstag
dotnet build

#1. Copy binaries
cp libwkhtmltox.so "bin/Debug/netcoreapp$dotnet_version/libwkhtmltox.so"
cp libwkhtmltox.dll "bin/Debug/netcoreapp$dotnet_version/libwkhtmltox.dll"

#2. Enjoy!
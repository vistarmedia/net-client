xbuild VistarClient/VistarClient/VistarClient.csproj /property:Configuration=Release
cd MonoDocs
./generate_docs.sh
cd ..

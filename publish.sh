rm -f ./bin/Release/net7.0/win-x64/publish/*.zip
dotnet clean
dotnet publish --configuration Release --runtime win-x64 --self-contained false --no-dependencies
cd ./bin/Release/net7.0/win-x64/publish/
zip -r9m publish-$(date +%Y%m%d-%H%M%S)-net7-react.zip .
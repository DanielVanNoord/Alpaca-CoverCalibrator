dotnet publish -c Release -r linux-arm --self-contained true /p:PublishTrimmed=true /p:PublishReadyToRun=true /p:PublishReadyToRunShowWarnings=true -o ./bin/Alpaca.CoverCalibrator.linux-armhf
dotnet publish -c Release -r linux-arm64 --self-contained true /p:PublishTrimmed=true /p:PublishReadyToRun=true /p:PublishReadyToRunShowWarnings=true -o ./bin/Alpaca.CoverCalibrator.linux-aarch64
dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishTrimmed=true /p:PublishReadyToRun=true /p:PublishReadyToRunShowWarnings=true -o ./bin/Alpaca.CoverCalibrator.linux-x64

cd bin

tar -cJf Alpaca.CoverCalibrator.linux-x64.tar.xz Alpaca.CoverCalibrator.linux-x64/
tar -cJf Alpaca.CoverCalibrator.linux-aarch64.tar.xz Alpaca.CoverCalibrator.linux-aarch64/
tar -cJf Alpaca.CoverCalibrator.linux-armhf.tar.xz Alpaca.CoverCalibrator.linux-armhf/
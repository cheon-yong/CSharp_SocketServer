

protoc.exe -I=./ --csharp_out=./ ./Protocol.proto ./Enum.proto ./Struct.proto

IF ERRORLEVEL 1 PAUSE
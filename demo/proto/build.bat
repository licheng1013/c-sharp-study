echo "compile proto to c#"
cmd /c "protoc.exe  *.proto  --csharp_out=../dto/"
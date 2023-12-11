color B

del  .PublishFiles\*.*   /s /q

dotnet restore

dotnet build

cd WRIMS.Api

dotnet publish -o ..\WRIMS.Api\bin\Debug\netcoreapp3.1\

md ..\.PublishFiles

xcopy ..\WRIMS.Api\bin\Debug\netcoreapp3.1\*.* ..\.PublishFiles\ /s /e 

echo "Successfully!!!! ^ please see the file .PublishFiles"

cmd
echo 'test';
export PATH="$PATH:/root/.dotnet/tools"
pwd
ls -las

dotnet ef database update -p Fixit.Persistance --startup-project Fixit.WebApi
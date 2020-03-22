create solution: dotnet new sln

create project: dotnet new classlib -n Domain          dotnet new webapi -n API

add project to solution: dotnet sln add Domain/

list projects in solution: dotnet sln list

add porject Domain as reference for project Application: cd Application/      dotnet add reference ../Domain/

add package to project App: dotnet add App package Microsoft.Azure.Kusto.Language

add EF tool: dotnet tool install --global dotnet-ef

DB migration: dotnet ef migrations add InitalCreate -p Persistence/ -s API/
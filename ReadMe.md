create solution: dotnet new sln

create project: dotnet new classlib -n Domain          dotnet new webapi -n API

add project to solution: dotnet sln add Domain/

list projects in solution: dotnet sln list

add porject Domain as reference for project Application: cd Application/      dotnet add reference ../Domain/

add package to project App: dotnet add App package Microsoft.Azure.Kusto.Language

add EF tool: dotnet tool install --global dotnet-ef

DB migration code generation: dotnet ef migrations add InitalCreate -p Persistence/ -s API/

update DB: dotnet ef database update    (这个项目中没有用这种方法，而是用code的方式，见Program.cs)

drop DB: dotnet ef database drop -p Persistence -s API

run: dotnet run -p API/

watch run: cd API/             dotnet watch run


两种validation的方式，一种是annotation，一种是Fluent validation（middleware）


---------------
dotnet user-secrets init


---------------
值得学习：
1. migration的方式
2. validation的方式
3. exception的方式
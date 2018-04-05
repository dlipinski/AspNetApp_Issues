rm *.db
rm Data/Migrations/*
rm Migrations/*
dotnet ef migrations add InitialCreate
dotnet ef database update
rm Data/Migrations/*
rm Migrations/*


#restore
dotnet restore
#IssueType
dotnet aspnet-codegenerator controller -name IssueTypeController -m IssueType -dc IssueManagerContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries 
#Issue
dotnet aspnet-codegenerator controller -name IssueController -m Issue -dc IssueManagerContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries 
#Config
dotnet aspnet-codegenerator controller -name ConfigController -m Config -dc IssueManagerContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries 
#ApplicationUser
dotnet aspnet-codegenerator controller -name ApplicationUserController -m ApplicationUser -dc IssueManagerContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
#UserStartView
dotnet aspnet-codegenerator controller -name UserStartViewController -m UserStartView -dc IssueManagerContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries 
#ApplicationUser
#dotnet aspnet-codegenerator controller -name ApplicationUserController -m ApplicationUser -dc IssueManagerContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries 

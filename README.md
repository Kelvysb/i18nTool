# i18nTool
Command line tool for translate i18n json files using Google translate API.

Install:
  
  ```
  dotnet tool install --global i18nTool
  ```
  
  * Requires .Net core 2.2 : https://dotnet.microsoft.com/download/dotnet-core/2.2
  * It may be nescessary to add the 'dotnet tools' folder to the path environment variable in windows.
  * in windows the dotnet tools folder it's '%USERPROFILE%\.dotnet\tools'
  
 Usage:
 
 ```
Translate:
     i18ntool -t <Origin i18n file path> <Target language (example pt-br)>

List all the keys and values from a language file:
     i18ntool -l <i18n folder path> <language (example en-us)>

Get current dir:
     i18ntool -env     

Program Version:
     i18ntool -v     

Help:
     i18ntool -h      
```

Example:

 ```
i18ntool -t ./locales/en-us.json pt-br
i18ntool -l ./locales pt-br
```

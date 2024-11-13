# Shift Logger
Set the APP_HOST in the ShiftLoggerUI/appsettings.json to the URL of wherever the HOST is being ran and chanege the database in appsettings.json for the ShiftLoggerAPI to whatever database you will use.

## Running:
- Run the API first
    ```
    dotnet run --project .\ShiftLoggerAPI\ --launch-profile https
    ```
- Then run the UI
    ```
    dotnet run --project .\ShiftLoggerUI\
    ```
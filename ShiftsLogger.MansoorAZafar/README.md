# Shift Logger

Set the `APP_HOST` in `ShiftLoggerUI/appsettings.json` to the URL of wherever the API host is running. Also, update the database configuration in `ShiftLoggerAPI/appsettings.json` to specify the database you will use.

## Running

- Run the API first:

    ```bash
    dotnet run --project ./ShiftLoggerAPI/ --launch-profile https
    ```

- Then run the UI:

    ```bash
    dotnet run --project ./ShiftLoggerUI/
    ```

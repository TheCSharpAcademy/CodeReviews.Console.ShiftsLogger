Build Instructions:
Please mark both ShiftLogger console app project and ShiftLoggerAPI webApi project as the startup projects.
rightclick on any project and go to configure startup projects.. --> select multiple projects and select acion value of both projects to 'start'.

in ShiftLogger console app project,  the API BaseURL is in IMyHttpClient.cs, Please change the BaseURL to the appropriate localhost port while running.
in ShiftLoggerAPI WebAPI project. the connection string for DB is retrieved from appsettings.Json change the connection string to your appropriate server before running.

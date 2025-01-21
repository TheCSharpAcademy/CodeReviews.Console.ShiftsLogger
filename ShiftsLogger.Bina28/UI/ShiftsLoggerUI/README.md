
# ShiftsLogger project

This project consists of two main applications:

1. **API Application**: Handles backend logic, data management, 
and database communication.
2. **UI Application**: A console-based interface for interacting 
with the API and managing shifts.

---

## API Application

The API application is responsible for the following core functionalities:

- Adding new shifts.
- Updating existing shifts.
- Removing shifts.
- Fetching all shifts.
- Fetching a specific shift by its ID.

### Key Features

- Exposes RESTful endpoints for all operations.
- Ensures data integrity and validation.
- Provides a scalable backend for managing shifts effectively.

### Configuration

Before running the API application, ensure you configure the database 
connection string in the `appsettings.json` file.

1. Locate the `appsettings.json` file in the root of the API application.
2. Update the `ConnectionStrings` section to match your database configuration:

```json

  {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YourServerName;Database=YourDbName"
     }
   }

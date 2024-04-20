using ShiftsLoggerUI;

AppEngine app = new();

while (app.IsRunning)
{
  await app.MainMenu();
}
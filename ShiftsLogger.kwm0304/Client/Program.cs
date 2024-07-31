

namespace Client;

public class Program
{
  static async Task Main()
  {
    using HttpClient client = new();
    while(true)
    {
      AppSession session = new(client);
      await session.OnStart();
    }
  }
}

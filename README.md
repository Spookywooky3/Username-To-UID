[![Build status](https://ci.appveyor.com/api/projects/status/kjlcpsas7604ucnr?svg=true)](https://ci.appveyor.com/project/Spookywooky3/username-to-uid)
# Username-To-UID
Retrieves the UID from the users username and discriminator
## Usage
```csharp
using System;
using GetUserIdFromUsername;

namespace Test
{
    class Program
    {
        static void Main()
        {
            using (DiscordApi api = new DiscordApi("<token for bot that is inside the server>"))
            {
                Console.WriteLine(api.GetIdFromUserName(guildId, "user#1337")
                .ConfigureAwait(false).GetAwaiter().GetResult().ToString());
                
                // await api.GetIdFromUserName(guildId, "user#1337");   
            }
            Console.ReadKey();
        }
    }
}
```
## Build
Build with .NET Core 3.1



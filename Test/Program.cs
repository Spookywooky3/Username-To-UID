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
                Console.WriteLine(api.GetIdFromUserName(564100801685880834, "user#1337").ConfigureAwait(false).GetAwaiter().GetResult().ToString());
                // await api.GetIdFromUserName(guildId, "user#1337");   
            }
            Console.ReadKey();
        }
    }
}

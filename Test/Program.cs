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
                Console.WriteLine(api.GetIdFromUserName(0, "user#1337")
                .ConfigureAwait(false).GetAwaiter().GetResult().ToString());

                // await api.GetIdFromUserName(guildId, "user#1337");   
            }
            Console.ReadKey();
        }
    }
}

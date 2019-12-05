using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace GetUserIdFromUsername
{
    // Shit naming conventions and kind of messy but cbf lmao
    public class DiscordApi : IDisposable
    {
        private bool disposed = false;

        string botToken { get; set; }
        public DiscordApi(string token)
        {
            botToken = token;
        }

        public async Task<ulong> GetIdFromUserName(ulong guildId, string usr)
        {
            string[] userArray = usr.Split('#');
            string username = userArray[0];
            string discriminator = userArray[1];

            string dataReceived;

            // Change the limit depending on server members and performance, dodgey i know but its better than checking the amount of guild members
            // every time.
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://discordapp.com/api/v6/guilds/" 
                + guildId.ToString() + "/members?limit=500"); 
            req.PreAuthenticate = true;
            req.Headers.Add("Authorization", "Bot " + botToken);
            req.Method = "GET";

            var response = await req.GetResponseAsync();
            using (Stream data = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(data);
                dataReceived = await reader.ReadToEndAsync();
            }
            response.Close();

            var json = await Task.Run(() =>
                JsonConvert.DeserializeObject<List<Wrapper>>(dataReceived)
            );
            var query = await Task.Run(() =>
                json.Single(user => user.discordUser.userName == username && user.discordUser.discriminator == discriminator)
            );

            ulong userId;
            ulong.TryParse(query.discordUser.id, out userId);

            return userId;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                GetIdFromUserName(0, "").Dispose();
            }
            disposed = true;
        }

    }

    class Wrapper
    {
        [JsonProperty("user")]
        public DiscordUser discordUser { get; set; }
        [JsonProperty("roles")]
        public List<object> roleIds { get; set; }
        [JsonProperty("nick")]
        public object nick { get; set; }
        [JsonProperty("joined_at")]
        public DateTime joined_at { get; set; }
        [JsonProperty("premium_since")]
        public object premium_since { get; set; }
        [JsonProperty("deaf")]
        public bool deaf { get; set; }
        [JsonProperty("mute")]
        public bool mute { get; set; }
    }

    class DiscordUser
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("username")]
        public string userName { get; set; }
        [JsonProperty("avatar")]
        public string avatar { get; set; }
        [JsonProperty("discriminator")]
        public string discriminator { get; set; }
        [JsonProperty("bot")]
        public bool bot { get; set; }
    }
}

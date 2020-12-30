using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Discord;
using Discord.Gateway;
using Discord.Commands;
using Discord.Media;

namespace discord_selfbot
{
    [Command("Server")]
    public class ServerDumperCommand : CommandBase
    {
        [Parameter("server_id")]
        public ulong server_id { get; private set; }

        public override void Execute()
        {
            if (this.Client.User.Id == this.Message.Author.User.Id)
                this.Message.Delete();
            else
                return;
            Discord.DiscordGuild server = Client.GetGuild(server_id);
            Console.WriteLine("————————ServerDumper————————");
            Program.Log($"Name", server.Name.ToString());
            Program.Log($"Owner", Client.GetUser(server.OwnerId).Username.ToString() + " | " + Client.GetUser(server.OwnerId).Id.ToString());
            Program.Log($"Verification Level", server.VerificationLevel.ToString());
            Program.Log($"Notifications", server.DefaultNotifications.ToString());
            Program.Log($"Nitro Boosts", server.NitroBoosts.ToString());
            Program.Log($"Premium Tier", server.PremiumTier.ToString());
            Console.WriteLine("————————————————————————————");
            Console.WriteLine();
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            if (this.Client.User.Id == this.Message.Author.User.Id)
                this.Message.Delete();
            else
                return;

            if (providedValue == null)
                Message.Channel.SendMessage($"Please provide a value for '{parameterName}'");
        }

    }
}
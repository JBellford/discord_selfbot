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
    [Command("User")]
    public class ProfileDumperCommand : CommandBase
    {
        [Parameter("user_profile")]
        public ulong user_profile { get; private set; }

        public override void Execute()
        {
            if (this.Client.User.Id == this.Message.Author.User.Id)
                this.Message.Delete();
            else
                return;
            Discord.DiscordUser user = Client.GetUser(user_profile);
            Console.WriteLine();
            Console.WriteLine("————————Profile Dump————————");
            Program.Log($"User", user.ToString());
            Program.Log($"UserID", user.Id.ToString());
            Program.Log($"HypeSquad", user.Hypesquad.ToString());
            Program.Log($"Created At", user.CreatedAt.ToString());
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
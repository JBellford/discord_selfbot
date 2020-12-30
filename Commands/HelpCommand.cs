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
    [Command("Help")]
    public class HelpCommand : CommandBase
    {
        public override void Execute()
        {
            if (this.Client.User.Id == this.Message.Author.User.Id)
                this.Message.Delete();
            else
                return; 
            Console.WriteLine();
            Console.WriteLine("————————  Commands  ————————");
            Program.Log($"b.Purge <0 - 500>", "Deletes x amount of messages");
            Program.Log($"b.User <User ID>", "Dumps some profile information on the user id");
            Program.Log($"b.Server <Server ID>", "Dumps some server information on the server id");
            Program.Log($"b.Nitro <on - off>", "Automatically claims nitro gifts");
            Program.Log($"b.Giveaway <on - off>", "Automatically joins giveaways");
            Console.WriteLine("————————————————————————————");
            Console.WriteLine();

        }

    }
}

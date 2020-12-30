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
    [Command("Nitro")]
    public class NitroCommand : CommandBase
    {
        [Parameter("on_off")]
        public string ON_OFF { get; private set; }

        public override void Execute()
        {
            if (this.Client.User.Id == this.Message.Author.User.Id)
                this.Message.Delete();
            else
                return;

            if (ON_OFF == "ON" || ON_OFF == "On" || ON_OFF == "on")
                Program.nitro_grabber = true;
            if (ON_OFF == "OFF" || ON_OFF == "Off" || ON_OFF == "off")
                Program.nitro_grabber = false;

            Program.Log($"{Client.User.ToString()} | Nitro Grabber", Program.nitro_grabber.ToString());
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

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
    [Command("Purge")]
    public class PurgeCommand : CommandBase
    {
        [Parameter("message_count")]
        public int MessageCount { get; private set; }

        public override void Execute()
        {
            if (this.Client.User.Id == this.Message.Author.User.Id)
                this.Message.Delete();
            else
                return;

            Program.Log($"{Client.User} | Started Purge", DateTime.Now.ToString());

            ulong current_message = 0;
            int current_messageNumber = 0;
            List<DiscordMessage> message_list = new List<DiscordMessage>();
            while (true)
            {
                if (current_message == 0)
                    message_list = Client.GetChannelMessages(Message.Channel.Id, new MessageFilters() { BeforeId = this.Message.Id, Limit = 50u }).ToList();
                else
                    message_list = Client.GetChannelMessages(Message.Channel.Id, new MessageFilters() { BeforeId = current_message, Limit = 50u }).ToList();

                foreach (var message in message_list)
                {
                    if (current_messageNumber >= MessageCount)
                    {
                        Program.Log($"{Client.User} | Finished Purge", DateTime.Now.ToString());
                        return;
                    }
                    try
                    {
                        if (message.Type == MessageType.Default && Message.Author.User.Id == Client.User.Id)
                        {
                            message.Delete();
                            current_message = message.Id;
                            current_messageNumber++;
                        }
                    }
                    catch
                    {

                    }
                    Thread.Sleep(450);
                }
                message_list.Clear();
            }
            
        }

        public override void HandleError(string parameterName, string providedValue, Exception exception)
        {
            if (this.Client.User.Id == this.Message.Author.User.Id)
                this.Message.Delete();
            else
                return;

            if (providedValue == null)
                Message.Channel.SendMessage($"Please provide a value between 1-500 for '{parameterName}'");
        }
    }
}

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
    class Program
    {
        public static string token = $"";
        public static bool is_loggedIn = false;

        #region Settings
        public static bool nitro_grabber = true;
        public static bool giveaway_joiner = true;
        #endregion

        static void Main(string[] args)
        {
            if (token == "") {Console.Write($"[+] - Token -> "); token = Console.ReadLine(); } else { Console.WriteLine($"[+] - Token -> {token}"); }

            DiscordSocketClient client = new DiscordSocketClient();

            client.OnLoggedIn += OnLoggedIn;
            client.OnMessageReceived += OnMessageReceived;

            client.CreateCommandHandler("b.");

            client.Login(token);

            while (true)
            {
                Console.Title = $"{DateTime.Now}      |     JBellford selfbot     |      Logged in as : {client.User.Username}";
                Thread.Sleep(450);
            }
        }

        private static void OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
        {
            if (!is_loggedIn)
                return;

            if (nitro_grabber && args.Message.Content.Contains("discord.gift/"))
            {
                string nitro = args.Message.Content;
                try
                {
                    if (nitro.Contains("http"))
                    {
                        nitro = nitro.Replace("https://discord.gift/", "");

                        if (nitro.Contains(" "))
                            nitro = nitro.Split(' ')[0];
                    }
                    else
                    {
                        nitro = nitro.Split('/')[1];

                        if (nitro.Contains(" "))
                           nitro = nitro.Split(' ')[0];

                        Log("Detected Nitro", nitro);
                    }

                    try
                    {
                        client.RedeemGift(nitro);
                        Log("Claimed Nitro", nitro);
                    }
                    catch { }
                }
                catch
                {

                }
            }

            if (giveaway_joiner && args.Message.Embed != null && args.Message.Embed.Description.Contains("React with 🎉 to enter"))
            {
                Thread.Sleep(5000);
                args.Message.AddReaction("🎉");
                Log("Joined Giveaway", client.GetGuild(args.Message.Guild.Id).Name);
            }


        }

        private static void OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            if (!is_loggedIn)
            {
                Console.Clear();
                Console.WriteLine();
                Log($"User", client.User.ToString());
                Log("UserID", client.User.Id.ToString());
                Log("Email", client.User.Email);
                if (client.User.PhoneNumber != null) { Log("Phone", client.User.PhoneNumber); }
                Log("HypeSquad", client.User.Hypesquad.ToString());
                Log("NitroType", client.User.Nitro.ToString());
                Log("Token", client.Token);
                Console.WriteLine();
                Console.WriteLine($"---------- Logger ----------");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                is_loggedIn = true;
            }

        }

        public static void Log(string one, string two)
        {
            Console.ForegroundColor = ConsoleColor.Red;Console.Write("["); Console.ForegroundColor = ConsoleColor.Gray; Console.Write("+");
            Console.ForegroundColor = ConsoleColor.Red; Console.Write("]"); Console.ForegroundColor = ConsoleColor.White; Console.Write(" - "); Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(one); Console.ForegroundColor = ConsoleColor.Red; Console.Write(" -> "); Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(two); 
        }
    }
}

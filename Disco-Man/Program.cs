using System;
using System.Threading.Tasks;
using DSharpPlus;
    
namespace Disco_Man
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().GetAwaiter().GetResult();
        }

        public static async Task Run()
        {
            var discord = new DiscordClient(new DiscordConfig
            {
                AutoReconnect = true,
                DiscordBranch = Branch.Stable,
                LargeThreshold = 250,
                LogLevel = LogLevel.Error,
                Token = "MzM1MTI0MTYxNTkwNTkxNDk5.DEyl8g.f3exnIH2ttsLBkNMon6PFoO1Jx0",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = false
            });

            discord.DebugLogger.LogMessageReceived += (o, e) =>
            {
                Console.WriteLine($"[{e.Timestamp}] [{e.Application}] [{e.Level}] {e.Message}");
            };

            discord.GuildAvailable += e =>
            {
                discord.DebugLogger.LogMessage(LogLevel.Info, "discord bot", $"Guild available: {e.Guild.Name}", DateTime.Now);
                return Task.Delay(0);
            };

            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower() == "ping")
                    await e.Message.RespondAsync("pong");
            };



            await discord.ConnectAsync();

            await Task.Delay(-1);
        }
    }
}

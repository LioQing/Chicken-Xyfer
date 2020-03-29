using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Chicken_Xyfer;

namespace Chicken_Xyfer
{
    public class Program
    {
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        string TOKEN = File.ReadAllText(@"D:\SuperEGG\Discord\Bots\Chicken-Xyfer\Chicken-Xyfer\Chicken-Xyfer\token.gitignore");

        private string PREFIX = "cx|";
        public static Color EMBEDCOLOR = new Color(0xC22947);

        private DiscordSocketClient _client;

        private RPG.RPG rpgGame = new RPG.RPG();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            await _client.LoginAsync(TokenType.Bot, TOKEN);
            await _client.StartAsync();

            _client.MessageReceived += MessageStartUp;

            await Task.Delay(-1);
        }

        private async Task MessageStartUp(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;

            int argPos = 0;
            if (msg.HasStringPrefix(PREFIX, ref argPos))
            {
                MainMessage(msg);
            }
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        private void MainMessage(SocketUserMessage msg)
        {
            string[] lowArgs = msg.Content.ToLower().Substring(PREFIX.Length).Split(' ');
            string[] args = msg.Content.Substring(PREFIX.Length).Split(' ');

            MessageBundle msgbd = new MessageBundle(msg, lowArgs, args);

            if (lowArgs.Length > 0)
            {
                if (lowArgs[0] == "help") HelpEmbed(msgbd);
                else if (lowArgs[0] == "rpg") rpgGame.RPGMain(msgbd);
            }
        }

        private async void HelpEmbed(MessageBundle msgbd)
        {
            SocketUserMessage msg;
            string[] lowArgs, args;

            msgbd.UnBundle(out msg, out lowArgs, out args);

            if (args.Length > 1)
            {
                if (lowArgs[1] == "rpg")
                {
                    var embed = new EmbedBuilder
                    {
                        Title = "Help - RPG",
                        Description = "Here are all the info/commands for RPG!\nPrefix: 'cx|rpg'",
                        Color = EMBEDCOLOR,
                    };
                    embed
                        .AddField("General info",
@"**Creature type** - Creature type includes player/plyr and monster/mstr.
**Name** - Names cannot include space.
**Info name** - Info name includes damage/dmg, damage_range/dmgrnge, health/hp, defence/def, experience/exp, level/lvl.

**Remarks**:
    All commands are not case-sensitive, all names are case-sensitive.")
                        .AddField("Commands",
@"**spawn/spwn [creature type] [OPTIONAL: name]** - Spawn a creature with a name (default creature type + a number).
**attack/atk [creature type] [creature name]** - Attack a monster.

**board/brd** - Show all creatures on board.

**player/plyr [OPTIONAL: player name] [OPTIONAL: info name]** - Get info (default all info) of a player (default own character).
**exp_chart/xpchrt** - Get the level/exp chart.");

                    await msg.Channel.SendMessageAsync(embed: embed.Build());
                }
            }
            else
            {
                var embed = new EmbedBuilder
                {
                    Title = "Help",
                    Description = "Here are all the commands you can use!\nPrefix: 'cx|'",
                    Color = EMBEDCOLOR,
                };
                embed
                    .AddField("Commands",
@"**help** - Get general help info.
**help [other command]** - Get help info on a specific command.
**rpg** - Play a round of Role Play Game(RPG).");

                await msg.Channel.SendMessageAsync(embed: embed.Build());
            }
            
        }
    }
}

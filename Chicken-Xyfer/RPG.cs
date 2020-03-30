using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Chicken_Xyfer.RPG.Components;
using Chicken_Xyfer.RPG.Entity;
using Chicken_Xyfer.RPG.Modules;

namespace Chicken_Xyfer.RPG
{
    class RPG
    {
        EmbedBuilder output = new EmbedBuilder();

        IList<string> playerInfoList = new List<string>()
        {
            "damage", "dmg",
            "damage_range", "dmgrnge",
            "health", "hp",
            "defence", "def",
            "experience", "exp",
            "level", "lvl"
        };

        IList<Player> players = new List<Player>();
        IList<Monster> monsters = new List<Monster>();

        SocketUserMessage msg;
        string[] lowArgs, args;

        //Main
        public async void RPGMain(MessageBundle msgbd)
        {
            msgbd.UnBundle(out msg, out lowArgs, out args);

            output = new EmbedBuilder
            {
                Title = "RPG",
                Description = "Sample",
                Color = Program.EMBEDCOLOR,
            };

            if (args.Length > 1)
            {
                if (lowArgs[1] == "spawn" || lowArgs[1] == "spwn")
                {
                    Spawn();
                } 
                else if (lowArgs[1] == "kill")
                {
                    Kill();
                }
                else if (lowArgs[1] == "board" || lowArgs[1] == "brd")
                {
                    Board();
                }
                else if (lowArgs[1] == "player" || lowArgs[1] == "plyr")
                {
                    PlayerInfo();
                }
                else if (lowArgs[1] == "exp_chart" || lowArgs[1] == "xpchrt")
                {
                    ExpChart();
                }
                else if (lowArgs[1] == "attack" || lowArgs[1] == "atk")
                {
                    if (UserHasPlayer(msg.Author))
                    {
                        Attack();
                    }
                }
            }

            if (output.Description != "Sample")
            {
                await msg.Channel.SendMessageAsync(embed: output.Build());
            }
        }

        private bool UserHasPlayer(IUser user)
        {
            return Player.GetByUserInList(user, players) != null;
        }

        //Spawn Character
        private void Spawn()
        {
            if (args.Length > 2)
            {
                if (lowArgs[2] == "player" || lowArgs[2] == "plyr")
                {
                    if (args.Length > 3)
                    {
                        players.Add(new Player(args[3], msg.Author));
                    }
                    else
                    {
                        players.Add(new Player($"Player{players.Count()}", msg.Author));
                    }
                    output
                        .WithTitle(players.ElementAt(players.Count() - 1).GetComponent<AttributesComponent>().Name)
                        .WithDescription($"New Player Character Spawned");
                }
                else if (lowArgs[2] == "monster" || lowArgs[2] == "mstr")
                {
                    if (args.Length > 3)
                    {
                        monsters.Add(new Monster(args[3]));
                    }
                    else
                    {
                        monsters.Add(new Monster($"Monster{monsters.Count()}"));
                    }
                    output
                        .WithTitle(monsters.ElementAt(monsters.Count() - 1).GetComponent<AttributesComponent>().Name)
                        .WithDescription($"New Monster Character Spawned");
                }
            }
        }

        //Kill Character
        private void Kill()
        {
            if (args.Length > 2)
            {
                if (lowArgs[2] == "player" || lowArgs[2] == "plyr")
                {
                    if (args.Length > 3)
                    {
                        Player player = BaseEntity.GetByNameInList(args[3], players);
                        if (player != null)
                        {
                            player.GetComponent<HealthComponent>().Hp = 0;
                            if (player.GetComponent<HealthComponent>().IsDead)
                            {
                                output.WithDescription($"Player \"{args[3]}\" is killed.");
                            }
                        }
                    }
                }
                else if (lowArgs[2] == "monster" || lowArgs[2] == "mstr")
                {
                    if (args.Length > 3)
                    {
                        Monster monster = BaseEntity.GetByNameInList(args[3], monsters);
                        if (monster != null)
                        {
                            monster.GetComponent<HealthComponent>().Hp = 0;
                            if (monster.GetComponent<HealthComponent>().IsDead)
                            {
                                output.WithDescription($"Monster \"{args[3]}\" is killed.");
                            }
                        }
                    }
                }
            }
        }

        //Player Info Get and Set
        private void PlayerInfo()
        {
            if (args.Length > 2)
            {
                if (UserHasPlayer(msg.Author))
                {
                    if (playerInfoList.Contains(lowArgs[2]))
                    {
                        PlayerInfo(msg.Author, 2);
                    }
                }
                else if (args.Length > 3)
                {
                    IUser user = BaseEntity.GetByNameInList(args[2], players).User;
                    PlayerInfo(user, 3);
                }
                else if (UserHasPlayer(msg.Author))
                {
                    IUser user = BaseEntity.GetByNameInList(args[2], players).User;
                    PlayerFullInfo(user);
                }
            }
            else if(UserHasPlayer(msg.Author))
            {
                PlayerFullInfo(msg.Author);
            }
        }

        private void PlayerFullInfo(IUser user)
        {
            Player player = Player.GetByUserInList(user, players);
            output
                .WithTitle(Player.GetByUserInList(user, players).GetComponent<AttributesComponent>().Name)
                .WithDescription("")
                .AddField("Name", Player.GetByUserInList(user, players).GetComponent<AttributesComponent>().Name)
                .AddField("User", Player.GetByUserInList(user, players).User)
                .AddField("Info Name", "Damage\nDamage Range\nHP\nDefence\nExp\nLevel", true)
                .AddField("Value",
                player.GetComponent<ExpComponent>().Exp + "\\" + Player.GetByUserInList(user, players).GetComponent<ExpComponent>().GetNextLvlExp() + "\n" +
                player.GetComponent<ExpComponent>().Lvl + "\n\n" +
                player.GetComponent<AttackComponent>().Dmg + "\n" +
                player.GetComponent<AttackComponent>().DmgRange + "\n" +
                player.GetComponent<HealthComponent>().Hp + "\n" +
                player.GetComponent<HealthComponent>().Def
                , true);
        }

        private void PlayerInfo(IUser user, int argPos)
        {
            if (lowArgs[argPos] == playerInfoList[0] || lowArgs[argPos] == playerInfoList[1]) //Dmg
            {
                if (args.Length > argPos + 1)
                {
                    if (int.TryParse(lowArgs[argPos + 1], out _))
                    {
                        Player.GetByUserInList(user, players).GetComponent<AttackComponent>().Dmg = Int32.Parse(lowArgs[3]);
                    }
                }
                output
                    .WithTitle($"Player \"{Player.GetByUserInList(user, players).GetComponent<AttributesComponent>().Name}\"")
                    .WithDescription($"Damage: {Player.GetByUserInList(user, players).GetComponent<AttackComponent>().Dmg}");
            }
            else if (lowArgs[argPos] == playerInfoList[2] || lowArgs[argPos] == playerInfoList[3]) //DmgRange
            {
                if (args.Length > argPos + 1)
                {
                    if (int.TryParse(lowArgs[argPos + 1], out _))
                    {
                        Player.GetByUserInList(user, players).GetComponent<AttackComponent>().DmgRange = Int32.Parse(lowArgs[3]);
                    }
                }
                output
                    .WithTitle($"Player \"{Player.GetByUserInList(user, players).GetComponent<AttributesComponent>().Name}\"")
                    .WithDescription($"Damage Range: {Player.GetByUserInList(user, players).GetComponent<AttackComponent>().DmgRange}");
            }
            else if (lowArgs[argPos] == playerInfoList[4] || lowArgs[argPos] == playerInfoList[5]) //HP
            {
                if (args.Length > argPos + 1)
                {
                    if (int.TryParse(lowArgs[argPos + 1], out _))
                    {
                        Player.GetByUserInList(user, players).GetComponent<HealthComponent>().Hp = Int32.Parse(lowArgs[3]);
                    }
                }
                output
                    .WithTitle($"Player \"{Player.GetByUserInList(user, players).GetComponent<AttributesComponent>().Name}\"")
                    .WithDescription($"HP: {Player.GetByUserInList(user, players).GetComponent<HealthComponent>().Hp}");
            }
            else if (lowArgs[argPos] == playerInfoList[6] || lowArgs[argPos] == playerInfoList[7]) //Def
            {
                if (args.Length > argPos + 1)
                {
                    if (int.TryParse(lowArgs[argPos + 1], out _))
                    {
                        Player.GetByUserInList(user, players).GetComponent<HealthComponent>().Def = Int32.Parse(lowArgs[3]);
                    }
                }
                output
                    .WithTitle($"Player \"{Player.GetByUserInList(user, players).GetComponent<AttributesComponent>().Name}\"")
                    .WithDescription($"Defence: {Player.GetByUserInList(user, players).GetComponent<HealthComponent>().Def}");
            }
            else if (lowArgs[argPos] == playerInfoList[8] || lowArgs[argPos] == playerInfoList[9]) //Exp
            {
                string LvlUpMsg = "";

                if (args.Length > argPos + 1)
                {
                    if (int.TryParse(lowArgs[argPos + 1], out _))
                    {
                        int LvlUp = Player.GetByUserInList(user, players).GetComponent<ExpComponent>().SetExp(Int32.Parse(lowArgs[3]));
                        if (LvlUp != 0)
                        {
                            LvlUpMsg = $" (Level Up {Player.GetByUserInList(user, players).GetComponent<ExpComponent>().Lvl - LvlUp} => {Player.GetByUserInList(user, players).GetComponent<ExpComponent>().Lvl})";
                        }
                    }
                }
                output
                    .WithTitle($"Player \"{Player.GetByUserInList(user, players).GetComponent<AttributesComponent>().Name}\"")
                    .WithDescription($"Exp: {Player.GetByUserInList(user, players).GetComponent<ExpComponent>().Exp} / {Player.GetByUserInList(user, players).GetComponent<ExpComponent>().GetNextLvlExp()}" + LvlUpMsg);
            }
            else if (lowArgs[argPos] == playerInfoList[10] || lowArgs[argPos] == playerInfoList[11]) //LvL
            {
                if (args.Length > argPos + 1)
                {
                    if (int.TryParse(lowArgs[argPos + 1], out _))
                    {
                        Player.GetByUserInList(user, players).GetComponent<ExpComponent>().Exp = Player.GetByUserInList(user, players).GetComponent<ExpComponent>().GetRequiredExpToLvl(Int32.Parse(lowArgs[3]));
                    }
                }
                output
                    .WithTitle($"Player \"{Player.GetByUserInList(user, players).GetComponent<AttributesComponent>().Name}\"")
                    .WithDescription($"Level: {Player.GetByUserInList(user, players).GetComponent<ExpComponent>().Lvl}");
            }
        }



        //Show all characters on board
        private void Board()
        {
            if (args.Length > 2)
            {
                if (lowArgs[2] == "monster" || lowArgs[2] == "mstr")
                {
                    output.WithDescription("Monsters: " + GeneralModules.GetCharacterNames(monsters));
                }
                else if (lowArgs[2] == "player" || lowArgs[2] == "plyr")
                {
                    output.WithDescription("Players: " + GeneralModules.GetCharacterNames(players));
                }
            }
            else
            {
                output
                    .AddField("Players", GeneralModules.GetCharacterNames(players), true)
                    .AddField("Monsters", GeneralModules.GetCharacterNames(monsters), true)
                    .WithDescription("")
                    .WithTitle("");
            }
        }

        //Attack
        private void Attack()
        {
            if (args.Length > 2)
            {
                Monster target = BaseEntity.GetByNameInList(args[2], monsters);
                if (target != null)
                {
                    Player attacker = Player.GetByUserInList(msg.Author, players);
                    int attackDmg = attacker.GetComponent<AttackComponent>().Attack();
                    int receivedDmg = target.GetComponent<HealthComponent>().Attacked(attackDmg);
                    output
                        .WithTitle($"Player \"{attacker.GetComponent<AttributesComponent>().Name}\" attacked Monster \"{target.GetComponent<AttributesComponent>().Name}\"")
                        .WithDescription($"\"{target.GetComponent<AttributesComponent>().Name}\" HP: {target.GetComponent<HealthComponent>().Hp + receivedDmg} => {target.GetComponent<HealthComponent>().Hp} ({receivedDmg} Damage)");
                }
            }
        }

        //Get Level/Exp Chart
        private void ExpChart()
        {
            string lvlStr = "", expStr = "";

            for (int lvl = 0; lvl <= ExpComponent.MAX_LVL; ++lvl)
            {
                lvlStr += lvl + "\n";
                expStr += ExpComponent.GetExpByLvl(lvl) + "\n";
            }

            expStr = expStr.Substring(0, expStr.Length - 1);
            lvlStr = lvlStr.Substring(0, lvlStr.Length - 1);

            output
                .WithDescription("A level-exp chart.")
                .AddField("Level", lvlStr, true)
                .AddField("Exp", expStr, true);
        }
    }
}

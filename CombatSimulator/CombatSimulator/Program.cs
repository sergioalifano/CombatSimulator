using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulator
{
    class Program
    {
        static int playerEnergy = 100;
        static int dragonEnergy = 200;
        static Random gnr = new Random();

        static void Main(string[] args)
        {
            
            Console.SetWindowSize(Console.WindowWidth, Console.LargestWindowHeight);
            bool play = true;
            string userAttackResult = string.Empty;
            string dragonAttackResult = string.Empty;
            string input = string.Empty;

            DisplayBanner();
            DisplayDragon();
            DisplayStory();
            

            while (play)
            {
                Console.Clear();
                DisplayBanner();

                Console.WriteLine("Choose your attack: ");
                Console.WriteLine("1. Attack with a sword\n2. Use the magic and throw a Fireball!\n3. Use the medication\n\n");

                //display player energy level
                Console.Write(ShowEnergyLevel(playerEnergy).PadRight(40));
                Console.ResetColor();

                //display dragon energy level 
                Console.WriteLine(ShowEnergyLevel(dragonEnergy));
                Console.ResetColor();

                //display the result of your attack
                Console.Write(userAttackResult.PadRight(40));
                //display the result of the dragon attack
                Console.WriteLine(dragonAttackResult);

                //if the input is a valid option
                if (validateInput(input = Console.ReadLine()))
                {
                    switch (int.Parse(input))
                    {
                        case 1: userAttackResult = SwordAttack();
                            break;
                        case 2: userAttackResult = MagicAttack();
                            break;
                        case 3: userAttackResult = HealEnergy();
                            break;
                    }
                }
                else
                { 
                    //if the input is not a valid option
                    userAttackResult = "Next time choose a valid option".PadRight(40);
                }

                if (dragonEnergy <= 0)
                {
                    Console.WriteLine("You defeat the dragon");
                    play = false;
                }

                else 
                {
                    dragonAttackResult = DragonAttack();
                   // Console.WriteLine(dragonAttackResult);
                    if (playerEnergy <= 0)
                    {
                        Console.WriteLine("The Dragon killed you");
                        play = false;
                    }
                }
               
                
            }
        }

        /// <summary>
        /// Attack with a sword
        /// </summary>
        static string SwordAttack()
        {
            int damage;
            //if it's in the 70% of the positive attack 
            if (gnr.Next(0,11) <= 7)
            {
                //decrement dragon energy
                damage= gnr.Next(20, 36);
                dragonEnergy -= damage;
                return "You hit the dragon for " + damage + "damages";
            }
            return "You missed!!";
        }

        static string MagicAttack()
        {
            int damage;
            damage = gnr.Next(10, 15);
            dragonEnergy -= damage;

            return "You took " + damage + " HP off the Dragon!";
        }

        static string HealEnergy()
        {
            int energyHealed;
            energyHealed = gnr.Next(10, 21);
            playerEnergy += energyHealed;

            return "The medication gave you " + energyHealed + "HP";
        }

        static string DragonAttack()
        {
            int damage;

            //if the dragon hit
            if (gnr.Next(0, 11) <= 8)
            {
                damage = gnr.Next(5, 16);
                playerEnergy -= damage;
                return "Ahahah " + damage + "HP less for you!!";
            }
            return "The shield won't save you next time...";
        }

        static bool validateInput(string input)
        {
            int inputNumber;
            //if the input is a number
            if (int.TryParse(input, out inputNumber))
            {
                if (inputNumber > 0 && inputNumber < 4)
                {
                    return true;
                }
            }
            return false;
        }

        static string ShowEnergyLevel(int energy)
        {
            string energyLevel = "ENERGY STATUS ";
  
                if (energy > 80)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        energyLevel += "██";
                    }
                }
                else if (energy > 50)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        energyLevel += "██";
                    }
                }
                else if (energy > 25)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        energyLevel += "██";
                    }

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    energyLevel += "██";
                }
                return energyLevel;
        }

        static void DisplayBanner(){
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(@" ██████╗ ██████╗ ███╗   ███╗██████╗  █████╗ ████████╗                    
██╔════╝██╔═══██╗████╗ ████║██╔══██╗██╔══██╗╚══██╔══╝                    
██║     ██║   ██║██╔████╔██║██████╔╝███████║   ██║                       
██║     ██║   ██║██║╚██╔╝██║██╔══██╗██╔══██║   ██║                       
╚██████╗╚██████╔╝██║ ╚═╝ ██║██████╔╝██║  ██║   ██║                       
 ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚═════╝ ╚═╝  ╚═╝   ╚═╝                       
                                                                         
███████╗██╗███╗   ███╗██╗   ██╗██╗      █████╗ ████████╗ ██████╗ ██████╗ 
██╔════╝██║████╗ ████║██║   ██║██║     ██╔══██╗╚══██╔══╝██╔═══██╗██╔══██╗
███████╗██║██╔████╔██║██║   ██║██║     ███████║   ██║   ██║   ██║██████╔╝
╚════██║██║██║╚██╔╝██║██║   ██║██║     ██╔══██║   ██║   ██║   ██║██╔══██╗
███████║██║██║ ╚═╝ ██║╚██████╔╝███████╗██║  ██║   ██║   ╚██████╔╝██║  ██║
╚══════╝╚═╝╚═╝     ╚═╝ ╚═════╝ ╚══════╝╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝
                                                                         ");
            Console.ResetColor();
        }

        static void DisplayDragon()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
                  -.            \_|//     |||\\  ~~~~~~::::... /~
               ___-==_       _-~o~  \/    |||  \\            _/~~-
       __---~~~.==~||\=_    -_--~/_-~|-   |\\   \\        _/~
   _-~~     .=~    |  \\-_    '-~7  /-   /  ||    \      /
  ~       .~       |   \\ -_    /  /-   /   ||      \   /
/  ____  /         |     \\ ~-_/  /|- _/   .||       \ /
|~~    ~~|--~~~~--_ \     ~==-/   | \~--===~~        .\
         '         ~-|      /|    |-~\~~       __--~~
                     |-~~-_/ |    |   ~\_   _-~            /\
                          /  \     \__   \/~                \__
                      _--~ _/ | .-~~____--~-/                  ~~==.
                     ((->/~   '.|||' -_|    ~~-/ ,              . _||
                               -_     ~\      ~~---l__i__i__i--~~_/
                                _-~-__   ~)  \--______________--~~
                              //.-~~~-~_--~- |-------~~~~~~~~
                                     //.-~~~--\");
            Console.ResetColor();
            Console.WriteLine();
        }

        static void DisplayStory(){
            string text=@"You are a brave knight and you must save the princess trapped in the castle. 
To get to her you must first defeat the terrible dragon that defends the castle doors.

You have two possible weapons: sword and fireball. 
To heal yourself you can use the magic potion that magician Merlin gave you before leaving.

The sword is the weapon that inflicts more damage but it hits only 70% of the times.
The fireball always strikes but does least damage to the dragon.
The magic potion gives you energy between 10 and 20 hp.

You have initially 100 HP
The dragon has 200 hp

GOOD LUCK!!!";

            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(10);
            }
            Console.WriteLine("\n\nPress any key to begin");
            
            Console.ReadLine();
        }
    
    }
}

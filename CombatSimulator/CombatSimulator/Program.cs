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
            //Change the console Height to fit the description of the game
            Console.SetWindowSize(Console.WindowWidth, Console.LargestWindowHeight);

            bool play = true;
            string userAttackResult = string.Empty;
            string dragonAttackResult = string.Empty;
            string input = string.Empty;

            //print out the logo of the game
            DisplayBanner();
            DisplayDragon();

            string name = DisplayStory();

            while (play)
            {
                Console.Clear();
                DisplayBanner();

                Console.WriteLine("Choose your attack: ");
                Console.WriteLine("1. Attack with a sword\n2. Use the magic and throw a Fireball!\n3. Use the medication\n\n");

                //display player energy level
                Console.Write(ShowEnergyLevel(playerEnergy,name).PadRight(40));
                Console.ResetColor();

                //display dragon energy level 
                Console.WriteLine(ShowEnergyLevel(dragonEnergy,"Dragon"));
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
                            //keep the result of the sword attack in the string userAttackresult
                        case 1: userAttackResult = SwordAttack();
                            break;
                            //keep the result of the magic attack in the string userAttackresult
                        case 2: userAttackResult = MagicAttack();
                            break;
                            //keep the result of the healing in the string userAttackresult
                        case 3: userAttackResult = HealEnergy();
                            break;
                    }
                }
                else
                { 
                    //if the input is not a valid option
                    userAttackResult = "Next time choose a valid option".PadRight(40);
                }

                //if the user wins
                if (dragonEnergy <= 0)
                {
                    play = false;
                    DisplayVictory();
                }
                else 
                {
                    //keep the result of the dragon attack in the string
                    dragonAttackResult = DragonAttack();
                    
                    //if the user lose
                    if (playerEnergy <= 0)
                    {
                        play = false;
                        DisplayLose();
                    }
                }         
            }
        }

       /// <summary>
       /// Attack with a sword
       /// </summary>
       /// <returns>a string with the result of the attack</returns>
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

        /// <summary>
        /// Attach with a fireball
        /// </summary>
        /// <returns>a string with the result of the attack</returns>
        static string MagicAttack()
        {
            int damage;
            damage = gnr.Next(10, 15);
            dragonEnergy -= damage;

            return "You took " + damage + " HP off of the Dragon!";
        }

        /// <summary>
        /// Increment the energy of the player
        /// </summary>
        /// <returns>a string with the amount of energy healed</returns>
        static string HealEnergy()
        {
            int energyHealed;
            energyHealed = gnr.Next(10, 21);
            playerEnergy += energyHealed;

            return "The medication gave you " + energyHealed + " HP";
        }

        /// <summary>
        /// The dragon attack
        /// </summary>
        /// <returns>a string with the result of the attack</returns>
        static string DragonAttack()
        {
            int damage;

            //if the dragon hit
            if (gnr.Next(0, 11) <= 8)
            {
                damage = gnr.Next(5, 16);
                playerEnergy -= damage;
                return "Ahahah " + damage + " HP less for you!!";
            }
            return "The shield won't save you next time...";
        }

        /// <summary>
        /// Validate the input
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns>True if the input is a number between 1 and 3. False otherwise</returns>
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

        /// <summary>
        /// Print out the energy bar
        /// </summary>
        /// <param name="energy">the amount of energy left</param>
        /// <param name="name">the name either the player or the dragon</param>
        /// <returns></returns>
        static string ShowEnergyLevel(int energy, string name)
        {
            //keep thename of the player(or dragon) and the energy status bar
            string energyLevel = string.Empty;

            if (name == "Dragon")
            {
                energyLevel = "DRAGON ";
            }
            else
            {
                energyLevel = name.ToUpper()+" ";
            }
            
            //these if are to choose the amount of energy to display and the color of the status bar
            if (energy > 95)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                for (int i = 0; i < 10; i++)
                { 
                    energyLevel += "██";
                }
             }
             else if (energy > 80)
             {
                 Console.ForegroundColor = ConsoleColor.Green;
                 for (int i = 0; i < 8; i++)
                 {   
                     energyLevel += "██";
                 }
              }
              else if (energy > 60)
              {
                  Console.ForegroundColor = ConsoleColor.Yellow;
                  for (int i = 0; i < 6; i++)
                  {   
                     energyLevel += "██";
                  }

               }
               else if (energy > 40)
               {
                   Console.ForegroundColor = ConsoleColor.Yellow;
                   for (int i = 0; i < 4; i++)
                   { 
                     energyLevel += "██";
                   }

               }
               else if (energy > 20)
               {
                   Console.ForegroundColor = ConsoleColor.Red;
                   for (int i = 0; i < 2; i++)
                   {  
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

        /// <summary>
        /// Print out the logo of the game
        /// </summary>
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

        /// <summary>
        /// Print ouf the dragon
        /// </summary>
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

        /// <summary>
        /// Print out the story 
        /// </summary>
        /// <returns></returns>
        static string DisplayStory(){
            string text=@"
You are a brave knight and you must save the princess trapped in the castle. 
To get to her you must first defeat the terrible dragon 
that defends the castle doors.

You have two possible weapons: SWORD and FIREBALLS. 
To heal yourself you can use the MAGIC POTION that 
Merlin the Magician gave you before leaving.

The SWORD is the weapon that inflicts more damages
but it hits only 70% of the times.
The FIREBALL always strikes but does least damage to the dragon.
The MAGIC POTION gives you energy between 10 and 20 HP.

You have initially 100 HP
The dragon has 200 HP

GOOD LUCK!!!";

            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(20);
            }
            Console.WriteLine("\n\nEnter your name to begin");
            
            return Console.ReadLine();
        }

        /// <summary>
        /// Print out the lose with an animated dragon
        /// </summary>
        static void DisplayLose()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(@"
                                                |===-~___                _,-'
                 -==\\                         `//~\\   ~~~~`---.___.-~~
             ______-==|                         | |  \\           _-~`
       __--~~~  ,-/-==\\                        | |   `\        ,'
    _-~       /'    |  \\                      / /      \      /
  .'        /       |   \\                   /' /        \   /'
 /  ____  /         |    \`\.__/-~~ ~ \ _ _/'  /          \/'
/-'~    ~~~~~---__  |     ~-/~         ( )   /'        _--~`
                  \_|      /        _)   ;  ),   __--~~
                    '~~--_/      _-~/-  / \   '-~ \
                   {\__--_/}    / \\_>- )<__\      \
                   /'   (_/  _-~  | |__>--<__|      | 
                  |0  0 _/) )-~     | |__>--<__|      |
                  / /~ ,_/       / /__>---<__/      |  
                 o o _//        /-~_>---<__-~      /
                 (^(~          /~_>---<__-      _-~              
                ,/|           /__>--<__/     _-~                 
             ,//('(          |__>--<__|     /                  .----_ 
            ( ( '))          |__>--<__|    |                 /' _---_~\
         `-)) )) (           |__>--<__|    |               /'  /     ~\`\
        ,/,'//( (             \__>--<__\    \            /'  //        ||
      ,( ( ((, ))              ~-__>--<_~-_  ~--____---~' _/'/        /'
    `~/  )` ) ,/|                 ~-_~>--<_/-__       __-~ _/ 
  ._-~//( )/ )) `                    ~~-'_/_/ /~~~~~~~__--~ 
   ;'( ')/ ,)(                              ~~~~~~~~~~ 
  ' ') '( (/");
                Console.WriteLine(@"
     )    )                                
  ( /( ( /(             (                  
  )\()))\())     (      )\            (    
 ((_)\((_)\      )\    ((_) (   (    ))\   
__ ((_) ((_)  _ ((_)    _   )\  )\  /((_)  
\ \ / // _ \ | | | |   | | ((_)((_)(_))    
 \ V /| (_) || |_| |   | |/ _ \(_-</ -_)   
  |_|  \___/  \___/    |_|\___//__/\___|   
                                           ");
                System.Threading.Thread.Sleep(1500);
                Console.Clear();

                Console.WriteLine(@"
                                                |===-~___                _,-'
                 -==\\                         `//~\\   ~~~~`---.___.-~~
             ______-==|                         | |  \\           _-~`
       __--~~~  ,-/-==\\                        | |   `\        ,'
    _-~       /'    |  \\                      / /      \      /
  .'        /       |   \\                   /' /        \   /'
 /  ____  /         |    \`\.__/-~~ ~ \ _ _/'  /          \/'
/-'~    ~~~~~---__  |     ~-/~         ( )   /'        _--~`
                  \_|      /        _)   ;  ),   __--~~
                    '~~--_/      _-~/-  / \   '-~ \
                   {\__--_/}    / \\_>- )<__\      \
                   /'   (_/  _-~  | |__>--<__|      | 
                  |0  0 _/) )-~     | |__>--<__|      |
                  / /~ ,_/       / /__>---<__/      |  
                 o o _//        /-~_>---<__-~      /
                               /~_>---<__-      _-~              
                              /__>--<__/     _-~                 
                             |__>--<__|     /                  .----_ 
                             |__>--<__|    |                 /' _---_~\
                             |__>--<__|    |               /'  /     ~\`\
                              \__>--<__\    \            /'  //        ||
                               ~-__>--<_~-_  ~--____---~' _/'/        /'
                                  ~-_~>--<_/-__       __-~ _/ 
                                     ~~-'_/_/ /~~~~~~~__--~ 
                                            ~~~~~~~~~~");
                Console.WriteLine(@"

     )    )                                
  ( /( ( /(             (                  
  )\()))\())     (      )\            (    
 ((_)\((_)\      )\    ((_) (   (    ))\   
__ ((_) ((_)  _ ((_)    _   )\  )\  /((_)  
\ \ / // _ \ | | | |   | | ((_)((_)(_))    
 \ V /| (_) || |_| |   | |/ _ \(_-</ -_)   
  |_|  \___/  \___/    |_|\___//__/\___|   
                                           ");

                System.Threading.Thread.Sleep(500);
                Console.Clear();
            }
            Console.ReadKey();
           
        }

        /// <summary>
        /// Print out a castle for the victory
        /// </summary>
        static void DisplayVictory()
        {
            Console.Clear();
            Console.WriteLine(@"
                                       /\
                                      /`:\
                                     /`'`:\
                                    /`'`'`:\
                                   /`'`'`'`:\
                                  /`'`'`'`'`:\
                                   |`'`'`'`:|
     _ _  _  _  _                  |] ,-.  :|_  _  _  _
    ||| || || || |                 |  |_| ||| || || || |
    |`' `' `' `'.|                 | _'=' |`' `' `' `'.|
    :          .:;                 |'-'   :          .:;
     \-..____..:/  _  _  _  _  _  _| _  _'-\-..____..:/
      :--------:_,' || || || || || || || `.::--------:
      |]     .:|:.  `' `'_`' `' `' `' `'    | '-'  .:|
      |  ,-. .[|:._     '-' ____     ___    |   ,-.'-|
      |  | | .:|'--'_     ,'____`.  '---'   |   | |.:|
      |  |_| .:|:.'--' ()/,| |`|`.\()   __  |   |_|.:|
      |  '=' .:|:.     |::_|_|_|\|::   '--' |  _'='.:|
      | __   .:|:.     ;||-,-,-,-,|;        | '--' .:|
      |'--'  .:|:. _  ; ||       |:|        |      .:|
      |      .:|:.'-':  ||       |;|     _  |]     _:|
      |      '-|:.   ;  ||       :||    '-' |     '--|
      |  _   .:|].  ;   ||       ;||]       |   _  .:|
      | '-'  .:|:. :   [||      ;|||        |  '-' .:|
  ,', ;._____.::-- ;---->'-,--,:-'<'--------;._____.::.`.
 ((  (          )_;___,' ,' ,  ; //________(          ) ))
  `. _`--------' : -,' ' , ' '; //-       _ `--------' ,'
       __  .--'  ;,' ,'  ,  ': //    -.._    __  _.-  -
   `-   --    _ ;',' ,'  ,' ,;/_  -.       ---    _,
       _,.   /-:,_,_,_,_,_,_(/:-\   ,     ,.    _
     -'   `-'--'-'-'-'-'-'-'-''--'-' `-'`'  `'`' `-");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"__   __                                    _ 
\ \ / /                                   | |
 \ V /___  _   _   ___  __ ___   _____  __| |
  \ // _ \| | | | / __|/ _` \ \ / / _ \/ _` |
  | | (_) | |_| | \__ \ (_| |\ V /  __/ (_| |
  \_/\___/ \__,_| |___/\__,_| \_/ \___|\__,_|
                                             
                                             ");

            Console.WriteLine(@" _   _                       _                           _ _ _ 
| | | |                     (_)                         | | | |
| |_| |__   ___   _ __  _ __ _ _ __   ___ ___  ___ ___  | | | |
| __| '_ \ / _ \ | '_ \| '__| | '_ \ / __/ _ \/ __/ __| | | | |
| |_| | | |  __/ | |_) | |  | | | | | (_|  __/\__ \__ \ |_|_|_|
 \__|_| |_|\___| | .__/|_|  |_|_| |_|\___\___||___/___/ (_|_|_)
                 | |                                           
                 |_|                                           ");

            Console.ReadKey();
        }
    }
}

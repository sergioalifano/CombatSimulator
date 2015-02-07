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
            bool play = true;

            while (play)
            {
                Console.Write(ShowEnergyLevel(playerEnergy).PadRight(40));
                Console.ResetColor();
                Console.WriteLine(ShowEnergyLevel(dragonEnergy));
                Console.ResetColor();

                switch (ChooseAttack())
                {
                    case 1: if (SwordAttack())
                        {
                            Console.WriteLine("You hit the Dragon!");
                            
                        }
                        else
                        {
                            Console.WriteLine("Missed!");
                        }
                        break;
                    case 2: MagicAttack();
                        break;
                    default: HealEnergy();
                        break;
                }

                if (dragonEnergy == 0)
                {
                    Console.WriteLine("You defeat the dragon");
                    play = false;
                }

                else if (DragonAttack())
                {
                    Console.WriteLine("You have been hit by the dragon!");
                    if (playerEnergy == 0)
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
        static bool SwordAttack()
        {
            //if it's in the 70% of the positive attack 
            if (gnr.Next(0,11) <= 7)
            {
                //decrement dragon energy
                dragonEnergy -= gnr.Next(20, 36);
                return true;
            }
            return false;
        }

        static void MagicAttack()
        {
            dragonEnergy -= gnr.Next(10, 15);
        }

        static void HealEnergy()
        {
            playerEnergy += gnr.Next(10, 21);
        }

        static bool DragonAttack()
        {
            if (gnr.Next(0, 9)<=8)
            {
                playerEnergy -= gnr.Next(5, 16);
                return true;
            }
            return false;
        }

        static int ChooseAttack()
        {
            Console.WriteLine("Choose your attack: ");
            Console.WriteLine("1. Attack with a sword\n2. Use the magic and throw a Fireball!\n3. Heal yourself");
            return int.Parse(Console.ReadLine());

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
                       // Console.ResetColor();
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
                return energyLevel;
        }
    }
}

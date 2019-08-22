using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiarsDice
{
    class Game
    {
        Random myRandom = new Random();
        List<int> myPlayerHand = new List<int>();
        List<int> myOpponentHand = new List<int>();
        int myGuess;

        public void StartGame()
        {
            ShowStartScreen();
            Console.ReadKey();
            Console.Clear();

            Print.PrintColorText("Scrambling dices", ConsoleColor.Cyan);
            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }

            for (int i = 0; i < 5; i++)
            {
                myPlayerHand.Add(myRandom.Next(1, 7));
                myOpponentHand.Add(myRandom.Next(1, 7));
            }

            Console.Clear();
            ShowDices(true);
            Console.WriteLine(" ");
            Print.PrintColorText("Guess the amount of numbers of a certain numbers! \n", ConsoleColor.Yellow);

            if (CompareToHand(GetPlayerNumbers()))
            {
                Console.Clear();
                Print.PrintColorText("Congratulations! You are right!", ConsoleColor.Cyan);
                ShowPlayAgain();
            }
            else
            {
                Console.Clear();
                Print.PrintColorText("Aww you're wrong, off to the cliff you go!", ConsoleColor.Red);
                Print.PrintColorText("There were " + GetAmountInHands(myGuess) + " of " + myGuess + "'s", ConsoleColor.Red);
                ShowPlayAgain();
            }
            Console.ReadKey();
        }
        void ShowStartScreen()
        {
            var tempStartScreen = new[]
            {
                @"----------------------------------------------------------------------",
                @"-                                                                    -",
                @"-                                                                    -",
                @"-                                                                    -",
                @"-                           Liar's Dice                              -",
                @"-                                                                    -",
                @"-                      A Game by ChunkTreasure                       -",
                @"-                                                                    -",
                @"-                                                                    -",
                @"-                                                                    -",
                @"-                                                                    -",
                @"-                      Press ENTER to continue                       -",
                @"-                                                                    -",
                @"----------------------------------------------------------------------",
            };

            foreach (string line in tempStartScreen)
            {
                Print.PrintMiddle(line, true, 0, 0, ConsoleColor.DarkRed);
                System.Threading.Thread.Sleep(5);
            }
        }

        void ShowDices(bool anOnlyPlayer)
        {
            Print.PrintColorText("Your hand: ", ConsoleColor.Yellow);
            for (int i = 0; i < myPlayerHand.Count; i++)
            {
                if (i == myPlayerHand.Count - 1)
                {
                    Print.PrintColorText(myPlayerHand[i].ToString() + "\n", ConsoleColor.Yellow);
                }
                else
                {
                    Print.PrintColorText(myPlayerHand[i] + ", ", ConsoleColor.Yellow);
                }
            }
        }

        void ShowPlayAgain()
        {
            Console.WriteLine(" ");
            string tempVal;

            do
            {
                Print.PrintColorText("Want to play again? (Y/N)", ConsoleColor.Yellow);

                tempVal = Console.ReadLine();
                if (tempVal == "Y")
                {
                    // Starts a new instance of the program itself
                    System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName);

                    // Closes the current process
                    Environment.Exit(0);
                }
                else if (tempVal == "N")
                {
                    // Closes the current process
                    Environment.Exit(0);
                }

            } while (tempVal != "N" && tempVal != "Y");
        }

        bool CompareToHand(int[] someNumbers)
        {
            int tempNumbersInHand = 0;

            for (int i = 0; i < myPlayerHand.Count; i++)
            {
                if (myPlayerHand[i] == someNumbers[0])
                {
                    tempNumbersInHand++;
                }
            }

            for (int i = 0; i < myOpponentHand.Count; i++)
            {
                if (myPlayerHand[i] == someNumbers[0])
                {
                    tempNumbersInHand++;
                }
            }

            if (tempNumbersInHand == someNumbers[1])
            {
                return true;
            }

            return false;
        }

        int[] GetPlayerNumbers()
        {
            int tempNum;
            int tempAmount;

            do
            {
                Print.PrintColorText("What number? (1 - 6): ", ConsoleColor.Red);
                tempNum = Convert.ToInt32(Console.ReadLine());
                Print.PrintColorText("Alright.. \n", ConsoleColor.Yellow);

                if (tempNum < 1 || tempNum > 6)
                {
                    Print.PrintColorText("What were you thinking? I said between 1 and 6 \n", ConsoleColor.DarkRed);
                    Print.PrintColorText("Print ENTER to try again", ConsoleColor.Yellow);
                }

            } while (tempNum < 0 || tempNum > 6);

            do
            {
                Print.PrintColorText("What amount of that number? (1 - 10): ", ConsoleColor.Red);
                tempAmount = Convert.ToInt32(Console.ReadLine());
                Print.PrintColorText("I see.. \n", ConsoleColor.Yellow);

                if (tempAmount < 1 || tempAmount > 10)
                {
                    Print.PrintColorText("Hmm.. can't seem to understand your logic there. I said between 1 and 10. \n", ConsoleColor.DarkRed);
                    Print.PrintColorText("Press ENTER to try again", ConsoleColor.Yellow);
                    Console.ReadKey();
                }

            } while (tempAmount < 1 || tempAmount > 10);

            myGuess = tempNum;
            return new int[] { tempNum, tempAmount };
        }

        int GetAmountInHands(int aNum)
        {
            int tempAmount = 0;

            for (int i = 0; i < myPlayerHand.Count; i++)
            {
                if (myPlayerHand[i] == aNum)
                {
                    tempAmount++;
                }
            }

            for (int i = 0; i < myOpponentHand.Count; i++)
            {
                if (myPlayerHand[i] == aNum)
                {
                    tempAmount++;
                }
            }

            return tempAmount;
        }
    }
}

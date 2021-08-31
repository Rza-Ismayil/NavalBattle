using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naval_Battle
{
    class Game
    {
        private int turn;
        private Player player1;
        private Player player2;

        public int Turn { get => turn; set => turn = value; }
        public Player Player1 { get => player1; set => player1 = value; }
        public Player Player2 { get => player2; set => player2 = value; }

        public Game()
        {
            turn = 1;

            Console.Write("Player1, please enter your name: ");
            player1 = new(Console.ReadLine());
            Console.Clear();

            Console.Write("Player2, please enter your name: ");
            player2 = new(Console.ReadLine());
            Console.Clear();
        }

        public void PlayGame()
        {
            while(!IsGameOver())
            {
                PlayTurn();
            }
        }

        public void PlayTurn()
        {
            if(turn == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("======Sea of {0}======", player2.Name);
                Console.ResetColor();

                player2.BattleSea.ShowSea(false);
                player1.MakeAHit(player2);
                player2.BattleSea.ShowSea(false);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("======Sea of {0}======", player2.Name);
                Console.ResetColor();
            }
            else if(turn == -1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("======Sea of {0}======", player1.Name);
                Console.ResetColor();

                player1.BattleSea.ShowSea(false);
                player2.MakeAHit(player1);
                player1.BattleSea.ShowSea(false);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("======Sea of {0}======", player1.Name );
                Console.ResetColor();
            }

            Console.WriteLine("[press enter for next turn]");
            Console.ReadLine();
            Console.Clear();

            turn *= -1;
        }

        public bool IsGameOver()
        {
            if (player1.IsLostGame())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations {0}, YOU WIN", player2.Name);
                Console.ResetColor();
                return true;
            }
            else if(player2.IsLostGame())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations {0}, YOU WIN", player1.Name);
                Console.ResetColor();
                return true;
            }
            return false;
        }
    }
}

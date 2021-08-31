using System;

namespace Naval_Battle
{
    class Player
    {
        private int score;
        private string name;
        private Sea battleSea;
        private Naval[] navals;

        public int Score { get => score; set => score = value; }
        public string Name { get => name; set => name = value; }
        public Naval[] Navals { get => navals; set => navals = value; }
        public Sea BattleSea { get => battleSea; set => battleSea = value; }

        public Player(string name)
        {
            this.name = name;

            battleSea = new(10);
            score = 0;
            navals = new Naval[10];
            CreateNavals();
        }

        public void CreateNavals()
        {
            int[] navalLengths = { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };


            for (int i = 0; i < navalLengths.Length; i++)
            {
                battleSea.ShowSea(true);
                bool addAble = false;
                while (!addAble)
                {
                    Console.WriteLine("\nPlease enter the properties for the of length {0}", navalLengths[i]);

                    if (navalLengths[i] == 1)
                    {
                        navals[i] = new(GetNavalStartPoint(), new(0, 0, true), navalLengths[i]);
                    }
                    else
                    {
                        navals[i] = new(GetNavalStartPoint(), GetNavalDirection(), navalLengths[i]);
                    }

                    if (battleSea.IsNavalAddable(navals[i]))
                    {
                        addAble = true;
                        battleSea.AddNaval(navals[i]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Naval cannot be added to these positions!");
                        Console.ResetColor();
                    }
                }
            }
            battleSea.ShowSea(true);
            Console.WriteLine("[press enter to continue]");
            Console.ReadLine();
        }

        public Point GetNavalStartPoint()
        {

            int[] inputs = new int[2];
            int row = -1;
            int col = -1;

            while (row == -1 && col == -1)
            {
                Console.Write("Dear {0}, please enter the starting point of naval: ", name);
                string input = Console.ReadLine();
                if (SetInputIfValid(input, inputs))
                {
                    col = inputs[0];
                    row = inputs[1];
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter in the valid format and in bounds (ex. A5)");
                    Console.ResetColor();
                }
            }

            return new(row, col, false);
        }

        public Point GetNavalDirection()
        {
            while (true)
            {
                Console.Write("Dear {0}, please enter the direction of naval (left, right, up, down): ", name);
                string direction = Console.ReadLine().ToLower();

                if (direction.Equals("left"))
                {
                    return new(0, -1, true);
                }
                else if (direction.Equals("right"))
                {
                    return new(0, 1, true);
                }
                else if (direction.Equals("up"))
                {
                    return new(-1, 0, true);
                }
                else if (direction.Equals("down"))
                {
                    return new(1, 0, true);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dear {0}, please enter one of the valid directions (left, right, up, down)", name);
                    Console.ResetColor();
                }
            }
        }

        public bool SetInputIfValid(string input, int[] output)
        {
            char[] inputs = input.ToCharArray();
            inputs[0] = char.ToUpper(inputs[0]);
            if (inputs.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Dear {0}, more or less than 2 inputs is not valid! Only 2 inputs must be written", name);
                Console.ResetColor();
                return false;
            }

            if (LetterToIndex(inputs[0]) < 0 || LetterToIndex(inputs[0]) >= battleSea.size)
            {
                return false;
            }

            if (CharNumToIndex(inputs[1]) < 0 || CharNumToIndex(inputs[1]) >= battleSea.size)
            {
                return false;
            }

            output[0] = LetterToIndex(inputs[0]);
            output[1] = CharNumToIndex(inputs[1]);

            return true;
        }

        public static int LetterToIndex(char letter)
        {
            return letter - 65;
        }

        public static int CharNumToIndex(char number)
        {
            return number - 48;
        }

        public void MakeAHit(Player enemy)
        {
            bool hitSuccessful = false;
            int[] inputs = new int[2];
            int row;
            int col;

            while (!hitSuccessful)
            {
                Console.Write("\nDear {0}, please enter the point you want to hit: ", name);
                string input = Console.ReadLine();
                if (SetInputIfValid(input, inputs))
                {
                    col = inputs[0];
                    row = inputs[1];
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dear {0}, please enter in the valid format and in bounds (ex. A5)", name);
                    Console.ResetColor();
                    continue;
                }

                if (enemy.BattleSea.IsCellHittable(row, col))
                {
                    enemy.BattleSea.HitCell(row, col);
                    enemy.RevealSinkNavals();
                    hitSuccessful = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dear {0}, please enter a hittable cell.", name);
                    Console.ResetColor();
                }
            }
        }

        public void RevealSinkNavals()
        {
            foreach (Naval naval in navals)
            {
                battleSea.RevealAroundIfSink(naval);
            }
        }

        public bool IsLostGame()
        {
            foreach (Naval naval in navals)
            {
                if(!naval.IsSink())
                {
                    return false;
                }
            }
            return true;
        }
    }
}

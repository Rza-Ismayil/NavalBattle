using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naval_Battle
{
    class Sea
    {
        public readonly int size;
        private NavalCell[,] seaMap;

        public NavalCell[,] SeaMap { get => seaMap; set => seaMap = value; }

        public Sea(int size)
        {
            seaMap = new NavalCell[size, size];
            this.size = size;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    seaMap[i, j] = new(i, j, false);
                }
            }
        }

        public bool IsInSea(NavalCell navalCell)
        {
            if (navalCell.X >= 0 && navalCell.X < size && navalCell.Y >= 0 && navalCell.Y < size)
                return true;
            return false;
        }

        public bool IsInSea(int x, int y)
        {
            if (x >= 0 && x < size && y >= 0 && y < size)
                return true;
            return false;
        }

        public bool IsCellEmpty(NavalCell navalCell)
        {
            foreach (NavalCell cell in GetNeighbors(navalCell))
            {
                if(cell.BelongNaval)
                {
                    return false;
                }
            }
            return true;
        }

        public NavalCell[] GetNeighbors(NavalCell navalCell)
        {
            int x = navalCell.X;
            int y = navalCell.Y;

            LinkedList<NavalCell> neighbors = new();

            neighbors.AddFirst(seaMap[x, y]);

            if (IsInSea(x + 1, y))
                neighbors.AddFirst(seaMap[x + 1, y]);

            if (IsInSea(x - 1, y))
                neighbors.AddFirst(seaMap[x - 1, y]);

            if (IsInSea(x, y + 1))
                neighbors.AddFirst(seaMap[x, y + 1]);

            if (IsInSea(x, y - 1))
                neighbors.AddFirst(seaMap[x, y - 1]);

            if (IsInSea(x + 1, y + 1))
                neighbors.AddFirst(seaMap[x + 1, y + 1]);

            if (IsInSea(x + 1, y - 1))
                neighbors.AddFirst(seaMap[x + 1, y - 1]);

            if (IsInSea(x - 1, y + 1))
                neighbors.AddFirst(seaMap[x - 1, y + 1]);

            if (IsInSea(x - 1, y - 1))
                neighbors.AddFirst(seaMap[x - 1, y - 1]);

            return neighbors.ToArray();
        }

        public bool IsNavalAddable(Naval naval)
        {
            foreach (NavalCell navalCell in naval.navalCells)
            {
                if (!(IsInSea(navalCell) && IsCellEmpty(navalCell)))
                {
                    return false;
                }
                
            }
            return true;
        }

        public void AddNaval(Naval naval)
        {
            foreach (NavalCell navalCell in naval.navalCells)
            {
                seaMap[navalCell.X, navalCell.Y] = navalCell;
            }
        }

        public void HitCell(int x, int y)
        {
            seaMap[x, y].IsShot = true;
        }

        public bool IsCellHittable(int x, int y)
        {
            if (IsInSea(x, y))
                return !seaMap[x, y].IsShot;
            else
                return false;
        }

        public void RevealAroundIfSink(Naval naval)
        {
            if(naval.IsSink())
            {
                foreach (NavalCell navalCell in naval.navalCells)
                {
                    foreach (NavalCell neighbour in GetNeighbors(navalCell))
                    {
                        neighbour.IsShot = true;
                    }
                }
            }
        }

        public void ShowSea(bool editMode)
        {
            Console.Write("  ");
            for (int i = 0; i < size; i++)
                Console.Write((char)(i + 65) + " ");
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                Console.Write(i + " ");
                for (int j = 0; j < size; j++)
                {
                    char symbol = seaMap[i, j].GetSymbol(editMode);

                    if (symbol == '~' || symbol == '\u25cf')
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (symbol == 'X')
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (symbol == '\u25a0')
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write(symbol + " ");

                    Console.ResetColor();
                }


                Console.WriteLine();
            }
        }
    }
}

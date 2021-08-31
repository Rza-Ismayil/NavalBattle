using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naval_Battle
{
    public class NavalCell
    {
        private int x, y;
        private bool isShot;
        private bool belongNaval;


        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public bool IsShot { get => isShot; set => isShot = value; }
        public bool BelongNaval { get => belongNaval; set => belongNaval = value; }

        public NavalCell(int x, int y, bool belongNaval)
        {
            this.x = x;
            this.y = y;
            this.belongNaval = belongNaval;
            isShot = false;
        }

        public char GetSymbol(bool editMode)
        {
            if(editMode)
            {
                if (isShot)
                {
                    if (belongNaval)
                    {
                        return 'X';
                    }
                    else
                    {
                        return '\u25cf';
                    }
                }
                else
                {
                    if (belongNaval)
                    {
                        return '\u25a0';
                    }
                    else
                    {
                        return '~';
                    }
                }
            }
            else
            {
                if(isShot)
                {
                    if (belongNaval)
                    {
                        return 'X';
                    }
                    else
                    {
                        return '\u25cf';
                    }
                }
                else
                {
                    return '~';
                }
            }
        }
    }

    public struct Point
    {
        public int X, Y;

        public Point(int x, int y, bool isVector)
        {
            if (x != 0 && y != 0 && isVector)
            {
                Console.WriteLine("Direction cannot be diagonal.");
                X = 0;
                Y = 0;
            }
            else
            {
                X = x;
                Y = y;
            }
        }
    }
}

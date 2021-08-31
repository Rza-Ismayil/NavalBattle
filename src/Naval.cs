using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naval_Battle
{

    public class Naval
    {
        public NavalCell[] navalCells;

        public Naval(Point starting, Point direction, int length)
        {
            navalCells = new NavalCell[length];

            for (int i = 0; i < length; i++)
            {
                navalCells[i] = new(starting.X + direction.X * i, starting.Y + direction.Y * i, true);
            }
        }

        public void Hit()
        {
            foreach (NavalCell navalCell in navalCells)
            {
                navalCell.IsShot = true;
            }
        }

        public bool IsSink()
        {
            foreach (NavalCell navalCell in navalCells)
            {
                if (!navalCell.IsShot)
                    return false;
            }
            return true;
        }
    }
}

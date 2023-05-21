using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Grid
{
    public interface IGridPassable
    {
        public bool IsLevelPassAble(int width, int height);
    }
}

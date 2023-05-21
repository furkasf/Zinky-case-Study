using Assets.Scripts.Game.GridCell;
using System.Collections.Generic;

namespace Assets.Scripts.Game.Grid
{
    public interface IGrid
    {
        public List<GridCellController> GetBlocksDestroy();
    }
}
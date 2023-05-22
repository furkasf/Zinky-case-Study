using Assets.Scripts.Game.GridCell;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Grid
{
    public class SubGridController : MonoBehaviour, IGrid
    {
        public GridCellController[,] Cells;

        public List<GridCellController> GetBlocksDestroy()
        {
            List<GridCellController> targetCells = new List<GridCellController>();

            foreach (GridCellController cell in Cells)
            {
                if (cell.State == GridCell.GridCellState.Out)
                {
                    return null;
                }
            }

            foreach (GridCellController cell in Cells)
            {
                targetCells.Add(cell);
            }

            return targetCells;
        }
    }
}
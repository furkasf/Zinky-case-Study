using Assets.Scripts.Game.GridCell;
using UnityEngine;

namespace Assets.Scripts.Game.Grid
{
    public class SubGridController : MonoBehaviour, IGrid
    {
        public GridCellController[,] Cells;

        public void DestroyBlock()
        {
            foreach (var cell in Cells)
            {
                if (cell.State == GridCell.GridCellState.Out)
                {
                    return;
                }
            }

            foreach (var cell in Cells)
            {
                cell.DestroyBlock();
            }
        }
    }
}
using Assets.Scripts.Game.GridCell;
using UnityEngine;

namespace Assets.Scripts.Game.Grid
{
    public class SubGridController : MonoBehaviour, IGrid
    {
        public GridCellController[,] cells;

        public void DestroyBlock()
        {
            foreach (var cell in cells)
            {
                if (cell.State == GridCell.GridCellState.Out)
                {
                    return;
                }
            }

            foreach (var cell in cells)
            {
                cell.DestroyBlock();
            }
        }
    }
}
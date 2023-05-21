using Assets.Scripts.Game.GridCell;
using UnityEngine;

namespace Assets.Scripts.Game.Grid
{
    public class MainGridController : MonoBehaviour, IGrid, IGridPassable
    {
        public GridCellController[,] Cells;

        public void DestroyBlock()
        {
            int rows = Cells.GetLength(0);
            int columns = Cells.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                bool IfCellFilled = true;

                for (int col = 0; col < columns; col++)
                {
                    if (Cells[row, col].State == GridCell.GridCellState.Out)
                    {
                        IfCellFilled = false;
                        break;
                    }
                }

                if (IfCellFilled)
                {
                    for (int col = 0; col < columns; col++)
                    {
                        print(IfCellFilled);
                        Cells[row, col].DestroyBlock();
                    }
                }
            }

            for (int col = 0; col < columns; col++)
            {
                bool isColumnFilled = true;
                for (int row = 0; row < rows; row++)
                {
                    if (Cells[row, col].State == GridCell.GridCellState.Out)
                    {
                        isColumnFilled = false;
                        break;
                    }
                }
                if (isColumnFilled)
                {
                    for (int row = 0; row < rows; row++)
                    {
                        Cells[row, col].DestroyBlock();
                    }
                }
            }
        }

        public bool IsLevelPassAble(int width, int height)
        {
            int gridWidth = Cells.GetLength(0);
            int gridHeight = Cells.GetLength(1);

            for (int row = 0; row < gridHeight - width; row++)
            {
                for (int col = 0; col < gridWidth - height; col++)
                {
                    bool isSuitable = true;

                    for (int startX = 0; startX < width; startX++)
                    {
                        for (int startY = 0; startY < height; startY++)
                        {
                            var gridCell = Cells[row + startX, col + startY];

                            if (gridCell.State == GridCellState.In)
                            {
                                isSuitable = false;
                                break;
                            }
                        }

                        if (!isSuitable)
                        {
                            break;
                        }
                    }


                    if (isSuitable)
                    {
                        return true;
                    }
                }
            }

            return false;

        }

    }
}
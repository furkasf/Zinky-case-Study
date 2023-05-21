using Assets.Scripts.Game.GridCell;
using UnityEngine;

namespace Assets.Scripts.Game.Grid
{
    public class MainGridController : MonoBehaviour, IGrid, IGridPassable
    {
        public GridCellController[,] cells;

        public void DestroyBlock()
        {
            int rows = cells.GetLength(0);
            int columns = cells.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                bool IfCellFilled = true;

                for (int col = 0; col < columns; col++)
                {
                    if (cells[row, col].State == GridCell.GridCellState.Out)
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
                        cells[row, col].DestroyBlock();
                    }
                }
            }

            for (int col = 0; col < columns; col++)
            {
                bool isColumnFilled = true;
                for (int row = 0; row < rows; row++)
                {
                    if (cells[row, col].State == GridCell.GridCellState.Out)
                    {
                        isColumnFilled = false;
                        break;
                    }
                }
                if (isColumnFilled)
                {
                    for (int row = 0; row < rows; row++)
                    {
                        cells[row, col].DestroyBlock();
                    }
                }
            }
        }

        public bool IsLevelPassAble(int width, int height)
        {
            int gridWidth = cells.GetLength(0);
            int gridHeight = cells.GetLength(1);

            for (int row = 0; row < gridHeight - width; row++)
            {
                for (int col = 0; col < gridWidth - height; col++)
                {
                    bool isSuitable = true;

                    for (int startX = 0; startX < width; startX++)
                    {
                        for (int startY = 0; startY < height; startY++)
                        {
                            var gridCell = cells[row + startX, col + startY];

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
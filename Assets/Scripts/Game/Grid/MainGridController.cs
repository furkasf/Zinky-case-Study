using Assets.Scripts.Game.GridCell;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Grid
{
    public class MainGridController : MonoBehaviour, IGrid, IGridPassable
    {
        public GridCellController[,] Cells;

        private int _gridWidth;
        private int _gridHeight;

        private void Start()
        {
            GetGridSize();
        }
        public List<GridCellController> GetBlocksDestroy()
        {
            int rows = Cells.GetLength(0);
            int columns = Cells.GetLength(1);

            List<GridCellController> targetCells = new List<GridCellController>();
            List<GridCellController> filledRowCells = new List<GridCellController>();
            List<GridCellController> filledColumnCells = new List<GridCellController>();

            for (int row = 0; row < rows; row++)
            {
                bool isRowFilled = true;

                for (int col = 0; col < columns; col++)
                {
                    if (Cells[row, col].State == GridCellState.Out)
                    {
                        isRowFilled = false;
                        break;
                    }
                }

                if (isRowFilled)
                {
                    for (int col = 0; col < columns; col++)
                    {
                        filledRowCells.Add(Cells[row, col]);
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
                        filledColumnCells.Add(Cells[row, col]);
                    }
                }
            }

            targetCells.AddRange(filledRowCells);
            targetCells.AddRange(filledColumnCells);

            return targetCells;
        }

        public bool IsLevelPassable(int shapeWidth, int shapeHeight)
        {
           for(int i = 0; i < _gridWidth ; i++)
            {
                for(int j =0 ; j < _gridHeight ; j++)
                {
                    if(IsShapeFit(i , j, shapeWidth, shapeHeight))
                    {
                        return true;
                    }
                }
            }
           return false;
        }

        private bool IsShapeFit(int gridX, int GridY, int shapeWidth, int shapeHeight)
        {

            if (gridX + shapeWidth > _gridWidth || GridY + shapeHeight > _gridHeight)
            {
                return false;
            }

            for (int x = 0; x < shapeWidth; x++)
            {
                for (int y = 0; y < shapeHeight; y++)
                {
                    var gridCell = Cells[gridX + x, GridY + y];

                    if (gridCell.State == GridCellState.In)
                    {
                        return false; 
                    }

                }
            }

            return true;
        }

        private void GetGridSize()
        {
            _gridWidth = Cells.GetLength(0); 
            _gridHeight = Cells.GetLength(1); 
        }

    }
}
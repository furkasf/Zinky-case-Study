using Assets.Scripts.Game.Grid;
using Assets.Scripts.Game.GridCell;
using UnityEngine;

namespace Assets.Scripts.Game.Level
{
    public class SudokuBlockGenerator : MonoBehaviour
    {
        [SerializeField] private int Width;
        [SerializeField] private int Height;
        [SerializeField] private float Scale;

        [Space]
        [SerializeField] private GameObject GridCellPrefab;

        private void Awake()
        {
            SudokuGenerator();
        }

        public void SudokuGenerator()
        {
            GameObject gridManager = CreateNewGameObject("GridManager");
            GridManager manager = gridManager.AddComponent<GridManager>();

            GameObject mainGridController = CreateNewGameObject("MainGridController", gridManager.transform);
            MainGridController mainGrid = mainGridController.AddComponent<MainGridController>();
            mainGrid.Cells = new GridCellController[Width, Height];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    GameObject cellGrid = Instantiate(GridCellPrefab, gridManager.transform);
                    cellGrid.transform.position = new Vector3(i * Scale, j * Scale, 0f);
                    mainGridController.GetComponent<MainGridController>().Cells[i, j] = cellGrid.GetComponent<GridCellController>();
                }
            }

            manager.Grids.Add(mainGrid);
            manager.GridPassable = mainGrid;

            DivideSudokuIntoSubGrids(ref mainGrid, ref manager);
        }

        private void DivideSudokuIntoSubGrids(ref MainGridController mainGrid, ref GridManager manager)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GameObject subGridController = CreateNewGameObject("SubGridController", manager.transform);
                    SubGridController subgrid = subGridController.AddComponent<SubGridController>();

                    subgrid.Cells = new GridCellController[3, 3];

                    for (int x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 3; y++)
                        {
                            subgrid.Cells[x, y] = mainGrid.Cells[i * 3 + x, j * 3 + y];
                        }
                    }
                    manager.Grids.Add(subgrid);
                }
            }
        }

        private GameObject CreateNewGameObject(string name)
        {
            GameObject emptyObject = new GameObject("EmptyObject");
            Transform emptyObjTransform = emptyObject.transform;

            emptyObjTransform.SetParent(transform);
            emptyObjTransform.position = Vector3.zero;
            emptyObjTransform.rotation = Quaternion.identity;
            emptyObjTransform.localScale = Vector3.one;
            emptyObject.name = name;

            return emptyObject;
        }

        private GameObject CreateNewGameObject(string name, Transform parent)
        {
            GameObject emptyObject = new GameObject("EmptyObject");
            Transform emptyObjTransform = emptyObject.transform;

            emptyObjTransform.SetParent(parent);
            emptyObjTransform.position = Vector3.zero;
            emptyObjTransform.rotation = Quaternion.identity;
            emptyObjTransform.localScale = Vector3.one;
            emptyObject.name = name;

            return emptyObject;
        }
    }
}
using Assets.Scripts.Game.Grid;
using Assets.Scripts.Game.GridCell;
using NaughtyAttributes;
using UnityEngine;

public class SudokuBlockEditor : MonoBehaviour
{
    public int Width;
    public int Height;
    public float Scale;

    [Space]
    public GameObject GridCellPrefab;

    private void Awake()
    {
        if(transform.childCount == 0)
        {
            SudokuGenerator();
        }
    }
    [Button]
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
                GameObject CellGrid = Instantiate(GridCellPrefab, gridManager.transform);
                CellGrid.transform.position = new Vector3(i * Scale, j * Scale, 0f);
                mainGridController.GetComponent<MainGridController>().Cells[i, j] = CellGrid.GetComponent<GridCellController>();
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
                GameObject subGridController = CreateNewGameObject("SubGridController", manager.transform);//can be add parent
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

        emptyObject.transform.SetParent(transform);

        emptyObject.transform.position = Vector3.zero;
        emptyObject.transform.rotation = Quaternion.identity;
        emptyObject.transform.localScale = Vector3.one;
        emptyObject.name = name;

        return emptyObject;
    }

    private GameObject CreateNewGameObject(string name, Transform parent)
    {
        GameObject emptyObject = new GameObject("EmptyObject");

        emptyObject.transform.SetParent(transform);

        emptyObject.transform.position = Vector3.zero;
        emptyObject.transform.rotation = Quaternion.identity;
        emptyObject.transform.localScale = Vector3.one;
        emptyObject.name = name;

        return emptyObject;
    }
}
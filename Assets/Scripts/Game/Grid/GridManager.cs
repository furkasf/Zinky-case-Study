using Assets.Scripts.Game.Block;
using Assets.Scripts.Game.GridCell;
using Game.Events;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game.Grid
{
    public class GridManager : MonoBehaviour
    {
        public List<IGrid> Grids = new List<IGrid>();
        public IGridPassable GridPassable;

        private void OnEnable()
        {
            GridSignals.onDestroyDestroyableBlocks += DestroyDestroyableBlocks;
            GridSignals.onIsLevelPassable += IsLevelPassable;
        }

        private void OnDisable()
        {
            GridSignals.onDestroyDestroyableBlocks -= DestroyDestroyableBlocks;
            GridSignals.onIsLevelPassable -= IsLevelPassable;
        }

        public void IsLevelPassable()
        {
            List<BlockManager> managers = SpawnSignal.onGetBlockManagers();

            bool isLevelPassable;

            for (int i = 0; i < managers.Count; i++)
            {
                isLevelPassable = GridPassable.IsLevelPassable(managers[i].ObjectWidth, managers[i].ObjectHeight);

                if (isLevelPassable)
                {
                    return;
                }
            }
            LevelSignal.onOpenRestartPanel();
        }

        public void DestroyDestroyableBlocks()
        {
            List<GridCellController> targets = new List<GridCellController>();

            foreach (IGrid grid in Grids)
            {
                List<GridCellController> gridTargetCell = grid.GetBlocksDestroy();

                if (gridTargetCell != null)
                {
                    targets.AddRange(gridTargetCell);
                }
            }

            var filteredList = targets.Distinct().ToList();

            foreach (var target in filteredList)
            {
                target.DestroyBlock();
            }
        }
    }
}
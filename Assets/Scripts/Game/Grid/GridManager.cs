using Assets.Scripts.Game.Block;
using Assets.Scripts.Game.Level;
using Assets.Scripts.Game.Spawner;
using System.Collections.Generic;
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

            bool isLevelPassable = false;

            for (int i = 0; i < managers.Count; i++)
            {
                isLevelPassable = GridPassable.IsLevelPassAble(managers[i].ObjectWidth, managers[i].ObjectHeight);

                if (isLevelPassable)
                {
                    return;
                }
            }
            LevelSignal.onLevelReset();
        }

        public void DestroyDestroyableBlocks()
        {
            foreach (var grid in Grids)
            {
                grid.DestroyBlock();
            }
        }
    }
}
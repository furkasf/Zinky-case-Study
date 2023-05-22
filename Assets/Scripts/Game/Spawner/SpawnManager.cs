using Assets.Scripts.Game.Block;
using Game.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Spawner
{
    public class SpawnManager : MonoBehaviour
    {
        private List<SpawnController> spawnControllers = new List<SpawnController>();

        private void Awake()
        {
            GetSpawners();
            SetSpawnerData();
        }

        private void Start()
        {
            SpawnNewBlock();
        }

        private void OnEnable()
        {
            SpawnSignal.onSpawnNewBlock += SpawnNewBlock;
            SpawnSignal.onGetBlockManagers += GetBlockManagers;
            SpawnSignal.onDestroyAllBlocks += DestroyAllBlocks;
            LevelSignal.onLevelReset += ResetAction;
        }

        private void OnDisable()
        {
            SpawnSignal.onSpawnNewBlock -= SpawnNewBlock;
            SpawnSignal.onGetBlockManagers -= GetBlockManagers;
            SpawnSignal.onDestroyAllBlocks += DestroyAllBlocks;
            LevelSignal.onLevelReset -= ResetAction;
        }

        private void GetSpawners()
        {
            foreach (Transform t in transform)
            {
                spawnControllers.Add(t.GetComponent<SpawnController>());
            }
        }

        private bool IsEmty()
        {
            for (int i = 0; i < spawnControllers.Count; i++)
            {
                if (spawnControllers[i].IsNeedToSpawnBlock() == false) return false;
            }
            return true;
        }

        private void SpawnNewBlock()
        {
            if (IsEmty() == true)
            {
                foreach (var Spawner in spawnControllers)
                {
                    Spawner.SpawnBlocks();
                }
            }
        }

        public List<BlockManager> GetBlockManagers()
        {
            List<BlockManager> managers = new List<BlockManager>();

            foreach (var spawn in spawnControllers)
            {
                if (spawn.transform.childCount > 0)
                {
                    managers.Add(spawn.transform.GetChild(0).GetComponent<BlockManager>());
                }
            }

            return managers;
        }

        private void SetSpawnerData()
        {
            SpawnBlockData data = Resources.Load<SpawnBlockData>("SpawnData");

            foreach (SpawnController controller in spawnControllers)
            {
                controller.SetData(ref data);
            }
        }

        private void DestroyAllBlocks()
        {
            foreach (var bloc in GetBlockManagers())
            {
                Destroy(bloc.gameObject);
            }
        }

        private void ResetAction()
        {
            foreach (var bloc in GetBlockManagers())
            {
                Destroy(bloc.gameObject);
            }
            foreach (var Spawner in spawnControllers)
            {
                Spawner.SpawnBlocks();
            }
        }
    }
}
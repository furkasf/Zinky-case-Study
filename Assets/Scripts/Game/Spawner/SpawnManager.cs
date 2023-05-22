using Assets.Scripts.Game.Block;
using Game.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Spawner
{
    public class SpawnManager : MonoBehaviour
    {
        private List<SpawnController> _spawnControllers = new List<SpawnController>();

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
                _spawnControllers.Add(t.GetComponent<SpawnController>());
            }
        }

        private bool IsEmpty()
        {
            for (int i = 0; i < _spawnControllers.Count; i++)
            {
                if (_spawnControllers[i].IsNeedToSpawnBlock() == false) return false;
            }
            return true;
        }

        private void SpawnNewBlock()
        {
            if (IsEmpty() == true)
            {
                foreach (SpawnController spawner in _spawnControllers)
                {
                    spawner.SpawnBlocks();
                }
            }
        }

        private List<BlockManager> GetBlockManagers()
        {
            List<BlockManager> managers = new List<BlockManager>();

            foreach (SpawnController spawn in _spawnControllers)
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

            foreach (SpawnController controller in _spawnControllers)
            {
                controller.SetData(ref data);
            }
        }

        private void DestroyAllBlocks()
        {
            foreach (BlockManager bloc in GetBlockManagers())
            {
                Destroy(bloc.gameObject);
            }
        }

        private void ResetAction()
        {
            foreach (BlockManager bloc in GetBlockManagers())
            {
                Destroy(bloc.gameObject);
            }
            foreach (SpawnController spawner in _spawnControllers)
            {
                spawner.SpawnBlocks();
            }
        }
    }
}
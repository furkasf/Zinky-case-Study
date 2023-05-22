using Assets.Scripts.Game.Block;
using Assets.Scripts.Game.Spawner;
using UnityEngine;

namespace Assets.Scripts.Game.Spawner
{
    public class SpawnController : MonoBehaviour
    {
        private SpawnBlockData _data;
        private BlockManager _block { get; set; }

        public bool IsNeedToSpawnBlock()
        {
            if (transform.childCount > 0) return false;
            return true;
        }

        public void SpawnBlocks()
        {
            GameObject block = Instantiate(_data.Prefabs[Random.Range(0, _data.Prefabs.Count)], transform);
            _block = block.GetComponent<BlockManager>();
        }

        public void SetData(ref SpawnBlockData data)
        {
            _data = data;
        }
    }
}
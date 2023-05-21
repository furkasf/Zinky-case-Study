using Assets.Scripts.Game.Block;
using Assets.Scripts.Game.Spawner;
using UnityEngine;

namespace Assets.Scripts.Game.Spawner
{
    public class SpawnController : MonoBehaviour
    {
        private SpawnBlockData _data;
        public BlockManager Block { get; private set; }

        public bool IsNeedToSpawnBlock()
        {
            if (transform.childCount > 0) return false;
            return true;
        }

        public void SpawnBlocks()
        {
            GameObject block = Instantiate(_data.prefabs[Random.Range(0, _data.prefabs.Count)], transform);
            Block = block.GetComponent<BlockManager>();
        }

        public void SetData(ref SpawnBlockData data)
        {
            _data = data;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Block
{
    public class BlockManager : MonoBehaviour
    {
        [field: SerializeField]
        public int ObjectWidth { get; private set; }

        [field: SerializeField]
        public int ObjectHeight { get; private set; }

        [field: SerializeField]
        public BlockState State { get; private set; }

        private List<BlockController> _blocks = new List<BlockController>();
        private Vector3 _startPos;

        private void Start()
        {
            State = BlockState.UnSnap;
            GetAllBlocks();
            _startPos = transform.parent.position;
        }

        private void GetAllBlocks()
        {
            foreach (Transform t in transform)
            {
                _blocks.Add(t.GetComponent<BlockController>());
            }
        }

        public void BlocksCastRay()
        {
            foreach (var block in _blocks)

            {
                block.CastRay();
            }
        }

        public void SnapBlocks()
        {
            if (IsBlocksAreSnapable() == false)
            {
                transform.localPosition = Vector3.zero;
                return;
            }

            foreach (BlockController block in _blocks)
            {
                if (block.IsSnapAble() == false)
                {
                    return;
                }
            }
            transform.parent = null;
            State = BlockState.InSnap;
            _blocks.ForEach(block => { block.SnaptoGrid(); });
        }

        public void RetutnBackToInitialPos()
        {
            transform.position = _startPos;
        }

        private bool IsBlocksAreSnapable()
        {
            for (int i = 0; i < _blocks.Count; i++)
            {
                if (_blocks[i].IsSnapAble() == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
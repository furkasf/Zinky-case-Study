using System;
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
        
        public BlockState State { get; private set; }

        private List<BlockController> _blocks = new List<BlockController>();
        private Vector3 _startPos;
        private Transform _myTransform;

        private void Start()
        {
            _myTransform = transform;
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
            foreach (BlockController block in _blocks)
            {
                block.CastRay();
            }
        }

        public void SnapBlocks()
        {
            if (IsBlocksAreSnapable() == false)
            {
                _myTransform.localPosition = Vector3.zero;
                return;
            }

            foreach (BlockController block in _blocks)
            {
                if (block.IsSnapAble() == false)
                {
                    return;
                }
            }
            _myTransform.SetParent(null);
            State = BlockState.InSnap;
            _blocks.ForEach(block => { block.SnaptoGrid(); });
        }

        public void RetutnBackToInitialPos()
        {
            _myTransform.position = _startPos;
        }

        private bool IsBlocksAreSnapable()
        {
            foreach(BlockController block in _blocks)
            {
                if (block.IsSnapAble() == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
using Assets.Scripts.Game.Block;
using Assets.Scripts.Game.Level;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game.GridCell
{
    public class GridCellController : MonoBehaviour
    {
        [field: SerializeField]
        public GridCellState State { get; private set; }

        private BlockController _snapBlock;

        private void Awake()
        {
            State = GridCellState.Out;
            _snapBlock = null;
        }

        private void OnEnable()
        {
            LevelSignal.onLevelReset += ResetBlock;
        }

        private void OnDisable()
        {
            LevelSignal.onLevelReset -= ResetBlock;
        }

        public void DestroyBlock()
        {
            StartCoroutine(InitDestroyableBlocks());
        }

        private IEnumerator InitDestroyableBlocks()
        {
            yield return new WaitForSecondsRealtime(0.1f);

            if (_snapBlock != null)
            {
                GameObject.Destroy(_snapBlock.transform.gameObject);
                _snapBlock = null;
                State = GridCellState.Out;
            }

            if (_snapBlock == null)
            {
                State = GridCellState.Out;
            }
            yield return null;
        }

        public void AttachToBlock(BlockController block)
        {
            _snapBlock = block;
            State = GridCellState.In;
        }

        private void ResetBlock()
        {
            if (_snapBlock != null)
            {
                GameObject.Destroy(_snapBlock.transform.parent.gameObject);
                _snapBlock = null;
            }
            State = GridCellState.Out;
        }
    }
}
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

        private BlockController _blockController = null;

        private void Awake()
        {
            State = GridCellState.Out;
        }

        private void OnEnable()
        {
            LevelSignal.onLevelReset += DestroyBlock;
        }

        private void OnDisable()
        {
            LevelSignal.onLevelReset -= DestroyBlock;
        }

        public void DestroyBlock()
        {
            if (_blockController != null)
            {
                GameObject.Destroy(_blockController.gameObject);
                _blockController = null;
                StartCoroutine(InitBlocksStateDelay());
            }
        }

        private IEnumerator InitBlocksStateDelay()
        {
            yield return new WaitForSecondsRealtime(1f);
            State = GridCellState.Out;
        }

        public void AttachToBlock(BlockController block)
        {
            _blockController = block;
            State = GridCellState.In;
        }
    }
}
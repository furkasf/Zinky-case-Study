using Assets.Scripts.Game.Block;
using Assets.Scripts.Game.Grid;
using Assets.Scripts.Game.Level;
using Assets.Scripts.Game.Spawner;
using UnityEngine;

namespace Assets.Scripts.Game.MyInput
{
    public class InputManager : MonoBehaviour
    {
        private BlockManager _blockManager;

        private void OnEnable()
        {
            LevelSignal.onLevelReset += ResetReferences;
        }

        private void OnDisable()
        {
            LevelSignal.onLevelReset -= ResetReferences;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectBlock();
            }
            if (Input.GetMouseButton(0))
            {
                MoveBlock();
            }
            if (Input.GetMouseButtonUp(0))
            {
                DropBlock();
            }
        }

        private void SelectBlock()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.parent.TryGetComponent(out BlockManager blockManager))
                {
                    _blockManager = (blockManager.State == BlockState.UnSnap) ? blockManager : null;
                }
            }
            else
            {
                _blockManager = null;
            }
        }

        private void MoveBlock()
        {
            if (_blockManager != null)
            {
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
                _blockManager.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
                _blockManager.BlocksCastRay();
            }
        }

        private void DropBlock()
        {
            if (_blockManager == null) return;

            _blockManager.SnapBlocks();
            _blockManager = null;

            SpawnSignal.onSpawnNewBlock();
            GridSignals.onDestroyDestroyableBlocks();
            GridSignals.onIsLevelPassable();
        }

        private void ResetReferences()
        {
            _blockManager = null;
        }
    }
}
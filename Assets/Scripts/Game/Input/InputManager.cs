using Assets.Scripts.Game.Block;
using Game.Events;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game.MyInput
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private BlockManager _blockManager;
        private YieldInstruction _delayInstraction = new WaitForSeconds(0.25f);
        private Coroutine _dropBlock;

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
                StopDropCoroutine();
                SelectBlock();
            }
            else if (Input.GetMouseButton(0))
            {
                MoveBlock();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StartDropCoroutine();
            }
        }

        private void SelectBlock()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
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
                _blockManager.transform.position = _camera.ScreenToWorldPoint(mousePosition);
                _blockManager.BlocksCastRay();
            }
        }

        private void StartDropCoroutine()
        {
            if (_dropBlock == null)
            {
                _dropBlock = StartCoroutine(DropBlock());
            }
            else
            {
                StopCoroutine(DropBlock());
                _dropBlock = StartCoroutine(DropBlock());
            }
        }

        private void StopDropCoroutine()
        {
            if (_dropBlock != null)
            {
                StopCoroutine(DropBlock());
                _dropBlock = null;
            }
        }

        private IEnumerator DropBlock()
        {
            if (_blockManager == null) yield break;

            _blockManager.SnapBlocks();
            _blockManager = null;

            SpawnSignal.onSpawnNewBlock();
            GridSignals.onDestroyDestroyableBlocks();

            yield return _delayInstraction;

            GridSignals.onIsLevelPassable();
        }

        private void ResetReferences()
        {
            _blockManager = null;
        }
    }
}
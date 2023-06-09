using Assets.Scripts.Game.GridCell;
using UnityEngine;

namespace Assets.Scripts.Game.Block
{
    public class BlockController : MonoBehaviour
    {
        private const float _maxRayDistance = 200f;
        private bool _isSnapAble;
        private Vector3 _snapPos;
        private GridCellController _cell;
        private Transform _myTransform;

        private void Awake()
        {
            _myTransform = transform;
        }

        public void CastRay()
        {
            Vector3 middlePoint = _myTransform.position + _myTransform.forward * 0.5f;

            Ray ray = new Ray(middlePoint, _myTransform.TransformDirection(Vector3.forward));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _maxRayDistance))
            {
                if (hit.transform.CompareTag("Block"))
                {
                    _isSnapAble = false;
                    _cell = null;
                    _snapPos = _myTransform.localPosition;
                }
                else if (hit.transform.TryGetComponent(out GridCellController gridCellController))
                {
                    _cell = gridCellController;

                    if (_cell != null && _cell.State == GridCellState.Out)
                    {
                        Vector3 cellPosition = _cell.transform.position;

                        float round_x = Mathf.Round(cellPosition.x);
                        float round_y = Mathf.Round(cellPosition.y);
                        int offSet_z = -1;

                        _isSnapAble = true;
                        _snapPos = new Vector3(round_x, round_y, offSet_z);
                    }
                }
            }
        }

        public bool IsSnapAble() => _isSnapAble;

        public void SnaptoGrid()
        {
            _cell.AttachToBlock(this);
            _myTransform.position = _snapPos;
        }
    }
}
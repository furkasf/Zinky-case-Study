using Game.Events;
using UnityEngine;

namespace Assets.Scripts.Game.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _restartPanel;

        private void OnEnable()
        {
            LevelSignal.onOpenRestartPanel += OpenRestartPanel;
        }

        private void OnDisable()
        {
            LevelSignal.onOpenRestartPanel += OpenRestartPanel;
        }

        private void OpenRestartPanel()
        {
            SpawnSignal.onDestroyAllBlocks();
            _restartPanel.SetActive(true);
        }

        public void CloseResetPanel()
        {
            _restartPanel.SetActive(false);
            LevelSignal.onLevelReset();
            SpawnSignal.onSpawnNewBlock();
        }
    }
}
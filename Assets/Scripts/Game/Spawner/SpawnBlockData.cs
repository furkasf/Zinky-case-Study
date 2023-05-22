using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Spawner
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "GameData/SpawnData")]
    public class SpawnBlockData : ScriptableObject
    {
        public List<GameObject> Prefabs;
    }
}
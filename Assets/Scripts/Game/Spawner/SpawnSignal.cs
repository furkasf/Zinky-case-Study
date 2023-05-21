using Assets.Scripts.Game.Block;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Game.Spawner
{
    public static class SpawnSignal
    {
        public static Action onSpawnNewBlock;
        public static Func<List<BlockManager>> onGetBlockManagers;
    }
}
using System;
using System.Collections.Generic;
using Assets.Scripts.Game.Block;

namespace Game.Events
{
    public static class SpawnSignal
    {
        public static Action onSpawnNewBlock;
        public static Func<List<BlockManager>> onGetBlockManagers;
    }
}
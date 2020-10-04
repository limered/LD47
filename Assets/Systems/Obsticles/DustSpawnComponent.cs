using System.Collections.Generic;
using SystemBase;
using UnityEngine;

namespace Assets.Systems.Obsticles
{
    public class DustSpawnComponent : GameComponent
    {
        public List<GameObject> DustPrefabs;
        public int DustCount;
    }
}
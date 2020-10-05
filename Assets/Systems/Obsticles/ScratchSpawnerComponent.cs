using System.Collections.Generic;
using SystemBase;
using UnityEngine;

namespace Assets.Systems.Obsticles
{
    public class ScratchSpawnerComponent : GameComponent
    {
        public int InverseSpawnPropability = 1000;
        public int MaxScratchCount = 2;

        public BoxCollider SpawnerBox;
        public GameComponent Plate;
        public List<GameComponent> ScratchPrefabs;
        public ParticleSystem ScratchEffect;
        public int CurrentScratchCount;
        public int ParticleCount = 60;
    }
}

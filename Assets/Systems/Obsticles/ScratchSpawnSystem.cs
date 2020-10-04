using SystemBase;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Systems.Obsticles
{
    [GameSystem]
    public class ScratchSpawnSystem : GameSystem<ScratchSpawnerComponent>
    {
        public override void Register(ScratchSpawnerComponent component)
        {
            SystemUpdate(component)
                .Subscribe(TryToSpawn)
                .AddTo(component);
        }

        private void TryToSpawn(ScratchSpawnerComponent obj)
        {
            var spawnRate = obj.InverseSpawnPropability;
            var shouldSpawn = (int)(Random.value * spawnRate) % spawnRate == 0;

            if (!shouldSpawn || obj.CurrentScratchCount >= obj.MaxScratchCount) return;

            var extends = obj.transform.position - obj.SpawnerBox.size * 0.5f;

            var x = Random.value * obj.SpawnerBox.size.x + extends.x;
            var y = Random.value * obj.SpawnerBox.size.y + extends.y;
            var position = obj.SpawnerBox.bounds.ClosestPoint(new Vector3(x, y));

            var scratchNr = (int)(Random.value * obj.ScratchPrefabs.Count);

            var scratch = Object.Instantiate(obj.ScratchPrefabs[scratchNr], position, Quaternion.identity, obj.Plate.transform);
            obj.CurrentScratchCount++;

            scratch.gameObject.OnDestroyAsObservable()
                .Subscribe(_ => obj.CurrentScratchCount--);
        }
    }
}

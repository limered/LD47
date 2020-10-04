using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemBase;
using Systems.GameState.Messages;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Systems.Obsticles
{
    [GameSystem]
    public class DustSpawnSystem : GameSystem<DustSpawnComponent>
    {
        public override void Register(DustSpawnComponent component)
        {
            //MessageBroker.Default.Receive<GameMsgStart>()
            //    .Subscribe(_ => StartSpawning(component))
            //    .AddTo(component);

            StartSpawning(component);
        }

        private void StartSpawning(DustSpawnComponent component)
        {
            SystemUpdate(component).Subscribe(SpawnDust).AddTo(component);
        }

        private void SpawnDust(DustSpawnComponent component)
        {
            const int perFrame = 150;
            var shouldSpawn = (int)(Random.value * perFrame) % perFrame == 0;
            if (!shouldSpawn) return;

            var pos = component.transform.position;
            var rand = (int)(Random.value * 4);
            var position = Random.insideUnitCircle * 4f + new Vector2(pos.x, pos.y);

            var obj = Object.Instantiate(component.DustPrefabs[rand],
                new Vector3(position.x, position.y, pos.z), Quaternion.identity);
            obj.GetComponent<DustComponent>().TargetLocation.Value = new Vector3(position.x,  position.y);
        }
    }
}

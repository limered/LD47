using GameState.States;
using SystemBase;
using SystemBase.StateMachineBase;
using Systems;
using UniRx;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Systems.Obsticles
{
    [GameSystem]
    public class DustSpawnSystem : GameSystem<DustSpawnComponent>
    {
        public override void Register(DustSpawnComponent component)
        {
            SystemUpdate(component)
                .Where(_ => IoC.Game.GameStateContext.CurrentState.Value is Running)
                .Subscribe(SpawnDust)
                .AddTo(component);

            IoC.Game.GameStateContext.CurrentState.Where(state => state is Running)
                .Subscribe(RemoveAllDusts)
                .AddTo(component);
        }

        private void RemoveAllDusts(BaseState<Game> obj)
        {
            GameObject[] dusts = GameObject.FindGameObjectsWithTag("Dust");
            foreach (var dust in dusts)
            {
                Object.Destroy(dust);
            }
        }

        private void SpawnDust(DustSpawnComponent component)
        {
            var shouldSpawn = (int)(Random.value * component.InverseDustSpawnPropability) % component.InverseDustSpawnPropability == 0;
            if (!shouldSpawn) return;

            var pos = component.transform.position;
            var rand = (int)(Random.value * 4);
            var posX = Random.value * 2 - 1 + pos.x;
            var posY = Random.value * 2 - 1 + pos.y;

            var obj = Object.Instantiate(component.DustPrefabs[rand],
                new Vector3(posX, posY, pos.z), Quaternion.identity);
            obj.GetComponent<DustComponent>().TargetLocation.Value = new Vector3(posX, posY);
        }
    }
}

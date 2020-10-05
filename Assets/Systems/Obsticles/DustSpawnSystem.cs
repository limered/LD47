using GameState.States;
using SystemBase;
using SystemBase.StateMachineBase;
using Systems;
using Assets.Systems.FinalDays;
using UniRx;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Systems.Obsticles
{
    [GameSystem]
    public class DustSpawnSystem : GameSystem<DustSpawnComponent, StarSpawnComponent>
    {
        public override void Register(DustSpawnComponent component)
        {
            SystemUpdate(component)
                .Where(_ => IoC.Game.GameStateContext.CurrentState.Value is Running)
                .Subscribe(SpawnDust)
                .AddTo(component);
        }

        private void SpawnDust(DustSpawnComponent component)
        {
            var shouldSpawn = (int)(Random.value * component.InverseDustSpawnPropability) % component.InverseDustSpawnPropability == 0;
            if (!shouldSpawn) return;

            var pos = component.transform.position;
            var rand = (int)(Random.value * component.DustPrefabs.Count);
            var posX = Random.value * component.SpawnerSize - component.SpawnerSize*0.5f + pos.x;
            var posY = Random.value * component.SpawnerSize - component.SpawnerSize*0.5f + pos.y;

            var obj = Object.Instantiate(component.DustPrefabs[rand],
                new Vector3(posX, posY, pos.z), Quaternion.identity);
            obj.GetComponent<DustComponent>().TargetLocation.Value = new Vector3(posX, posY);
        }

        public override void Register(StarSpawnComponent component)
        {
            SystemUpdate(component)
                .Where(_ => IoC.Game.GameStateContext.CurrentState.Value is GameOver)
                .Subscribe(SpawnDust)
                .AddTo(component);
        }
    }
}

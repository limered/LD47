using Assets.Systems.HamsterCollision;
using Assets.Systems.Movement;
using GameState.States;
using System;
using SystemBase;
using StrongSystems.Audio;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Systems.Obsticles
{
    [GameSystem]
    public class ScratchSpawnSystem : GameSystem<ScratchSpawnerComponent, HamsterComponent>
    {
        public override void Register(HamsterComponent component) => RegisterWaitable(component);

        public override void Register(ScratchSpawnerComponent component)
        {
            WaitOn<HamsterComponent>()
                .Then(hamster => SystemUpdate(component)
                .Where(_ => IoC.Game.GameStateContext.CurrentState.Value is Running)
                .Do(scratch => TryToSpawn(scratch, hamster)))
            .Subscribe()
                .AddTo(component);
        }

        private void TryToSpawn(ScratchSpawnerComponent obj, HamsterComponent hamster)
        {
            if (obj.ScratchEffect) obj.ScratchEffect.transform.LookAt(obj.transform.position - Quaternion.AngleAxis(-90, Vector3.back) * Hamster.DirectionToVector3(hamster.CurrentDirection.Value), Vector3.back);
            var spawnRate = obj.InverseSpawnPropability;
            var shouldSpawn = (int)(Random.value * spawnRate) % spawnRate == 0;

            if (!shouldSpawn || obj.CurrentScratchCount >= obj.MaxScratchCount) return;

            if (obj.ScratchEffect) obj.ScratchEffect.Emit(new ParticleSystem.EmitParams { }, obj.ParticleCount);

            Observable.Timer(TimeSpan.FromSeconds(obj.SpawnDelay)).Subscribe(__ =>
            {
                var extends = obj.transform.position - obj.SpawnerBox.size * 0.5f;

                var x = Random.value * obj.SpawnerBox.size.x + extends.x;
                var y = Random.value * obj.SpawnerBox.size.y + extends.y;
                var position = obj.SpawnerBox.bounds.ClosestPoint(new Vector3(x, y));

                var scratchNr = (int)(Random.value * obj.ScratchPrefabs.Count);

                var scratch = Object.Instantiate(obj.ScratchPrefabs[scratchNr], position, Quaternion.identity, obj.Plate.transform);
                "scratch".Play();
                obj.CurrentScratchCount++;

                scratch.gameObject.OnDestroyAsObservable()
                    .Subscribe(_ => obj.CurrentScratchCount--);

                Observable.Timer(TimeSpan.FromSeconds(20))
                    .Subscribe(_ => scratch.GetComponent<ScratchComponent>().Remove.Execute())
                    .AddTo(scratch);

            }).AddTo(obj);
        }
    }
}

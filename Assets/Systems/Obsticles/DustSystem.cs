﻿using System;
using SystemBase;
using Assets.Utils.Math;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Systems.Obsticles
{
    [GameSystem]
    public class DustSystem : GameSystem<DustComponent>
    {
        public override void Register(DustComponent component)
        {
            component
                .Jump
                .Select(_ => component)
                .Subscribe(OnDustJump)
                .AddTo(component);

            SystemUpdate(component)
                .Subscribe(OnDustMove)
                .AddTo(component);

            component.OnTriggerEnterAsObservable()
                .Subscribe(coll => OnCollideWithFloor(coll, component))
                .AddTo(component);
        }

        private void OnCollideWithFloor(Collider obj, DustComponent component)
        {
            if (obj.gameObject.layer != LayerMask.NameToLayer("Floor")) return;

            component.IsOnRecord = true;
            component.transform.parent = obj.transform;
        }

        private void OnDustMove(DustComponent comp)
        {
            var distance = comp.TargetLocation.Value.DistanceTo(comp.transform.localPosition);
            if (distance < 0.1f || comp.IsOnRecord) return;

            var newPos = Vector3.Lerp(comp.transform.position, comp.TargetLocation.Value, 0.01f);
            comp.transform.position = newPos;
        }

        private void OnDustJump(DustComponent obj)
        {
            obj.transform.parent = null;
            obj.IsOnRecord = false;

            var pos = obj.transform.position;
            var randXY = (Random.insideUnitCircle + new Vector2(pos.x, pos.y)) * 2;
            var randZ = Math.Abs(Random.value * 3) + 0.2f;
            
            obj.transform.position = new Vector3(pos.x, pos.y, -randZ);
            obj.TargetLocation.Value = new Vector3(randXY.x, randXY.y, 0);
        }
    }
}

using System;
using SystemBase;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Systems.Turntable
{
    [GameSystem]
    public class TurntableSystem : GameSystem<Turntable, Vinyl, Arm>
    {
        readonly ReactiveProperty<Turntable> _turntable = new ReactiveProperty<Turntable>();

        public override void Register(Turntable component)
        {
            _turntable.Value = component;
        }

        public override void Register(Vinyl component)
        {
            component
                .WaitOn(_turntable)
                .SelectMany(turntable =>
                    component.UpdateAsObservable().Do(_ => component.transform.Rotate(Vector3.forward, Time.fixedDeltaTime * turntable.Speed.Value, Space.Self)))
                .Subscribe()
                .AddTo(component);
        }

        public override void Register(Arm component)
        {

        }
    }
}

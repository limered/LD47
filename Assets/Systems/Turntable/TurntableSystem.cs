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
        Turntable turntable;

        public override void Register(Turntable component)
        {
            turntable = component;
        }

        public override void Register(Vinyl component)
        {
            var turntable = component.GetComponent<Turntable>();

            component.UpdateAsObservable()
               .Subscribe(_ => component.transform.Rotate(Vector3.forward, Time.fixedDeltaTime * turntable.Speed.Value, Space.Self))
               .AddTo(component);
        }

        public override void Register(Arm component)
        {

        }
    }
}

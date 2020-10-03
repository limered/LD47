using System;
using SystemBase;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Assets.Systems.Turntable
{
    [GameSystem]
    public class TurntableSystem : GameSystem<Turntable, Vinyl, Arm>
    {

        public override void Register(Turntable component) => RegisterLazy(component);

        public override void Register(Vinyl component)
        {
            WaitOn<Turntable>()
                .ThenOnUpdate(turntable => component.transform.Rotate(component.Axis, Time.fixedDeltaTime * turntable.Speed.Value, Space.Self))
                .AddTo(component);
        }


        public override void Register(Arm component)
        {

        }
    }
}

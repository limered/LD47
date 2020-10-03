using SystemBase;
using UniRx;
using UnityEngine;

namespace Assets.Systems.Turntable
{
    [GameSystem]
    public class TurntableSystem : GameSystem<Turntable, Vinyl, Arm>
    {
        public override void Register(Turntable component) => RegisterWaitable(component);

        public override void Register(Vinyl component)
        {
            WaitOn<Turntable>()
                .ThenOnUpdate(turntable => component
                    .transform
                    .Rotate(component.Axis, Time.deltaTime * turntable.Speed.Value, Space.Self))
                .AddTo(component);
        }

        public override void Register(Arm component)
        {
        }
    }
}

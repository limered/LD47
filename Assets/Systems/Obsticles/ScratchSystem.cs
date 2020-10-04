using SystemBase;
using UniRx;
using UnityEngine;

namespace Assets.Systems.Obsticles
{
    [GameSystem]
    public class ScratchSystem : GameSystem<ScratchComponent>
    {
        public override void Register(ScratchComponent component)
        {
            SystemUpdate(component)
                .Subscribe(AnimateScratch)
                .AddTo(component);
        }

        private void AnimateScratch(ScratchComponent obj)
        {
            var timeModifier = Time.realtimeSinceStartup * 20;

            var xScaleModifier = Mathf.Sin(timeModifier)*0.1f + 1f;
            var yScaleModifier = Mathf.Cos(timeModifier)*0.1f + 1f;

            obj.transform.localScale = new Vector3(xScaleModifier, yScaleModifier, 1);
        }
    }
}

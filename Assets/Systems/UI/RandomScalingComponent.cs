using SystemBase;
using UnityEngine;

namespace Assets.Systems.UI
{
    public class RandomScalingComponent : GameComponent
    {
        public float WobbleStrengthPercent = 0.1f;
        public float WobbleSpeed = 20;

        private Vector3 _basicScale;

        protected override void OverwriteStart()
        {
            _basicScale = transform.localScale;
        }

        private void Update()
        {
            var animationSpeed = Time.realtimeSinceStartup * WobbleSpeed;

            var wobble = _basicScale * WobbleStrengthPercent;

            var xScaleModifier = Mathf.Sin(animationSpeed) * wobble.x + _basicScale.x;
            var yScaleModifier = Mathf.Cos(animationSpeed) * wobble.y + _basicScale.y;

            transform.localScale = new Vector3(xScaleModifier, yScaleModifier, _basicScale.z);
        }
    }
}

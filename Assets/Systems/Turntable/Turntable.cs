using SystemBase;
using UniRx;
using UnityEngine;

namespace Assets.Systems.Turntable
{
    public class Turntable : GameComponent
    {
        [Range(0f, 1f)]
        public FloatReactiveProperty Progress = new FloatReactiveProperty(0);
        public FloatReactiveProperty Speed = new FloatReactiveProperty(5);
    }
}


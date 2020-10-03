using SystemBase;
using UniRx;
using UnityEngine;

namespace Assets.Systems.Turntable
{
    public class Turntable : GameComponent
    {
        public FloatReactiveProperty Speed = new FloatReactiveProperty(30);
    }
}


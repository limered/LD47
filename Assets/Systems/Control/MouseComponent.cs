using SystemBase;
using UniRx;
using UnityEngine;

namespace Assets.Systems.Control
{
    public class MouseComponent : GameComponent
    {
        public GameObject Target;
        public Vector3ReactiveProperty MousePosition = new Vector3ReactiveProperty();
        public BoolReactiveProperty MousePressed = new BoolReactiveProperty();
        public BoolReactiveProperty IsAboweRecord = new BoolReactiveProperty();
    }
}
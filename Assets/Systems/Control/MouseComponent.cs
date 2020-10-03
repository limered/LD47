using SystemBase;
using UniRx;

namespace Assets.Systems.Control
{
    public class MouseComponent : GameComponent
    {
        public Vector3ReactiveProperty MousePosition = new Vector3ReactiveProperty();
        public BoolReactiveProperty MousePressed = new BoolReactiveProperty();
    }
}
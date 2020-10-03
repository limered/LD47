using SystemBase;

namespace Assets.Systems.HamsterCollision
{
    public class HamsterCollisionSystem : GameSystem<HamsterComponent>
    {
        public override void Register(HamsterComponent component)
        {
        }
    }

    public class HamsterComponent : GameComponent
    {
    }
}

using SystemBase;

namespace Assets.Systems.NeedleCollision
{
    public class NeedleCollisionConfigComponent : GameComponent
    {
        public float DustSlowAmount = .05f;
        public double DustSlowLength = 1f;
    }
}

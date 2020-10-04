using SystemBase;

namespace Assets.Systems.NeedleCollision
{
    public class NeedleCollisionConfigComponent : GameComponent
    {
        public float DustSlowAmount = .02f;
        public double DustSlowLength = 1f;
        public float ScratchJumpAmount = .05f;
    }
}

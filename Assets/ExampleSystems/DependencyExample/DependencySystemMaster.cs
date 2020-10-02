using SystemBase;
using ExampleSystems.Example;

namespace ExampleSystems.DependencyExample
{
    [GameSystem]
    public class DependencySystemMaster : GameSystem<FunnyMovementComponent>
    {
        public override void Register(FunnyMovementComponent component)
        {
        }
    }
}

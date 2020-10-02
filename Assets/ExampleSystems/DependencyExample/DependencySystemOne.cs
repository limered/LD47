using SystemBase;
using ExampleSystems.Example;

namespace ExampleSystems.DependencyExample
{
    [GameSystem(typeof(DependencySystemMaster))]
    public class DependencySystemOne : GameSystem<FunnyMovementComponent>
    {
        public override void Register(FunnyMovementComponent component)
        {
        }
    }
}

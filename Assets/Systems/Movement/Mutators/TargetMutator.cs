using Assets.Utils.Math;
using UnityEngine;

namespace Assets.Systems.Movement.Mutators
{
    public class TargetMutator : MovementMutator
    {
        [Range(0, 100)]
        public float Acceleration;
        [Range(0.0f, 0.5f)]
        public float MaxSpeed;
        [Range(0, 50)]
        public float MaxDistance;
        public GameObject Target;

        public override void Mutate(bool canMove, Vector3 oldDirection, float oldSpeed, out Vector3 newDirection, out float newSpeed)
        {
            if (Target && canMove)
            {
                var targetDirection = transform.position.DirectionTo(Target.transform.position);
                var speed = Acceleration * (transform.position.DistanceTo(Target.transform.position) / MaxDistance) * Time.deltaTime;
                speed = Mathf.Min(speed, MaxSpeed);

                newDirection = targetDirection;
                newSpeed = speed;
            }
            else 
            {
                base.Mutate(canMove, oldDirection, oldSpeed, out newDirection, out newSpeed);
            }
        }
    }
}
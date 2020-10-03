using System;
using Assets.Utils.Math;
using UnityEngine;

namespace Assets.Systems.Movement.Mutators
{
    public class RunAwayMutator : MovementMutator
    {
        public float Acceleration;
        public float MaxSpeed;
        public float MaxDistance;
        public GameObject Source;

        public override void Mutate(bool canMove, Vector3 oldDirection, float oldSpeed, out Vector3 newDirection, out float newSpeed)
        {
            if (Source)
            {
                var direction = Source.transform.position.DirectionTo(transform.position).XY();
                var distance = Source.transform.position.DistanceTo(transform.position);

                var speed = (MaxDistance / distance) * Acceleration * Time.deltaTime;
                speed = Mathf.Min(speed, MaxSpeed);

                var angle = VectorUtils.Angle(direction, oldDirection.XY());

                if (Math.Abs(oldDirection.sqrMagnitude) < float.Epsilon)
                {
                    newDirection = new Vector3(direction.x, direction.y);
                    newSpeed = speed;
                }
                else
                {
                    var newDir = oldDirection.XZ().Rotate(angle);
                    newDirection = new Vector3(newDir.x, newDir.y);
                    newSpeed = oldSpeed + speed;
                }
            }
            else
            {
                base.Mutate(canMove, oldDirection, oldSpeed, out newDirection, out newSpeed);
            }
        }
    }
}

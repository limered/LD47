using System;
using UnityEngine;

namespace Assets.Systems.Movement.Mutators
{
    public class CollisionMutator : MovementMutator
    {
        public LayerMask CollidesWith = new LayerMask { value = 0x7FFFFFFF };
        public float ObstaclePaddig;
        public Func<Vector3, float, bool> CanMoveInDirection { get; set; }

        public override void Mutate(bool canMove, Vector3 oldDirection, float oldSpeed, out Vector3 newDirection, out float newSpeed)
        {
            if (CanMoveInDirection != null)
            {
                if (CanMoveInDirection(oldDirection, oldSpeed + ObstaclePaddig))
                {
                    newDirection = oldDirection;
                    newSpeed = oldSpeed;
                }
                else
                {
                    newDirection = oldDirection;
                    newSpeed = 0f;
                }
            }
            else //if nothing is defined for 'CanMoveInDirection' dont alter anything
            {
                base.Mutate(canMove, oldDirection, oldSpeed, out newDirection, out newSpeed);
            }
        }
    }
}
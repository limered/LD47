using System.Collections.Generic;
using SystemBase;
using Assets.Systems.Movement.Mutators;
using UniRx;
using UnityEngine;

namespace Assets.Systems.Movement
{
    public class MovementComponent : GameComponent
    {
        public List<MovementMutator> MovementMutators;
        public BoolReactiveProperty CanMove = new BoolReactiveProperty();

        public Vector3ReactiveProperty Direction = new Vector3ReactiveProperty(Vector3.zero);
        public FloatReactiveProperty Speed = new FloatReactiveProperty(0);
        public Vector3ReactiveProperty CurrentVelocity = new Vector3ReactiveProperty(Vector3.zero);
    }
}
using SystemBase;
using UniRx;
using UnityEngine;

namespace Assets.Systems.HamsterCollision
{
    public class HamsterComponent : GameComponent
    {
        public float RotationSpeed;
        public bool IsUpright = true;
        public Vector3 Axis;
        public GameObject RecordModel;
        public GameObject HamsterModel;
        public ReactiveProperty<Hamster.HamsterDirection> CurrentDirection = new ReactiveProperty<Hamster.HamsterDirection>();

        public SpriteRenderer NormalSprite;
        public SpriteRenderer RotatingSprite;
    }
}
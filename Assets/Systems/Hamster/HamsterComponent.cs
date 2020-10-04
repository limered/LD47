using SystemBase;
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
        public GameObject BroomModel;
        public GameObject HammerModel;
    }
}
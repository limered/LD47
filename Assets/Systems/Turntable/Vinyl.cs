﻿using SystemBase;
using UnityEngine;

namespace Assets.Systems.Turntable
{
    public class Vinyl : GameComponent
    {
        public Vector3 Axis = new Vector3(0, 1, 0);
        public GameObject VinylAnimationGameObject;
    }
}

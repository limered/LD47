﻿using System;
using SystemBase;
using UniRx;
using UnityEngine;

namespace Assets.Systems.Obsticles
{
    public class DustComponent : GameComponent
    {
        public Collider Collider;
        public ReactiveCommand Jump = new ReactiveCommand();
        public Vector3ReactiveProperty TargetLocation = new Vector3ReactiveProperty();
        public BoolReactiveProperty IsOnRecord = new BoolReactiveProperty();
        public IDisposable DyingDisposable { get; set; }
    }
}

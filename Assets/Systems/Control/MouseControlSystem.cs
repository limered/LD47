﻿using System;
using SystemBase;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Systems.Control
{
    [GameSystem]
    public class MouseControlSystem : GameSystem<MouseComponent>
    {
        public override void Register(MouseComponent component)
        {
            component.UpdateAsObservable()
                //.Where(u => IoC.Game.GameStateContext.CurrentState.Value.GetType() == typeof(Running))
                .Subscribe(CheckMousePorition(component))
                .AddTo(component);
        }

        private static Action<Unit> CheckMousePorition(MouseComponent component)
        {
            var floorLayer = LayerMask.NameToLayer("Floor");

            return u =>
            {
                if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
                {
                    component.MousePressed.Value = true;
                }
                if (Input.GetMouseButtonUp((int)MouseButton.LeftMouse))
                {
                    component.MousePressed.Value = false;
                }

                if (!Input.GetMouseButton((int)MouseButton.LeftMouse)) return;

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, 1 << floorLayer))
                {
                    component.MousePosition.Value = new Vector3(hit.point.x, 0, hit.point.z);
                }
            };
        }
    }
}

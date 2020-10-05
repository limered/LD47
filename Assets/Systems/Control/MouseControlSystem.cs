using Assets.Systems.UI;
using GameState.States;
using System;
using SystemBase;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

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

            MessageBroker.Default.Receive<PlayButtonClickedEvent>().Subscribe(_ => RollIn(component))
                .AddTo(component);
        }

        private static Action<Unit> CheckMousePorition(MouseComponent component)
        {
            var floorLayer = LayerMask.NameToLayer("Floor");
            var recordLayer = LayerMask.NameToLayer("Record");

            return u =>
            {
                if (IoC.Game.GameStateContext.CurrentState.Value is StartScreen)
                {
                    return;
                }

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

                if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, 1 << floorLayer)) return;
                //if (!Physics.Raycast(ray, out _, Mathf.Infinity, 1 << recordLayer)) return;

                component.MousePosition.Value = new Vector3(hit.point.x, hit.point.y);
                component.transform.position = component.MousePosition.Value;
            };
        }

        private void RollIn(MouseComponent comp)
        {
            comp.Target.transform.position = comp.MousePosition.Value;
        }
    }
}

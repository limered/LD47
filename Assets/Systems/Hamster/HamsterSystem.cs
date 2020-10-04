using SystemBase;
using Assets.Systems.Obsticles;
using Assets.Systems.Tools.Actions;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Assets.Utils.Math;
using GameState.States;
using UnityEngine.UIElements;
using Utils;

namespace Assets.Systems.HamsterCollision
{
    [GameSystem]
    public class HamsterSystem : GameSystem<HamsterComponent, Turntable.Turntable>
    {
        public override void Register(HamsterComponent component)
        {
            component
                .OnTriggerEnterAsObservable()
                .Subscribe(coll => Collider(coll, component))
                .AddTo(component);

            WaitOn<Turntable.Turntable>()
                .ThenOnUpdate(tTable => ResetRotation(component, tTable))
                .AddTo(component);

            SystemUpdate(component)
                .Subscribe(SetAnimationByPosition)
                .AddTo(component);

            SystemUpdate(component)
                .Where(_ => Input.GetMouseButtonDown((int)MouseButton.RightMouse))
                .Subscribe(CheckForToolSwitch)
                .AddTo(component);
        }

        private void CheckForToolSwitch(HamsterComponent obj)
        {
            var tools = obj.GetComponentInChildren<ToolComponent>();
            var nxtTool = tools.CurrentTool == CurrentTool.Broom ? CurrentTool.Hammer : CurrentTool.Broom;
            MessageBroker.Default.Publish(new SelectToolAction{ToolToSelect = nxtTool});
        }

        private void ResetRotation(HamsterComponent comp, Turntable.Turntable table)
        {
            if (!comp.IsUpright || !(IoC.Game.GameStateContext.CurrentState.Value is Running)) return;
            comp.transform
                .Rotate(comp.Axis, Time.deltaTime * table.Speed.Value, Space.Self);
        }

        private void Collider(Collider other, HamsterComponent hamster)
        {
            var tool = hamster.GetComponentInChildren<ToolComponent>();

            if (tool.CurrentTool == CurrentTool.Broom)
            {
                var dust = other.GetComponent<DustComponent>();
                if (dust)
                {
                    MoveDustToAnotherPlace(dust);
                }
            }
            else if (tool.CurrentTool == CurrentTool.Hammer)
            {
                var scratch = other.GetComponent<ScratchComponent>();
                if (scratch)
                {
                    DeleteScratch(scratch);
                }
            }
        }

        private void DeleteScratch(ScratchComponent scratch)
        {
            MessageBroker.Default.Publish(new CleanUpEvent());
            scratch.Remove.Execute();
        }

        private void MoveDustToAnotherPlace(DustComponent dust)
        {
            MessageBroker.Default.Publish(new CleanUpEvent());
            dust.Jump.Execute();
        }

        public override void Register(Turntable.Turntable component)
        {
            RegisterWaitable(component);
        }

        private void SetAnimationByPosition(HamsterComponent comp)
        {
            var animator = comp.HamsterModel.GetComponent<Animator>();

            var recordPosition = comp.RecordModel.transform.position;
            var hamsterPosition = comp.transform.position;
            var directionToHamster = recordPosition.DirectionTo(hamsterPosition).normalized;
            var line = new Vector2(1, 0);

            var angle = Vector2.Angle(directionToHamster, line);
            var top = directionToHamster.y > 0;

            if(angle <= 22.5f && angle >= 0f)
            {
                animator.Play(Hamster.Move.Up);
            } else if(angle <= 67.5f && angle >= 22.5f) {
                animator.Play(top ? Hamster.Move.LeftUp : Hamster.Move.RightUp);
            } else if(angle <= 112.5f && angle >= 67.5f)
            {
                animator.Play(top ? Hamster.Move.Left : Hamster.Move.Right);
            } else if(angle <= 157.5f && angle >= 112.5f)
            {
                animator.Play(top ? Hamster.Move.LeftDown : Hamster.Move.RightDown);
            } else if(angle <= 202.5f && angle >= 157.5f)
            {
                animator.Play(Hamster.Move.Down);
            }
        }
    }
}

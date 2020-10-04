using SystemBase;
using Assets.Systems.Tools.Actions;
using UniRx;
using UnityEngine;

[GameSystem]
public class ToolSystem : GameSystem<ToolComponent, ToolButton>
{
    public override void Register(ToolComponent component)
    {
        RegisterWaitable(component);
        MessageBroker.Default.Receive<CleanUpEvent>()
            .Subscribe(_ => AnimateCleaning(component))
            .AddTo(component);

        MessageBroker.Default.Receive<SelectToolAction>()
            .Subscribe(newTool => OnSelectTool(component, newTool))
            .AddTo(component);
    }

    private void OnSelectTool(ToolComponent component, SelectToolAction obj)
    {
        SelectTool(component, obj.ToolToSelect);
    }

    public override void Register(ToolButton component)
    {
        WaitOn<ToolComponent>().Subscribe(toolComponent => component.ToolComponent = toolComponent).AddTo(component);
    }

    private void AnimateCleaning(ToolComponent comp)
    {
        if(comp.CurrentTool == CurrentTool.Broom)
        {
            var animator = comp.BroomModel.GetComponent<Animator>();
            animator.runtimeAnimatorController.animationClips[1].wrapMode = WrapMode.Once;

            animator.Play(Hamster.Clean.Broom);
        }
        else if(comp.CurrentTool == CurrentTool.Hammer)
        {
            var animator = comp.HammerModel.GetComponent<Animator>();
            animator.Play(Hamster.Clean.Hammer);
        }
    }

    private void SelectTool(ToolComponent comp, CurrentTool tool)
    {
        comp.CurrentTool = tool;
        if (comp.CurrentTool == CurrentTool.Broom)
        {
            comp.HammerModel.SetActive(false);
            comp.BroomModel.SetActive(true);
        }
        else if (comp.CurrentTool == CurrentTool.Hammer)
        {
            comp.BroomModel.SetActive(false);
            comp.HammerModel.SetActive(true);
        } else
        {
            comp.HammerModel.SetActive(false);
            comp.BroomModel.SetActive(false);
        }
    }
}

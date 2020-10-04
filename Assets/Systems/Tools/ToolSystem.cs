using SystemBase;
using UniRx;
using UnityEngine;

[GameSystem]
public class ToolSystem : GameSystem<ToolComponent>
{
    public override void Register(ToolComponent component)
    {
        MessageBroker.Default.Receive<CleanUpEvent>().Subscribe(_ => AnimateCleaning(component));

        //TODO select tool
        SelectTool(component, CurrentTool.Broom);
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

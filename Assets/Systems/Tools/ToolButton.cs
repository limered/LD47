using Assets.Systems.Tools.Actions;
using SystemBase;
using Systems.GameState.Messages;
using UniRx;
using UnityEngine;

public class ToolButton : GameComponent
{
    public ToolComponent ToolComponent { get; internal set; }
    public Sprite[] ButtonSprites;

    public void OnMouseDown()
    {
        var toolToSelect = ToolComponent.CurrentTool == CurrentTool.Broom ? CurrentTool.Hammer : CurrentTool.Broom;

        MessageBroker.Default.Publish(
            new SelectToolAction {
                ToolToSelect = toolToSelect
            });
        OnSelectTool(toolToSelect);
    }

    protected override void OverwriteStart()
    {
        MessageBroker.Default.Receive<SelectToolAction>()
            .Subscribe(newTool => OnSelectTool(newTool.ToolToSelect))
            .AddTo(this);
    }

    public void OnSelectTool(CurrentTool tool)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = tool == CurrentTool.Broom ? ButtonSprites[0] : ButtonSprites[1];
    }
}

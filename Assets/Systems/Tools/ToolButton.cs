using Assets.Systems.Tools.Actions;
using SystemBase;
using UniRx;
using UnityEngine;

public class ToolButton : GameComponent
{
    public ToolComponent ToolComponent { get; internal set; }
    public Sprite[] ButtonSprites;

    public void OnMouseDown()
    {
        MessageBroker.Default.Publish(
            new SelectToolAction { 
                ToolToSelect = ToolComponent.CurrentTool == CurrentTool.Broom ? CurrentTool.Hammer : CurrentTool.Broom
            });
        gameObject.GetComponent<SpriteRenderer>().sprite = ToolComponent.CurrentTool == CurrentTool.Broom ? ButtonSprites[0] : ButtonSprites[1];
    }
}

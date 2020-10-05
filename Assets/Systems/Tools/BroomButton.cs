using Assets.Systems.Tools.Actions;
using SystemBase;
using UniRx;
using UnityEngine;

public class BroomButton : GameComponent
{
    public void OnMouseDown()
    {
        MessageBroker.Default.Publish(
            new SelectToolAction
            {
                ToolToSelect = CurrentTool.Broom
            });
    }
}

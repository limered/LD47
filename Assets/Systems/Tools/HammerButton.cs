using Assets.Systems.Tools.Actions;
using SystemBase;
using UniRx;

public class HammerButton : GameComponent
{
    public void OnMouseDown()
    {
        MessageBroker.Default.Publish(
            new SelectToolAction
            {
                ToolToSelect = CurrentTool.Hammer
            });
    }
}

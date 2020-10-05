using SystemBase;
using UnityEngine;

public class ToolComponent : GameComponent
{
    public CurrentTool CurrentTool;
    public GameObject BroomModel;
    public GameObject HammerModel;
}
public enum CurrentTool
{
    Broom,
    Hammer,
    None
}

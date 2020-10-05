using SystemBase;
using UniRx;
using UnityEngine;
using SystemBase.StateMachineBase;
using GameState.States;
using Utils;

public class FinalDisplayComponent : GameComponent
{
    public GameObject[] Objects;
    public GameObject[] ObjectsToHide;

    protected override void OverwriteStart()
    {
        IoC.Game.GameStateContext.CurrentState
                .Where(state => state is GameOver)
                .Subscribe(_ => OnStartFinalDays())
                .AddTo(this);

        IoC.Game.GameStateContext.CurrentState
            .Where(state => state is Running)
            .Subscribe(_ => OnEndFinalDays())
            .AddTo(this);
    }

    private void OnStartFinalDays()
    {
        foreach(GameObject ObjectToDisplay in Objects)
        {
            ObjectToDisplay.SetActive(true);
        }

        foreach (GameObject ObjectToDisplay in ObjectsToHide)
        {
            ObjectToDisplay.SetActive(false);
        }
    }

    private void OnEndFinalDays()
    {
        foreach (GameObject ObjectToHide in Objects)
        {
            ObjectToHide.SetActive(false);
        }

        foreach (GameObject ObjectToHide in ObjectsToHide)
        {
            ObjectToHide.SetActive(true);
        }
    }
}

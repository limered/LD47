using SystemBase;
using UniRx;
using UnityEngine;
using SystemBase.StateMachineBase;
using GameState.States;
using Utils;

public class FinalDisplayComponent : GameComponent
{
    public GameObject[] Objects;

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
    }

    private void OnEndFinalDays()
    {
        foreach (GameObject ObjectToHide in Objects)
        {
            ObjectToHide.SetActive(false);
        }
    }
}

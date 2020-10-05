using GameState.States;
using SystemBase;
using SystemBase.StateMachineBase;
using Systems.GameState.Messages;
using StrongSystems.Audio.Helper;
using UniRx;
using UnityEngine;
using Utils;

namespace Systems
{
    public class Game : GameBase
    {
        public readonly StateContext<Game> GameStateContext = new StateContext<Game>();

        private void Awake()
        {
            IoC.RegisterSingleton(this);

            GameStateContext.Start(new Loading());

            InstantiateSystems();

            Init();

            MessageBroker.Default.Publish(new GameMsgFinishedLoading());
        }

        private void Start()
        {
            //MessageBroker.Default.Publish(new GameMsgStart());
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
        }

        public override void Init()
        {
            base.Init();

            IoC.RegisterSingleton<ISFXComparer>(()=> new SFXComparer());
        }
    }
}
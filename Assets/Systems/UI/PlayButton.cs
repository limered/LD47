using GameState.States;
using SystemBase;
using UniRx;
using UnityEngine;
using Utils;

namespace Assets.Systems.UI
{
    public class PlayButton : GameComponent
    {
        public GameObject Play;
        public Sprite[] ButtonSprites;

        public void OnMouseDown()
        {
            MessageBroker.Default.Publish(new PlayButtonClickedEvent());
            Play.GetComponent<SpriteRenderer>().sprite = ButtonSprites[1];
        }
        protected override void OverwriteStart()
        {
            IoC.Game.GameStateContext.CurrentState.Where(state => state is StartScreen || state is GameOver).Subscribe(_ => ResetSprite());
        }

        private void ResetSprite()
        {
            Play.GetComponent<SpriteRenderer>().sprite = ButtonSprites[0];
        }
    }
}

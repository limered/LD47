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
            var animator = Play.GetComponentInChildren<Animator>();
            animator.Play(Hamster.PlayBlink.Init);
        }
        protected override void OverwriteStart()
        {
            var animator = Play.GetComponentInChildren<Animator>();
            animator.Play(Hamster.PlayBlink.Blink);

            IoC.Game.GameStateContext.CurrentState.Where(state => state is StartScreen || state is GameOver).Subscribe(_ => ResetSprite());
        }

        private void ResetSprite()
        {
            Play.GetComponent<SpriteRenderer>().sprite = ButtonSprites[0];
            var animator = Play.GetComponentInChildren<Animator>();
            animator.Play(Hamster.PlayBlink.Blink);
        }
    }
}

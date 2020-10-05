using SystemBase;
using UniRx;
using UnityEngine;

namespace Assets.Systems.UI
{
    public class PlayButton : GameComponent
    {
        public GameObject Play;
        public Sprite ButtonPressedSprite;

        public void OnMouseDown()
        {
            MessageBroker.Default.Publish(new PlayButtonClickedEvent());
            Play.GetComponent<SpriteRenderer>().sprite = ButtonPressedSprite;
        }
    }
}

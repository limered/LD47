using UniRx;
using UnityEngine;

namespace Assets.Systems.UI
{
    public class PlayButton : MonoBehaviour
    {
        public void OnMouseDown()
        {
            MessageBroker.Default.Publish(new PlayButtonClickedEvent());
        }
    }
}

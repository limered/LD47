using Systems.GameState.Messages;
using UniRx;
using UnityEngine;

namespace Assets
{
    public class PlayButton : MonoBehaviour
    {
        public void OnMouseDown()
        {
            MessageBroker.Default.Publish(new GameMsgStart());
        }
    }
}

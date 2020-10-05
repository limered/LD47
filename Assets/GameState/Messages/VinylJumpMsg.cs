namespace Assets.GameState.Messages
{
    public class VinylJumpMsg
    {
        public float JumpAmountInPercent { get; }

        public VinylJumpMsg(float jumpAmountInPercent)
        {
            JumpAmountInPercent = jumpAmountInPercent;
        }
    }
}
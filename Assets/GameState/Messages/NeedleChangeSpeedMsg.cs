namespace Assets.GameState.Messages
{
    public class NeedleChangeSpeedMsg
    {
        public float SpeedChangeAmount { get; }

        public NeedleChangeSpeedMsg(float speedChangeAmount)
        {
            SpeedChangeAmount = speedChangeAmount;
        }
    }
}
using SystemBase;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(Animator))]
public class HamsterAnimator : GameComponent
{
    public StringReactiveProperty Move = new StringReactiveProperty();
}

public static class Hamster
{
    public static class Move
    {
        public const string Up = "hinten_hamster";
        public const string Down = "vorne_hamster";
        public const string Left = "links_hamster";
        public const string LeftUp = "links_hoch_hamster";
        public const string LeftDown = "links_runter_hamster";
        public const string Right = "rechts_hamster";
        public const string RightUp = "rechts_hoch_hamster";
        public const string RightDown = "rechts_runter_hamster";
    }
}

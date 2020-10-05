using SystemBase;
using UnityEngine;
using UniRx;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class HamsterAnimator : GameComponent
{
    public StringReactiveProperty Move = new StringReactiveProperty();
    public StringReactiveProperty Clean = new StringReactiveProperty();
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

    public static string DirectionToAnimation(HamsterDirection dir)
    {
        return new Dictionary<HamsterDirection, string>{
                { HamsterDirection.Up, Move.Up},
                { HamsterDirection.Down, Move.Down},
                { HamsterDirection.Left, Move.Left},
                { HamsterDirection.LeftUp, Move.LeftUp},
                { HamsterDirection.LeftDown, Move.LeftDown},
                { HamsterDirection.Right, Move.Right},
                { HamsterDirection.RightUp, Move.RightUp},
                { HamsterDirection.RightDown, Move.RightDown},
            }[dir];
    }

    public enum HamsterDirection
    {
        Up, Down, Left, LeftUp, LeftDown, Right, RightUp, RightDown
    }

    public static Vector3 DirectionToVector3(HamsterDirection dir)
    {
        return new Dictionary<HamsterDirection, Vector3>{
                { HamsterDirection.Up, new Vector3(0, 1, 0).normalized},
                { HamsterDirection.Down, new Vector3(0, -1, 0).normalized},
                { HamsterDirection.Left, new Vector3(-1, 0, 0).normalized},
                { HamsterDirection.LeftUp, new Vector3(-1, 1, 0).normalized},
                { HamsterDirection.LeftDown, new Vector3(-1, -1, 0).normalized},
                { HamsterDirection.Right, new Vector3(1, 0, 0).normalized},
                { HamsterDirection.RightUp, new Vector3(1, 1, 0).normalized},
                { HamsterDirection.RightDown, new Vector3(1, -1, 0).normalized},
            }[dir];
    }

    public static class Clean
    {
        public const string Broom = "broom_clean";
        public const string Hammer = "hammer_clean";
    }
}

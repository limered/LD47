using System;

namespace SystemBase
{
    public interface IGameSystem
    {
        Type[] ComponentsToRegister { get; }

        void Init();

        void RegisterComponent(GameComponent component);
    }
}

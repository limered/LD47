using UnityEngine;

namespace Utils.Unity
{
    public static class GameObjectExtensions
    {
        public static bool TryGetComponent<TComp>(this GameObject go, out TComp component) where TComp : Component
        {
            component = go.GetComponent<TComp>();
            return component != null;
        }
    }
}
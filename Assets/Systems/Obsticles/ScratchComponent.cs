using SystemBase;
using UnityEngine;

namespace Assets.Systems.Obsticles
{
    public class ScratchComponent : GameComponent
    {
        private void Update()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -0.01f, transform.localPosition.z);
        }
    }
}

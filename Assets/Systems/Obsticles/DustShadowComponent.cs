using UnityEngine;

namespace Assets.Systems.Obsticles
{
    public class DustShadowComponent : MonoBehaviour
    {
        private GameObject shadowGameobject;

        public bool IsCopy;

        private void Start()
        {
            if (IsCopy) return;
            shadowGameobject = Instantiate(this.gameObject, transform);
            shadowGameobject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.1f);
            shadowGameobject.GetComponent<DustShadowComponent>().IsCopy = true;
        }

        private void LateUpdate()
        {
            if (IsCopy) return;

            var pos = transform.position;
            shadowGameobject.transform.position = new Vector3(pos.x, pos.y, -0.12f);
        }
    }
}

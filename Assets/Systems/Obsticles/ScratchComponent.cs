using System.Collections;
using SystemBase;
using UniRx;
using UnityEngine;

namespace Assets.Systems.Obsticles
{
    public class ScratchComponent : GameComponent
    {
        public ReactiveCommand Remove = new ReactiveCommand();

        private void Update()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -0.01f, transform.localPosition.z);
        }

        public void StartFadeout()
        {
            StartCoroutine(FadeRoutine(GetComponentInChildren<SpriteRenderer>()));
        }

        private IEnumerator FadeRoutine(SpriteRenderer spR)
        {
            for (var i = 0; i < 300; i++)
            {
                spR.color = new Color(spR.color.r, spR.color.g, spR.color.b, spR.color.a * 0.9f);
                yield return new WaitForSeconds(0.001f);
            }
        }
    }
}

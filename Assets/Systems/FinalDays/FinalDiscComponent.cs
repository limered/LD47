using System;
using System.Collections;
using SystemBase;
using UnityEngine;

namespace Assets.Systems.FinalDays
{
    public class FinalDiscComponent : GameComponent
    {
        public AudioSource EndMusic;

        public Vector3 startSize;
        public Vector3 endSize;
        public IDisposable ColorDisposable { get; set; }

        protected override void OverwriteStart()
        {
            startSize = transform.localScale;
        }

        public void StartResize()
        {
            StartCoroutine(ResizeAnimation(this));
        }

        private IEnumerator ResizeAnimation(FinalDiscComponent element)
        {
            var currentScale = element.transform.localScale;
            for (var i = 0; i < 90; i++)
            {
                var scaleDelta = Vector3.Lerp(currentScale, element.endSize, i / 90f);

                element.transform.localScale = currentScale + scaleDelta;

                yield return new WaitForSeconds(0.0333f);
            }
        }
    }
}
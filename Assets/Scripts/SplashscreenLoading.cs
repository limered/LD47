using UnityEngine;
using UnityEditor;
using UniRx;
using System;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class SplashscreenLoading : MonoBehaviour
    {
        public float loadTime = 5f;
        public bool autoplay = false;
        public GameObject progressBar;
        public GameObject playButton;

        private void Start()
        {
            Observable.Timer(TimeSpan.FromSeconds(loadTime))
                .DoOnSubscribe(() =>
                {
                    
                    
                    progressBar?.SetActive(true);
                    playButton?.SetActive(false);
                })
                .Subscribe(_ =>
                {
                    progressBar?.SetActive(autoplay || false);
                    playButton?.SetActive(!autoplay);
                    
                    if (autoplay) StartGame();
                })
                .AddTo(this);
        }

        public void StartGame()
        {
            SceneManager.LoadScene("main", LoadSceneMode.Single);
        }
    }
}
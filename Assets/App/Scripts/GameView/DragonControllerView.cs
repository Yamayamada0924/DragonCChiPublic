using System;
using UnityEngine;

namespace App.Scripts.GameView
{
    public class DragonControllerView : MonoBehaviour
    {
        [SerializeField] private Animator dragonAnimator;
        [SerializeField] private ParticleSystem particleSystem;

        private Vector3 _initPosition;

        private Transform _transformCache;

        private const string AnimationKeyStr = "animation";
        private static readonly int AnimationKey = Animator.StringToHash(AnimationKeyStr);

        private const int IdleAnimation = 1; 
        private const int EatAnimation = 2; 
        private const int SleepAnimation = 3; 
        
        private void Awake()
        {
            _transformCache = transform;
            _initPosition = _transformCache.localPosition;
        }
        
        public void Show()
        {
            _transformCache.localPosition = _initPosition;
        }

        public void Hide()
        {
            var newPosition = _initPosition;
            newPosition.y -= 50.0f;
            _transformCache.localPosition = newPosition;
        }
        
        public void PlayIdle()
        {
            dragonAnimator.SetInteger(AnimationKey, IdleAnimation);
        }
        
        public void PlayEat()
        {
            dragonAnimator.SetInteger(AnimationKey, EatAnimation);
        }
        
        public void PlaySleep()
        {
            dragonAnimator.SetInteger(AnimationKey, SleepAnimation);
        }

        public void ShowParticle()
        {
            particleSystem.Play();
        }
        public void HideParticle()
        {
            particleSystem.Stop();
        }
    }
}
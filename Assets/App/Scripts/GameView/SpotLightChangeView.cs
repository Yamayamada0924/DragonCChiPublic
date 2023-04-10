using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using yydlib;

namespace App.Scripts.GameView
{
    public class SpotLightChangeView : MonoBehaviour
    {
        [SerializeField] private Light spotLight;

        private float _maxIntensity;
        
        private CancellationTokenSource _cancellationTokenSource;
        private void Awake()
        {
            _maxIntensity = spotLight.intensity;
        }

        public async void ChangeLight(float rate, float duration)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            
            DoChangeLightUniTask(_cancellationTokenSource.Token, rate, duration).Forget();
        }
        
        async UniTask DoChangeLightUniTask(CancellationToken token, float rate, float duration)
        {
            float time = duration;
            time -= Time.deltaTime;
            float targetIntensity = _maxIntensity * rate;
            float startIntensity = spotLight.intensity;
            while(time > 0.0f)
            {
                spotLight.intensity = GameUtil.GetLinear(startIntensity, targetIntensity, duration, 0.0f, time);
                await UniTask.DelayFrame(1);
                if(token.IsCancellationRequested)
                {
                    return;
                }
                time -= Time.deltaTime;
            }

            spotLight.intensity = targetIntensity;
        }
    }
}
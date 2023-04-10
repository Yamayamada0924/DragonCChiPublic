using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using yydlib;

namespace App.Scripts.UI
{
    public class DayNightView : MonoBehaviour
    {
        [SerializeField] private Image dayImage;
        [SerializeField] private Image nightImage;

        public void SetDay()
        {
            dayImage.enabled = true;
            nightImage.enabled = false;
        }
        
        public void SetNight()
        {
            dayImage.enabled = false;
            nightImage.enabled = true;
        }
    }
}
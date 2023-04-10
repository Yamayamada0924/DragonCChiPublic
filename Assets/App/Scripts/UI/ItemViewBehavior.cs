using System;
using System.Collections.Generic;
using App.Scripts.FixedData;
using App.Scripts.GameCommon;
using Fungus;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using yydlib;

namespace App.Scripts.UI
{

    public class ItemViewBehavior : MonoBehaviour
    {
        [SerializeField] private Image itemImage;

        public void SetImage(Sprite sprite)
        {
            itemImage.sprite = sprite;
        }
    }
}

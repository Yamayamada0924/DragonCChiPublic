using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.UI
{
    [RequireComponent(typeof(Slider))]
    public class SliderPointerUp : MonoBehaviour, IPointerUpHandler
    {
        private Slider _slider;
        public SliderPointerUpEvent OnValueChanged { get; set; } = new SliderPointerUpEvent();

        public class SliderPointerUpEvent : UnityEvent<float>
        {
        }

        // Start is called before the first frame update
        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnValueChanged.Invoke(_slider.value);
        }
    }
}

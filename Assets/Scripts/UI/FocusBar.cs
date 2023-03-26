using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LM
{
    public class FocusBar : MonoBehaviour
    {
        Slider slider;

        private void Awake() {
            slider = GetComponent<Slider>();
        }

        public void SetMaxFocusAndCurrent(float maxFocus) {
            slider.maxValue = maxFocus;
            slider.value = maxFocus;
        }

        public void SetCurrentFocus(float currentFocus) {
            slider.value = currentFocus;
        }
    }
    
}
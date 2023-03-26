using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LM {
    public class StaminaBar : MonoBehaviour // TODO: unify with HealthBar
    {
        Slider slider;

        private void Awake() {
            slider = GetComponent<Slider>();
        }

        public void SetMaxStaminaAndCurrent(float maxStamina) {
            slider.maxValue = maxStamina;
            slider.value = maxStamina;
        }

        public void SetCurrentStamina(float currentStamina) {
            slider.value = currentStamina;
        }

    }
}

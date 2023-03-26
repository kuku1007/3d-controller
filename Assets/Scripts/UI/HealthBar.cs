using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LM {
    public class HealthBar : MonoBehaviour
    {
        Slider slider;

        private void Awake() {
            slider = GetComponent<Slider>();
        }

        public void SetMaxHealthAndCurrent(int maxHealth) {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void SetCurrentHealth(int currentHealth) {
            slider.value = currentHealth;
        }

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace move{
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxhealth(int maxhealth){
        slider.maxValue = maxhealth;
        slider.value = maxhealth;
    }
    public void setCurrentHealth( int currenthealth){
        slider.value = currenthealth;
    }
}
}
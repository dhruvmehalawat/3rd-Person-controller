using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace move{
public class StanimaBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    public void SetMaxStamina(float maxStamina){
        slider.maxValue = maxStamina;
        slider.value = maxStamina;
    }
    public void setCurrentStanima( float maxStamina){
        slider.value = maxStamina;
    }
}
}
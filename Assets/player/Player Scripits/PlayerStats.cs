using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class PlayerStats : MonoBehaviour
{
    public int healthlevel = 10;
    public float staminaPoints = 10;
    public int maxhealth;
    public int currenthealth;
    public float maxStamina;
    public float currentStamina;

    public HealthBar healthBar;
    public StanimaBar stanimaBar;
    public animterHandler Anim;
    private void Awake() {
        Anim = GetComponentInChildren<animterHandler>();
    }

    private void Start() {
        maxhealth = SetMaxhealthfromhealthpoint();
        maxStamina = setMaxStaminaformpoints();
        currenthealth = maxhealth;
        currentStamina = maxStamina;
        healthBar.SetMaxhealth(maxhealth);
        stanimaBar.SetMaxStamina(maxStamina);
    }
    public void drainStamina(float staminaDrain){
        currentStamina = currentStamina - staminaDrain;
        stanimaBar.setCurrentStanima(currentStamina);
        if(currentStamina == 0){
            currentStamina = 0;
        }
    }
    public void DamageHealth(int damage){
        currenthealth = currenthealth - damage;
        Anim.playTargetAnmation("GettingHit",true);
        healthBar.setCurrentHealth(currenthealth);
        if(currenthealth == 0){
            currenthealth=0;
            Anim.playTargetAnmation("death",true);
        }
    }
    private int SetMaxhealthfromhealthpoint(){
        maxhealth = healthlevel *10;
        return maxhealth;
    }
    private float setMaxStaminaformpoints(){
        maxStamina = staminaPoints*20;
        return maxStamina;
    }
}
}
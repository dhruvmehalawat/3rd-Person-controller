using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
[CreateAssetMenu(menuName = "Items/Wepon Item")]
public class WeponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnArmed;
    [Header("One Handed attack animation")] 
    public string OH_lightAttack_1;
    public string OH_heavyAttack_1;
    public string  OH_lightAttack_2;
    public string OH_heavyAttack_2;
    [Header("idle arm ainmation")]
    public string right_arm_idle;
    public string left_arm_idle;
    [Header("Stamina costs")]
    public int StaminaCost;
    public float lightAttackCost = 1;
    public float HeavyAttackCost = 2;
    
  
}
}
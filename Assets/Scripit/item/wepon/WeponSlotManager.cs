using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class WeponSlotManager : MonoBehaviour
{
   WeponHolderSlot isLefthanded;
   WeponHolderSlot isRighthanded;
   public WeponItem attacingWeapon;

   DamageCollider lefthandDamagecollider;
   DamageCollider righthandDamagecollider;
   Animator animator;
   QuickSlotsUI quickSlotsUI;
   PlayerStats playerStats;
   

   private void Awake() {
    animator = GetComponent<Animator>();
    playerStats = GetComponentInParent<PlayerStats>();
    quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
    WeponHolderSlot[] weponslot = GetComponentsInChildren<WeponHolderSlot>();
    foreach(WeponHolderSlot weponHolder in weponslot){
        if(weponHolder.isLedtHanded){
            isLefthanded = weponHolder;
        }
        else if(weponHolder.isRightHanded){
            isRighthanded=weponHolder;
        }
    }
   }
   public void LoadWeponSlot(WeponItem weponItem,bool isleft){
    if(isleft){
        isLefthanded.LoadWeponModel(weponItem);
        loadleftHandWeponDmgCollider();
        quickSlotsUI.updateWeponQickslot(true,weponItem);
        if(weponItem != null){
            animator.CrossFade(weponItem.left_arm_idle,0.2f);
        }
        else{
            animator.CrossFade("left_arm_empty",0.2f);
        }
    }
    else{
        isRighthanded.LoadWeponModel(weponItem);
       loadrightHandWeponDmgCollider();
       quickSlotsUI.updateWeponQickslot(false,weponItem);
        if(weponItem != null){
            animator.CrossFade(weponItem.right_arm_idle,0.2f);
        }
        else{
            animator.CrossFade("right_arm_empty",0.2f);
        }
    }
   }
   private void loadleftHandWeponDmgCollider(){
    lefthandDamagecollider = isLefthanded.currentGameModel.GetComponentInChildren<DamageCollider>();
   }
   private void loadrightHandWeponDmgCollider(){
     righthandDamagecollider = isRighthanded.currentGameModel.GetComponentInChildren<DamageCollider>();
   }
   public void openlefthandWeaponDmgCollider(){
    lefthandDamagecollider.enableCollider();

   }
    public void closelefthandWeaponDmgCollider(){
        lefthandDamagecollider.DisableCollider();
    
   }
    public void openrighthandWeaponDmgCollider(){
    righthandDamagecollider.enableCollider();
   }
    public void closerighthandWeaponDmgCollider(){
    righthandDamagecollider.DisableCollider();
   }
    public void drainStaminalightattack(){
        playerStats.drainStamina(attacingWeapon.StaminaCost*attacingWeapon.lightAttackCost);
    }
    public void drainStaminaheavyattack(){
        playerStats.drainStamina(attacingWeapon.StaminaCost*attacingWeapon.HeavyAttackCost);
    }
}
}
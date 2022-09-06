using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class PlayerAttacker : MonoBehaviour
{
    animterHandler AnimterHandler;
    MovementHandler movementHandler;
    QuickSlotsUI quickSlotsUI;
    public string lastAttack;
    WeponSlotManager weponSlotManager;
    private void Awake() {
        quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
        movementHandler = GetComponent<MovementHandler>();
        weponSlotManager = GetComponentInChildren<WeponSlotManager>();
        AnimterHandler = GetComponentInChildren<animterHandler>();
    }
    
    public void handlelightAttack(WeponItem weponItem){
        if(quickSlotsUI.righthandweponative == false){return;}
        weponSlotManager.attacingWeapon = weponItem;
        AnimterHandler.playTargetAnmation(weponItem.OH_lightAttack_1,true);
        lastAttack = weponItem.OH_lightAttack_1;
    }
    public void handleHeavyAttack(WeponItem weponItem){
        if(quickSlotsUI.righthandweponative == false){return;}
        weponSlotManager.attacingWeapon = weponItem;
        AnimterHandler.playTargetAnmation(weponItem.OH_heavyAttack_1,true);
        lastAttack = weponItem.OH_heavyAttack_1;
    }
    public void handleWeponCombo(WeponItem weponItem){
        if(quickSlotsUI.righthandweponative == false){return;}
        if(movementHandler.comboFlag){
            AnimterHandler.animator.SetBool("candoCombo",false);
        if(lastAttack == weponItem.OH_lightAttack_1){
            AnimterHandler.playTargetAnmation(weponItem.OH_lightAttack_2,true);
        }
        else if(lastAttack == weponItem.OH_heavyAttack_1){
            AnimterHandler.playTargetAnmation(weponItem.OH_heavyAttack_2,true);
        }
        }
    }

}
}
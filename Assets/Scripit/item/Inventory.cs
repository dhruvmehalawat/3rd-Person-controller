using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class Inventory : MonoBehaviour
{
    WeponSlotManager weponSlotManager;

    public WeponItem leftWepon;
    public WeponItem rightWepon;
    public WeponItem unarmedweapon;
    public WeponItem[] righthandWepons = new WeponItem[1];
    public WeponItem[] lefthandWepons = new WeponItem[1];
    public int currentlefthandweapon = -1;
    public int currentrighthandweaponIndex = -1;
    public List<WeponItem> weaponInvertory;

    private void Awake() {
        weponSlotManager = GetComponentInChildren<WeponSlotManager>();
    }
    void Start()
    {
        leftWepon = unarmedweapon;
        rightWepon = unarmedweapon;
    }
    public void chandeWeponrighthand(){
        currentrighthandweaponIndex = currentrighthandweaponIndex+1;
        if(currentrighthandweaponIndex == 0 && righthandWepons[0] != null){
            rightWepon = righthandWepons[currentrighthandweaponIndex];
            weponSlotManager.LoadWeponSlot(righthandWepons[currentrighthandweaponIndex],false);
        }
        else if(currentrighthandweaponIndex == 0 && righthandWepons[0] == null){
            currentrighthandweaponIndex = currentrighthandweaponIndex+1;
        }
        else if(currentrighthandweaponIndex == 1 && righthandWepons[1] != null){
            rightWepon = righthandWepons[currentrighthandweaponIndex];
            weponSlotManager.LoadWeponSlot(righthandWepons[currentrighthandweaponIndex],false);
        }
        else if(currentrighthandweaponIndex == 1 && righthandWepons[1] == null){
            currentrighthandweaponIndex = currentrighthandweaponIndex+1;
        }
        if(currentrighthandweaponIndex > righthandWepons.Length-1){
            currentrighthandweaponIndex = -1;
            rightWepon = unarmedweapon;
            weponSlotManager.LoadWeponSlot(unarmedweapon,false);
        }
    }
    public void chandeWeponleftthand(){
        currentlefthandweapon = currentlefthandweapon+1;
        if(currentlefthandweapon == 0 && lefthandWepons[0] != null){
            leftWepon = lefthandWepons[currentlefthandweapon];
            weponSlotManager.LoadWeponSlot(lefthandWepons[currentlefthandweapon],true);
        }
        else if(currentlefthandweapon == 0 && lefthandWepons[0] == null){
            currentlefthandweapon = currentlefthandweapon+1;
        }
        else if(currentlefthandweapon == 1 && lefthandWepons[1] != null){
            leftWepon = lefthandWepons[currentlefthandweapon];
            weponSlotManager.LoadWeponSlot(lefthandWepons[currentlefthandweapon],true);
        }
        else if(currentlefthandweapon == 1 && lefthandWepons[1] == null){
            currentlefthandweapon = currentlefthandweapon+1;
        }
        if(currentlefthandweapon > lefthandWepons.Length-1){
            currentlefthandweapon = -1;
            leftWepon = unarmedweapon;
            weponSlotManager.LoadWeponSlot(unarmedweapon,true);
        }
    }

}
}
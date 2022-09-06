using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace move{
public class QuickSlotsUI : MonoBehaviour
{
    public Image righthandIcon;
    public Image lefthandicon;
    public bool righthandweponative;

    public void updateWeponQickslot(bool isleft, WeponItem weapon){
        if(isleft == false){
            if(weapon.ItemIcon != null){
                righthandIcon.sprite =  weapon.ItemIcon;
                righthandIcon.enabled = true;
                righthandweponative = true;
            }
            else{
                righthandIcon.sprite = null;
                righthandIcon.enabled =false;
                righthandweponative = false;
            }
           
        }
        else{
            if(weapon.ItemIcon != null){
                lefthandicon.sprite = weapon.ItemIcon;
                lefthandicon.enabled = true;
            }
            else{
                lefthandicon.sprite = null;
                lefthandicon.enabled = false;
            }
            
        }
    }
}
}
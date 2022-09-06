using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class WeponPicker : Interectable
{
    public WeponItem weponItem;
    public override void Interact(PlayerManger playerManger){
        base.Interact(playerManger);
        pickupitem(playerManger);
    }
    private void pickupitem(PlayerManger playerManger){
        Inventory inventory;
        locamovement locamovement;
        animterHandler anim;

        inventory = playerManger.GetComponent<Inventory>();
        locamovement = playerManger.GetComponent<locamovement>();
        anim = playerManger.GetComponentInChildren<animterHandler>();

        locamovement.rigidbody.velocity = Vector3.zero;
        anim.playTargetAnmation("pick_up",true);
        inventory.weaponInvertory.Add(weponItem);
        Destroy(gameObject);
        
    }
}
}
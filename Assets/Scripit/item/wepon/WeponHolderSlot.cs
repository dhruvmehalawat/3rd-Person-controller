using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class WeponHolderSlot : MonoBehaviour
{
   public Transform parrentOverride;
   public bool isLedtHanded;
   public bool isRightHanded;
   public GameObject currentGameModel;

   public void unloadWepon(){
    if(currentGameModel != null){
        currentGameModel.SetActive(false);
    }
   }
   public void UnloadAndDestroy(){
    if(currentGameModel !=null){
        Destroy(currentGameModel);
    }
   }
   public void LoadWeponModel(WeponItem weponItem){
    UnloadAndDestroy();
    if(weponItem == null){
        unloadWepon();
    }
    GameObject model = Instantiate(weponItem.modelPrefab) as GameObject;
    if(model != null){
        if(parrentOverride !=null){
            model.transform.parent = parrentOverride;
        }
        else{
            model.transform.parent = transform;
        }
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
        model.transform.localScale = Vector3.one;
    }
    currentGameModel = model;
   }


}
}
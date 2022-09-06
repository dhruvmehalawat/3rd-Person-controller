using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class animterHandler : MonoBehaviour
{
    public Animator animator;
    MovementHandler movementHandler;
    locamovement Locamovement;
    PlayerManger playerManger;

    int horizontal;
    int vertical;
    public bool canRotate = false;
    public void Inisalize(){
        playerManger = GetComponentInParent<PlayerManger>();
        animator = GetComponent<Animator>();
        movementHandler = GetComponentInParent<MovementHandler>();
        Locamovement = GetComponentInParent<locamovement>();
        vertical = Animator.StringToHash("vertical");
        horizontal = Animator.StringToHash("horizontal");

    }
     public void playTargetAnmation(string targetAnim,bool isIntreacting){
        animator.applyRootMotion = isIntreacting;
        animator.SetBool("isintreacting" ,isIntreacting );
        animator.CrossFade(targetAnim,0.2f);
    }
    public void updateAnimaterVlaue(float verticalMove,float horizontalmove,bool isSprinting){
        #region vertical
        float v= 0;
        if(verticalMove > 0 && verticalMove < 0.55f){
            v = 0.55f;
        }
        else if(verticalMove > 0.55f){
            v=1;
        }
        else if(verticalMove < 0 && verticalMove > -0.55f){
            v = -0.55f;
        }
        else if(verticalMove < -0.55f ){
            v = -1f;
        }
        else{
            v=0;
        }

        #endregion
        #region horizontal
        float h= 0;
        if(horizontalmove > 0 && horizontalmove < 0.55f){
            h = 0.55f;
        }
        else if(horizontalmove > 0.55f){
            h=1;
        }
        else if(horizontalmove < 0 && horizontalmove > -0.55f){
            h = -0.55f;
        }
        else if(horizontalmove < -0.55f ){
            h = -1f;
        }
        else{
            h=0;
        }

        #endregion
        if(isSprinting){
            v=2;
            h=horizontalmove;
        }
        animator.SetFloat(vertical,v,0.1f,Time.deltaTime);
        animator.SetFloat(horizontal,h,0.1f,Time.deltaTime);
    }
   
    public void CanRotate(){
        canRotate=true;
    }
    public void stoproatation(){
        canRotate=false;
    }
   private void OnAnimatorMove() {
        if(playerManger.isIntreacting == false){
            return;
        }
        float delta = Time.deltaTime;
        Locamovement.rigidbody.drag = 0;
        Vector3 deltapostion = animator.deltaPosition;
        deltapostion.y = 0;
        Vector3 velocity = deltapostion/delta;
        Locamovement.rigidbody.velocity = velocity;
    }
    public void EnableCombo(){
        animator.SetBool("candoCombo",true);
    }
    public void disableCombo(){
        animator.SetBool("candoCombo",false);
    }
}
}
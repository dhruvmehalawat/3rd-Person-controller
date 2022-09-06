using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class PlayerManger : MonoBehaviour
{
    MovementHandler movementHandler;
    Animator anim;
    locamovement Locamovement;
    cameraHandler camerahandler;
    [Header("Player flags")]
    public bool isIntreacting;
    public bool canDoCombo;
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;

    void Start()
    {
        camerahandler = cameraHandler.singleton;
        movementHandler = GetComponent<MovementHandler>();
        Locamovement = GetComponent<locamovement>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float delta = Time.deltaTime;
        isIntreacting = anim.GetBool("isintreacting");
        canDoCombo = anim.GetBool("candoCombo");
        anim.SetBool("IsInAir",isInAir);
        movementHandler.Tick(delta);
        Locamovement.handlejump();
        Locamovement.handlerollandsprint(delta);
        checkforIntractablrobject();
    }
    private void FixedUpdate() {
       float delta = Time.fixedDeltaTime;
        Locamovement.handleMovement(delta);
        Locamovement.HandleFalling(delta,Locamovement.movedirection);
   }
    private void LateUpdate() {
        movementHandler.rollflag = false;
        movementHandler.RBInput = false;
        movementHandler.RTInput = false;
        movementHandler.b_down = false;
        movementHandler.b_up = false;
        movementHandler.b_left = false;
        movementHandler.b_right = false;
        movementHandler.F_input = false;
        movementHandler.b_Jump = false;
        float delta = Time.deltaTime;
        if(camerahandler != null){
            camerahandler.followtarget(delta);
            camerahandler.handleCameraRotation(delta,movementHandler.mouseX,movementHandler.mouseY);
        }
        if(isInAir)
        {
            Locamovement.inAirTime  = Locamovement.inAirTime + Time.deltaTime;
        }
    }
    public void checkforIntractablrobject(){
        RaycastHit hit;
        if(Physics.SphereCast(transform.position , 0.4f,transform.forward,out hit,1f,camerahandler.ignoreLayer)){
            if(hit.collider.tag == "Intractable"){
                Interectable interectableobj = hit.collider.GetComponent<Interectable>();
                if(interectableobj != null){
                    string interectableobjtext= interectableobj.InterectableText;
                    //set to ui
                    if(movementHandler.F_input){
                        interectableobj.Interact(this);
                    }
                }
            }
        }
    }
}
}
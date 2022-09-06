using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
    public class locamovement : MonoBehaviour
    {  
        Transform cameraoject;
        MovementHandler movementHandler;
        PlayerManger playerManger;

        public Vector3 movedirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector] 
        public animterHandler animterhandler;

        public new Rigidbody rigidbody;
        public GameObject normalcamera;
        [Header("Grond N AiR Detection Statd")]
        [SerializeField] float GroungDetecitonRayStartPoint = 0.5f;
        [SerializeField] float MinDistanceNeededToFall = 1f;
        [SerializeField] float groundDirectionRayDistance = 0.2f;
        LayerMask ignoreforGroundCheck;
        public float inAirTime;

        [Header("Player Stats")]
        [SerializeField] float MoveSpeed = 5f;
        [SerializeField] float rotationSpeed = 10f;
        [SerializeField] float sprintspeed = 7f;
        [SerializeField] float fallingSpeed = 45f;
        [SerializeField]  float walkingSpeed = 1; 

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            playerManger = GetComponent<PlayerManger>();
            movementHandler = GetComponent<MovementHandler>();
            animterhandler = GetComponentInChildren<animterHandler>();
            cameraoject = Camera.main.transform;
            myTransform = transform;

            playerManger.isGrounded = true;
            ignoreforGroundCheck = ~(1 << 8 | 1 << 11);
        }
        #region movement
        Vector3 normalVector;
        Vector3 tagetpostion;

        void handleRotation(float delta){
            Vector3 tagetDirection = Vector3.zero;
            float moveoverride = movementHandler.moveAmt;
            tagetDirection = cameraoject.forward * movementHandler.vertical;
            tagetDirection += cameraoject.right * movementHandler.horizontal;
            tagetDirection.Normalize();
            tagetDirection.y = 0;
            if(tagetDirection == Vector3.zero){
                tagetDirection = myTransform.forward;
            }
            float rs = rotationSpeed;
            Quaternion tr = Quaternion.LookRotation(tagetDirection);
            Quaternion targetRoation = Quaternion.Slerp(myTransform.rotation,tr,rs*delta);
            myTransform.rotation = targetRoation;


        }
        public void handleMovement(float delta){
            if(movementHandler.rollflag){return;}
            if(playerManger.isIntreacting){return;}
            movedirection = cameraoject.forward * movementHandler.vertical;
            movedirection += cameraoject.right * movementHandler.horizontal;
            movedirection.Normalize();
            movedirection.y=0;
            float speed = MoveSpeed;
            if(movementHandler.sprintflag && movementHandler.moveAmt>0.5){
                speed = sprintspeed;
                playerManger.isSprinting = true;
                movedirection *=speed;
            }
            else{
                if(movementHandler.moveAmt< 0.5f){
                    movedirection *= walkingSpeed;
                    playerManger.isSprinting = false;
                }
                else{
                    playerManger.isSprinting = false;
                    movedirection *= speed;
                }
            }
            Vector3 projectile = Vector3.ProjectOnPlane(movedirection,normalVector);
            rigidbody.velocity =projectile; 
            animterhandler.Inisalize();
            animterhandler.updateAnimaterVlaue(movementHandler.moveAmt,0,playerManger.isSprinting);
            if(animterhandler.canRotate){
            handleRotation(delta);}
            
        }
        
        public void handlerollandsprint(float delta){
            if(animterhandler.animator.GetBool("isintreacting")){
                return;
            }
            if(movementHandler.rollflag){
                movedirection = cameraoject.forward * movementHandler.vertical;
                movedirection += cameraoject.right * movementHandler.horizontal;
                if(movementHandler.moveAmt > 0){
                    animterhandler.playTargetAnmation("rolling",true);
                    movedirection.y = 0;
                    Quaternion rollratation = Quaternion.LookRotation(movedirection);
                    myTransform.rotation = rollratation; 
                }
                else
                {
                    animterhandler.playTargetAnmation("backstep",true);
                }
            } 
        } 
        public void HandleFalling(float delta, Vector3 moveDirection){
            playerManger.isGrounded= false;
            RaycastHit hit;
            Vector3 origin = myTransform.position;
            origin.y += GroungDetecitonRayStartPoint;

            if(Physics.Raycast(origin,myTransform.forward, out hit,0.4f)){
                moveDirection = Vector3.zero;
            }
            if(playerManger.isInAir){
                rigidbody.AddForce(-Vector3.up * fallingSpeed);
                rigidbody.AddForce(moveDirection * fallingSpeed / 7f);
            }
            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir*groundDirectionRayDistance;
            tagetpostion = myTransform.position;

            Debug.DrawRay(origin , -Vector3.up * MinDistanceNeededToFall,Color.red,0.1f,false);
            if(Physics.Raycast(origin,-Vector3.up,out hit,MinDistanceNeededToFall,ignoreforGroundCheck)){

                normalVector = hit.normal;
                Vector3 tp = hit.point;
                playerManger.isGrounded = true;
                tagetpostion.y = tp.y;

                if(playerManger.isInAir){
                    if(inAirTime > 0.5f){
                      Debug.Log("you were in the air for" + inAirTime)  ;
                      animterhandler.playTargetAnmation("land",true);

                    }
                    else{
                        animterhandler.playTargetAnmation("Empty",false);
                        inAirTime = 0;
                    }
                    playerManger.isInAir = false;
                }

            }
            else{
                if(playerManger.isGrounded){
                    playerManger.isGrounded = false;
                }
                if(playerManger.isInAir == false){
                    if(playerManger.isIntreacting == false){
                        animterhandler.playTargetAnmation("falling",true);
                    }
                    Vector3 val = rigidbody.velocity;
                    val.Normalize();
                    rigidbody.velocity = val *(MoveSpeed/2);
                    playerManger.isInAir = true;
                }
            }
                if(playerManger.isIntreacting || movementHandler.moveAmt > 0){
                    myTransform.position = Vector3.Lerp(myTransform.position,tagetpostion,Time.deltaTime/0.1f);
                }
                else{
                    myTransform.position  = tagetpostion;
                }
        }
        #endregion
        public void handlejump(){
            if(animterhandler.animator.GetBool("isintreacting")){
                return;
            }
            if(movementHandler.b_Jump){
                if(movementHandler.moveAmt > 0){
                    movedirection = cameraoject.forward * movementHandler.vertical;
                    movedirection += cameraoject.right * movementHandler.horizontal;
                    animterhandler.playTargetAnmation("jump",true);
                    movedirection.y = 0;
                    Quaternion jumprotation = Quaternion.LookRotation(movedirection);
                    myTransform.rotation = jumprotation; 
                }

            }
        }
    }
}
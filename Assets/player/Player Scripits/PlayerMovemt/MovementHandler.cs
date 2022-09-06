using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace move{
public class MovementHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmt;
    public float mouseX;
    public float mouseY;
    public bool b_input;
    public bool b_up;
    public bool F_input;
    public bool b_down;
    public bool b_left;
    public bool b_right;
    public bool b_Jump;
    public bool RTInput;
    public bool RBInput;
    public bool rollflag;
    public bool sprintflag;
    public bool comboFlag;
    public bool jumpflag;

    public float rollinputtimer;
    

    PlayerController playerControler;
    PlayerAttacker playerAttacker;
    PlayerManger playerManger;
    Inventory inventory;
    

    Vector2 movmentInput;
    Vector2 cameraInput;
    private void Awake() {
        playerManger = GetComponent<PlayerManger>();
        playerAttacker = GetComponent<PlayerAttacker>();
        inventory = GetComponent<Inventory>();
    }
    private void OnEnable() {
        if(playerControler == null){
            playerControler = new PlayerController();
            playerControler.PlayerMovents.Movements.performed += playerControler => movmentInput = playerControler.ReadValue<Vector2>();
            playerControler.PlayerMovents.MouseInput.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControler.PlayerInputs.Jump.performed += i => b_Jump = true;
            playerControler.PlayerInputs.RB.performed += i => RBInput =true;
            playerControler.PlayerInputs.RT.performed += i => RTInput = true;
            playerControler.PlayerWslots.up.performed += i => b_up = true;
            playerControler.PlayerWslots.down.performed +=i => b_down = true;
            playerControler.PlayerInputs.F.performed += i => F_input = true;
        }
        playerControler.Enable();
    }
    private void OnDisable() {
        playerControler.Disable();
    }
    public void Tick(float delta){
        moveInput(delta);
        handleRollInput(delta);
        handleAttackInput(delta);
        handleWeaponQuickslots();
        handleJumpInput();
    }
    private void moveInput(float delta){
        horizontal = movmentInput.x;
        vertical= movmentInput.y;
        moveAmt = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }
    private void handleRollInput(float delta){
        b_input = playerControler.PlayerInputs.Rolls.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
        sprintflag = b_input;
        if(b_input){
            rollinputtimer += delta;
        }
        else{
            if(rollinputtimer > 0 && rollinputtimer < 0.5f){
                rollflag = true;
                sprintflag =false;
            } 
            rollinputtimer = 0;
        }
    }
    public void handleAttackInput(float delta){
        if(RBInput){
            if(playerManger.canDoCombo){
                comboFlag = true;
                playerAttacker.handleWeponCombo(inventory.rightWepon);
                comboFlag =false;
            }
            else{
                if(playerManger.isIntreacting){return;}
                if(playerManger.canDoCombo){return;}
                
                playerAttacker.handlelightAttack(inventory.rightWepon);
            }
            
        }
        else if(RTInput){
            if(playerManger.canDoCombo){
                comboFlag = true;
                playerAttacker.handleWeponCombo(inventory.rightWepon);
                comboFlag =false;
            }
            else{
                if(playerManger.isIntreacting){return;}
                if(playerManger.canDoCombo){return;}
                
                playerAttacker.handleHeavyAttack(inventory.rightWepon);
            }
        }
    }
    public void handleWeaponQuickslots(){
       
        if(b_up){
            inventory.chandeWeponrighthand();
        }
        else if(b_down){
            inventory.chandeWeponleftthand();
        }
    }
    public void handleJumpInput(){
        if(b_Jump){
            jumpflag =true;
        }
        else{
            jumpflag =false;
        }
    }
}
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
public class PlayerLocomotion : MonoBehaviour // TODO: probably promote to PlayerManager and move some logic to other class for movement
{
    [Header("Dependencies")]
    public Transform camFollowTransform;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public InputHandler inputHandler;
    [HideInInspector] public PlayerAnimationManager playerAnimationManager;
    
    [Header("Parameters")]
    public Vector3 verticalVelocity;
    public float gravity = -9.8f;
    
    public Vector3 direction;
    public float rotationSpeed = 10f;
    public float movementSpeed = 3;
    public bool isSprinting; // TODO

    [Header("States")]
    public State currentState;
    public MoveState moveState = new MoveState();
    public JumpState jumpState = new JumpState();
    public FallState fallState = new FallState();
    public RollState rollState = new RollState();
    public NormalActionState normalActionState = new NormalActionState();
    public AlternativeActionState alternativeActionState = new AlternativeActionState();

    public Vector3 currentInAirDirection;

    void Start()
    {
        // Application.targetFrameRate = 30;
        characterController = GetComponent<CharacterController>();
        inputHandler = GetComponent<InputHandler>();
        playerAnimationManager = GetComponentInChildren<PlayerAnimationManager>();
        playerAnimationManager.Initialize();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetState(moveState);
    }

    void Update()
    {   
        inputHandler.TickInput(Time.deltaTime);
        FetchTargetDirection();
        
        currentState.OnUpdate(this);

        #region OLD
        // inputHandler.TickInput(Time.deltaTime);
        // if(characterController.isGrounded && playerAnimationManager.anim.GetBool("isInAir") == true) {
        //     playerAnimationManager.anim.SetBool("isInAir", false);
        //     playerAnimationManager.PlayTargetAnimation("Empty", false);
        // }
        // FetchTargetDirection();
        // HandleRolling();
        // HandleJump();
        // if(!playerAnimationManager.anim.GetBool("animationOngoing")) {
        //     HandleMovement(); // TODO here: pass dependencies to methods that are using it
        // }
        // HandleRotation();
        // HandleAnimatorMotionMovement();
        #endregion
    }

    void LateUpdate() {
        ResetInputs();
    }

    private void ResetInputs()
    {
        inputHandler.lookInput = Vector2.zero;
        inputHandler.jumpImput = false;
        inputHandler.roll_Input = false;
        inputHandler.normalAttackInput = false;
        inputHandler.alternativeAttackInput = false;
    }

    private void FetchTargetDirection() {
        direction = transform.forward * inputHandler.movementInput.y + transform.right * inputHandler.movementInput.x;
        direction.Normalize();
    }
    
    public void SetState(State state) {
        currentState = state;
        currentState.EnterState(this);
    }

    public void HandleMovement(Vector3 direction) {
        characterController.Move(direction * movementSpeed * Time.deltaTime);
    }
    
    public void HandleGravity() {
        // TODO: edit: there are some issues with isGrounded when Move triggered twice in frame
        Debug.Log("isg " + characterController.isGrounded);
        if(characterController.isGrounded && verticalVelocity.y < 0) {
            verticalVelocity.y = 0;
        }
        verticalVelocity.y += gravity * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);
    }

    public void HandleRotation() {
        #region Follow Transform Rotation

        //Rotate the Follow Target transform based on the input
        camFollowTransform.rotation *= Quaternion.AngleAxis(inputHandler.lookInput.x * rotationSpeed, Vector3.up);

        #endregion

        #region Vertical Rotation
        camFollowTransform.rotation *= Quaternion.AngleAxis(-inputHandler.lookInput.y * rotationSpeed, Vector3.right);

        var angles = camFollowTransform.localEulerAngles;
        angles.z = 0;

        var angle = camFollowTransform.localEulerAngles.x;

        //Clamp the Up/Down rotation
        if (angle > 180 && angle < 340)
        {
            angles.x = 340; // or -20 in inspector
        }
        else if(angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        camFollowTransform.localEulerAngles = angles;
        #endregion

        if (inputHandler.movementInput.x == 0 && inputHandler.movementInput.y == 0) 
        {   
            return; // Do not rotate player transform when in idle/notMoving TODO here: when not moving and for example rolling the camera jitters
        }   
        

        //Set the player rotation based on the look transform
        transform.rotation = Quaternion.Euler(0, camFollowTransform.rotation.eulerAngles.y, 0);
        //reset the y rotation of the look transform
        camFollowTransform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }
}
}


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
    [HideInInspector] public PlayerInventory playerInventory;
    [HideInInspector] public WeaponBodySlotManager weaponBodySlotManager;
    
    [Header("Parameters")]
    public LayerMask groundLayer;
    public float gravity = -9.8f;
    public float groundCheckVerticalOffset = 0.95f;
    public float groundCheckRadiusOffset = 0.04f;
    public float slipCheckVerticalOffset = 1.1f;
    public float slipCheckRadiusOffset = 0.2f;
    public float wallCheckVerticalOffset = 0.71f;
    public float wallCheckRadiusOffset = 0f;
    public float rotationSpeed = 0.1f;
    public float movementSpeed = 3;
    public bool isSprinting; // TODO
    public float slipSpeed = 3;
    [SerializeField] Vector2 m_wallCheck = new Vector2(0.31f, 0.31f);
	[SerializeField] Vector3 m_groundCheck = new Vector3(0.1f, 0.2f, 0.4f);

    [Header("States")]
    public State currentState;
    public MoveState moveState = new MoveState();
    public JumpState jumpState = new JumpState();
    public FallState fallState = new FallState();
    public RollState rollState = new RollState();
    public NormalActionState normalActionState = new NormalActionState();
    public AlternativeActionState alternativeActionState = new AlternativeActionState();

    public Vector3 currentInAirDirection;
    public bool isOnGround;
    public Vector3 inputDirection;
    public Vector3 verticalVelocity;
    public Vector3 charVel;

    void Start()
    {
        // Application.targetFrameRate = 30;
        characterController = GetComponent<CharacterController>();
        inputHandler = GetComponent<InputHandler>();
        playerInventory = GetComponent<PlayerInventory>();
        weaponBodySlotManager = GetComponentInChildren<WeaponBodySlotManager>();
        playerAnimationManager = GetComponentInChildren<PlayerAnimationManager>();
        playerAnimationManager.Initialize(); // TODO: wwhy

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetState(moveState);
    }

    void Update()
    {   
        isOnGround = isGrounded();
        inputHandler.TickInput(Time.deltaTime);
        FetchTargetDirection();
        charVel = characterController.velocity;
        currentState.OnUpdate(this);
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
        inputDirection = transform.forward * inputHandler.movementInput.y + transform.right * inputHandler.movementInput.x;
        inputDirection.Normalize();
    }
    
    public void SetState(State state) {
        currentState = state;
        currentState.EnterState(this);
    }

    public void HandleMovement(Vector3 direction) {
        characterController.Move(direction * movementSpeed * Time.deltaTime);
    }
    
    public void HandleGravity() {
        if(isOnGround && verticalVelocity.y < 0) {
            verticalVelocity.y = -2;
        }
        verticalVelocity.y += gravity * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);
    }

    private bool isGrounded() {
        Vector3 spherePos = transform.position;
        spherePos.y -= groundCheckVerticalOffset;
        return Physics.CheckSphere(spherePos, characterController.radius - groundCheckRadiusOffset, groundLayer);
    }

    public void HandleSlippery() {
        Vector3 spherePos = transform.position;
        spherePos.y -= wallCheckVerticalOffset;
  
        Collider[] colliders = Physics.OverlapSphere(spherePos, characterController.radius + wallCheckRadiusOffset, groundLayer);
        if(colliders.Length > 0)
        {
            Vector3 slipDirection = Vector3.zero;

            foreach (Collider collider in colliders)
            {
                Vector3 colliderPosition = collider.transform.position;
                Vector3 intersectionDirection = (spherePos - colliderPosition).normalized;
                intersectionDirection.y *= -1; // slip down
                slipDirection += intersectionDirection;
            }
			SlipMove(slipDirection);
		}
    }

    public bool checkIfActivateSlippery() {
        Vector3 spherePos = transform.position;
        spherePos.y -= slipCheckVerticalOffset;
        return !Physics.CheckSphere(spherePos, characterController.radius - slipCheckRadiusOffset, groundLayer);
    }

	void SlipMove(Vector3 slip_direction){
		characterController.Move(((slip_direction * slipSpeed) + Vector3.down) * Time.deltaTime);
	}

    void OnDrawGizmos(){
        if(characterController == null)
            return;
        Gizmos.color = Color.red; // is Grounded check
        Vector3 spherePos = transform.position;
        spherePos.y -= groundCheckVerticalOffset;
        Gizmos.DrawWireSphere(spherePos, characterController.radius - groundCheckRadiusOffset);
        
        spherePos = transform.position; // wall slippery sphere
        spherePos.y -= wallCheckVerticalOffset;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(spherePos, characterController.radius + wallCheckRadiusOffset);

        Gizmos.color = Color.yellow; // start slippery sphere check
        spherePos = transform.position;
        spherePos.y -= slipCheckVerticalOffset;
        Gizmos.DrawWireSphere(spherePos, characterController.radius - slipCheckRadiusOffset);
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


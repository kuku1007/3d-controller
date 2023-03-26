using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM{
    public class InputHandler : MonoBehaviour {
        public Vector2 movementInput;
        public Vector2 lookInput;

        public bool roll_Input;
        public bool rb_Input;
        public bool rbHold_Input;
        public bool rt_Input;
        public bool jumpImput;
        public bool interact_Input;
        public bool inventory_Input;
        public bool inventoryFlag;
        public bool twoH_Input;
        public bool twoHFlag;

        // Slots
        public bool u_Arrow;
        public bool d_Arrow;
        public bool l_Arrow;
        public bool r_Arrow;

        public bool sprintFlag;
        PlayerControls playerControlsInputActions;
        PlayerAnimationManager playerAnimationManager;
        WeaponBodySlotManager weaponHandSlotManager;

        private void Awake() {
            playerAnimationManager = GetComponentInChildren<PlayerAnimationManager>();
            weaponHandSlotManager = GetComponentInChildren<WeaponBodySlotManager>();
        }
        public void OnEnable() {
            if(playerControlsInputActions == null) {
                playerControlsInputActions = new PlayerControls();
                playerControlsInputActions.PlayerMovementMap.Look.performed += inputActions => lookInput = inputActions.ReadValue<Vector2>();
                playerControlsInputActions.PlayerMovementMap.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                playerControlsInputActions.PlayerActionsMap.Jump.performed += inputActions => jumpImput = true;
                playerControlsInputActions.PlayerActionsMap.RB.performed += i => rb_Input = true;
                playerControlsInputActions.PlayerActionsMap.RT.performed += i => rt_Input = true;
                playerControlsInputActions.PlayerQuickSlotsMap.LeftArrow.performed += i => l_Arrow = true;
                playerControlsInputActions.PlayerQuickSlotsMap.RightArrow.performed += i => r_Arrow = true; // TODO: instead changing boolean I can subscribe here NextRightWeapon();
                playerControlsInputActions.PlayerActionsMap.Interact.performed += i => interact_Input = true;
                playerControlsInputActions.PlayerActionsMap.Inventory.performed += i => inventory_Input = true;
                playerControlsInputActions.PlayerActionsMap._2H.performed += i => twoH_Input = true;
                playerControlsInputActions.PlayerActionsMap.RBHold.performed += i => rbHold_Input = true;
                playerControlsInputActions.PlayerActionsMap.Roll.performed += i => roll_Input = true;
            }

            playerControlsInputActions.Enable();
        }

        private void OnDisable() {
            playerControlsInputActions.Disable();
        }

        public void TickInput(float delta) {
            HandleAttackInput(delta);
            HanleQuickSlotInput();
            HandleInventoryInput();
            Handle2HInput();
        }


        private void HandleAttackInput(float delta) {
            // if(rb_Input) {
            //     playerAttacker.HandleNormalAttack();
            // }
            // if(rt_Input) {
            //     playerAnimationManager.anim.SetBool("isUsingRightHand", true);
            //     playerAttacker.HandleMeleeHeavyAttack(playerInventory.rightWeapon);
            // }
        }

        private void HanleQuickSlotInput() {
            // if(l_Arrow) {
            //     playerInventory.NextLeftWeapon();
            // }
            // if(r_Arrow) {
            //     playerInventory.NextRightWeapon();
            // }
        }

        private void HandleInventoryInput() {
            // if(inventory_Input) {
            //     inventoryFlag = !inventoryFlag;
                
            //     if(inventoryFlag) {
            //         Cursor.lockState = CursorLockMode.Confined;
            //         Cursor.visible = true;
            //         uIManager.OpenSelectWindow();
            //         uIManager.UpdateUI();
            //         uIManager.CloseHUDWindow();
            //     } else {
            //         Cursor.lockState = CursorLockMode.Locked;
            //         Cursor.visible = false;
            //         uIManager.CloseSelectWindow();
            //         uIManager.CloseAllInventoryWindow();
            //         uIManager.OpenHUDWindow();
            //     }
            // }
        }
    
        private void Handle2HInput() {
            // if(twoH_Input) {
            //     twoHFlag = !twoHFlag;
            //     if(twoHFlag) {
            //         weaponHandSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
            //     } else {
            //         weaponHandSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
            //         weaponHandSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);
            //     }
            // }
        }
    
        private void HandleCritAttackInput() {
            // if(rbHold_Input) {
            //     rbHold_Input = false; // TODO: now cancelling is here, not in LateUpdate of PlayerManager?
            //     playerAttacker.AttemptToBackstabOrRiposte();
            // }
        }
    }
}


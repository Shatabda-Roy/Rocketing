using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    [Header("Movement Input Buttons")]
    public bool isRunPressed;
    public bool isJumpPressed = false;
    public bool isCrouchPressed;
    public bool isJumping = false;
    public bool isCrouching = false;
    private InputActions _inputActions;
    [Space()]
    
    [Header("Movement")] public Vector2 currentMovementInput;

    public bool isMovementPressed;

    [Header("Interaction Input Buttons")]
    public bool isInteracting;
    [SerializeField] float mouseSensitivity;
    public Vector2 currentMouseInput;
    public Vector3 currentMouseMovement;
    public bool isMouseMoving;
    public bool isMouseMovingOld;

    [Header("DebugControls")] public bool isCursorBeingLocked = false;

    public void Awake() {
        _inputActions = new InputActions();

        //_inputActions.PlayerControls.Run.started += onRun;
        //_inputActions.PlayerControls.Run.canceled += onRun;

        _inputActions.PlayerControls.Jump.started += onJump;
        _inputActions.PlayerControls.Jump.canceled += onJump;
        
        _inputActions.DebugControls.CursorLock.started += onCursorLock;
        _inputActions.DebugControls.CursorLock.canceled += onCursorLock;
        
        _inputActions.PlayerControls.mouseLook.started += onMouseLook;
        _inputActions.PlayerControls.mouseLook.performed += onMouseLook;
        _inputActions.PlayerControls.mouseLook.canceled += onMouseLook;
        
        _inputActions.PlayerControls.Movement.started += onMovementInput;
        _inputActions.PlayerControls.Movement.performed += onMovementInput;
        _inputActions.PlayerControls.Movement.canceled += onMovementInput;

    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
}
    void onRun(InputAction.CallbackContext context){
        isRunPressed = context.ReadValueAsButton();
    }
    void onJump(InputAction.CallbackContext context){
        isJumpPressed = context.ReadValueAsButton();
    }

    void onCursorLock(InputAction.CallbackContext context)
    {
        isCursorBeingLocked = context.ReadValueAsButton();
    }
    void onMouseLook(InputAction.CallbackContext context)
    {
        currentMouseInput = context.ReadValue<Vector2>();
        //currentMouseInput.Normalize();
        isMouseMoving = currentMouseInput.x != 0 || currentMouseInput.y != 0;
    }
    void OnEnable()
    {
        _inputActions.PlayerControls.Enable();
        //_inputActions.CharacterActions.Enable();
        _inputActions.DebugControls.Enable();
    }
        void OnDisable()
    {
        _inputActions.PlayerControls.Disable();
        //_inputActions.CharacterActions.Disable();
        _inputActions.DebugControls.Disable();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform InputManager;
    private Rigidbody _rigidbody;
    [Space()]
    
    [Header("Settings")]
    [SerializeField] private float mainThrust = 100f;
    [SerializeField] private float rotationThrust = 10f;
    [Space()]
    
    private InputManager _inputManager;

    private bool isLeftHorizontalMovement;
    private bool isRightHorizontalMovement;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _inputManager = InputManager.GetComponent<InputManager>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        isLeftHorizontalMovement = _inputManager.currentMovementInput.x != 1 && _inputManager.currentMovementInput.x != 0;
        isRightHorizontalMovement = _inputManager.currentMovementInput.x != -1 && _inputManager.currentMovementInput.x != 0;
        
        if (isLeftHorizontalMovement)
        {
            ApplyRotation(rotationThrust);
        }
        if (isRightHorizontalMovement)
        {
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * (rotationThisFrame * Time.deltaTime));
        _rigidbody.freezeRotation = false;
    }

    void HandleJump()
    {
        if (_inputManager.isJumpPressed)
        {
            _rigidbody.AddRelativeForce(Vector3.up * (mainThrust * Time.deltaTime));
        }
    }
}

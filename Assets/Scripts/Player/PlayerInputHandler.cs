using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset _playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string _actionMapName = "Gameplay";

    [Header("Action Name References")]
    [SerializeField] private string _move = "Move";
    [SerializeField] private string _jump = "Jump";

    private InputAction _moveAction;
    private InputAction _jumpAction;

    public Vector2 MoveInput { get; private set; }
    public bool JumpTriggered { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

        _moveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_move);
        _jumpAction = _playerControls.FindActionMap(_actionMapName).FindAction(_jump);
        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        _moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        _moveAction.canceled += context => MoveInput = Vector2.zero;

        _jumpAction.performed += context => JumpTriggered = true;
        _jumpAction.canceled += context => JumpTriggered = false;
    }

    private void OnEnable()
    {
        _moveAction.Enable();
        _jumpAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _jumpAction.Disable();
    }
}

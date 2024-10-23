using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private Rigidbody _rb;
    private Vector3 _movementVector;
    private bool _isJumping = false;
    private Vector3 _jumpVector;
    private float _distToGround;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _distToGround = GetComponent<Collider>().bounds.extents.y;
        _jumpVector = new Vector3(0f, 10.0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        _movementVector = PlayerInputHandler.Instance.MoveInput;
        _isJumping = PlayerInputHandler.Instance.JumpTriggered;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, _movementVector.x * _speed);
        if (_isJumping && IsGrounded())
        {
            _rb.AddForce(_jumpVector * _jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, _distToGround);
    }

}

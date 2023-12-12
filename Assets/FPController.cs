using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPController : MonoBehaviour
{
    /*
    public float movementSpeed;
    public float rotateSpeed;
    Rigidbody rb;
    public GameObject gadgetSpawnLocation;
    public GameObject YRotate;
    public InputActionReference move;
    public InputActionReference look;
    public InputActionReference fire;
    public InputActionReference gadget;
    */

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cameraTransform;

    private PlayerInputActions controls;

    private Vector2 movementInput;
    private Vector2 lookInput;


    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();

        controls = new PlayerInputActions();
        controls.Player.Move.performed += context => movementInput = context.ReadValue<Vector2>();
        controls.Player.Look.performed += context => lookInput = context.ReadValue<Vector2>();
        //controls.Player.Jump.performed += _ => Jump();
        controls.Player.Gadget.performed += _ => Gadget();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Move.canceled += _ => movementInput = Vector2.zero;
        controls.Player.Look.canceled += _ => lookInput = Vector2.zero;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        controls.Player.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        //Move();
        //Look();

        Debug.Log("movemtn input = " + movementInput);

        MovePlayer();
        RotateCamera();
    }

    private void Gadget()
    {
        Debug.Log("Gadget");

        GetComponent<Gadget>().Play();
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);
        Debug.Log("Move Direction " + moveDirection);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed * Time.deltaTime;
        characterController.Move(moveDirection);
    }

    private void RotateCamera()
    {
        float xRotation = lookInput.x * lookSensitivity * Time.deltaTime;
        float yRotation = lookInput.y * lookSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up, xRotation);
        cameraTransform.Rotate(Vector3.right, -yRotation);
    }

    private void Jump()
    {
        // Implement jump logic here
        // For example, use characterController.Move(Vector3.up * jumpForce * Time.deltaTime);
    }

    /*
    private void Move()
    {
        Vector3 movement = new Vector3(move.action.ReadValue<Vector2>().x, 0, move.action.ReadValue<Vector2>().y) * movementSpeed;
        //rb.AddForce(movement, ForceMode.Impulse);
        rb.velocity = movement;
        Debug.Log("velocity = " + rb.velocity);
    }

    private void Look()
    {
        Vector2 looking = look.action.ReadValue<Vector2>() * rotateSpeed;

        //rotate objects
        transform.Rotate(0, looking.x, 0);
        YRotate.transform.Rotate(-looking.y, 0, 0);
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        Debug.Log("Shoot");
    }
    */
}

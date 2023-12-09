using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPController : MonoBehaviour
{
    public float movementSpeed;
    public float rotateSpeed;
    Rigidbody rb;
    public GameObject YRotate;
    public InputActionReference move;
    public InputActionReference look;
    public InputActionReference fire;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        fire.action.performed += Shoot;
    }


    private void Update()
    {
        Move();
        Look();
    }


    private void Move()
    {
        Vector3 movement = new Vector3(move.action.ReadValue<Vector2>().x, 0, move.action.ReadValue<Vector2>().y) * movementSpeed;
        //rb.AddForce(movement, ForceMode.Impulse);
        rb.velocity = movement;
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
}

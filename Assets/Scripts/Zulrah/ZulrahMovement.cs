using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZulrahMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;

    private CharacterController zulrahController;
    private Vector3 movementX;
    private Vector3 movementZ;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        zulrahController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxis("Horizontal") * Vector3.right * moveSpeed * Time.deltaTime;

        movementZ = Input.GetAxis("Vertical") * Vector3.forward * moveSpeed * Time.deltaTime;

        movement = transform.TransformDirection(movementX + movementZ);

        movement.y = 0;

        zulrahController.Move(movement);
    }
}

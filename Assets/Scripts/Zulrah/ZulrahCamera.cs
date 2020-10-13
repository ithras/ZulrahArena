using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZulrahCamera : MonoBehaviour
{

    public float MouseSensitivity = 100f;
    public float smoothing = 0f;
    public Transform zulrah;
    private Vector2 smoothedVelocity;
    private Vector2 currentLookingPos;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        inputValues = Vector2.Scale(inputValues, new Vector2(MouseSensitivity * smoothing, MouseSensitivity * smoothing));

        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

        currentLookingPos.x += smoothedVelocity.x;
        currentLookingPos.y += smoothedVelocity.y;

        transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y, Vector3.right);
        zulrah.localRotation = Quaternion.AngleAxis(currentLookingPos.x, Vector3.up);
    }
}


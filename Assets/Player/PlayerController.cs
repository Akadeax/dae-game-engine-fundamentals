using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float pitchLimitDeg = 90f;
    [SerializeField] float gravity = 2f;

    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Apply movement (I see no sense in using the new input system in a PC-only prototype)
        Vector3 camForward = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 camRight = Vector3.Cross(Vector3.up, camForward);

        Vector3 forwardBackward = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime * camForward;
        Vector3 leftRight = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime * camRight;

        characterController.Move(forwardBackward + leftRight);

        transform.Rotate(Input.GetAxisRaw("Mouse X") * rotationSpeed * Vector3.up);
        // Mouse rotation
        Vector3 oldRotationEuler = Camera.main.transform.rotation.eulerAngles;


        oldRotationEuler.x -= Input.GetAxisRaw("Mouse Y") * rotationSpeed;
        oldRotationEuler.x = RestrictAngle(oldRotationEuler.x, -pitchLimitDeg, pitchLimitDeg);

        Camera.main.transform.eulerAngles = oldRotationEuler;


        // Apply gravity
        if (!characterController.isGrounded)
        {
            characterController.Move(gravity * Time.deltaTime * Vector3.down);
        }
    }

    // The angle rolls over 0 -> 360 so we can't just Mathf.Clamp; used for camera pitch
    public static float RestrictAngle(float angle, float angleMin, float angleMax)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        else if (angle < -180)
        {
            angle += 360;
        }

        if (angle > angleMax)
        {
            angle = angleMax;
        }

        if (angle < angleMin)
        {
            angle = angleMin;
        }

        return angle;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public  UnityEvent<Augment> OnAugmentChanged;
    public enum Augment
    {
        None,
        Detacher
    }

    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float pitchLimitDeg = 90f;

    public Augment currentAugment;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Vector3 camForward = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 camRight = Vector3.Cross(Vector3.up, camForward);

        Vector3 forwardBackward = Input.GetAxisRaw("Vertical") * speed * Time.fixedDeltaTime * camForward;
        Vector3 leftRight = Input.GetAxisRaw("Horizontal") * speed * Time.fixedDeltaTime * camRight;

        rb.MovePosition(transform.position + forwardBackward + leftRight);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            print("ah");
            currentAugment = Augment.Detacher;
            OnAugmentChanged.Invoke(currentAugment);
        }

        // Mouse rotations
        transform.Rotate(Input.GetAxisRaw("Mouse X") * rotationSpeed * Time.deltaTime * Vector3.up);
        Vector3 oldRotationEuler = Camera.main.transform.rotation.eulerAngles;


        oldRotationEuler.x -= Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime;
        oldRotationEuler.x = RestrictAngle(oldRotationEuler.x, -pitchLimitDeg, pitchLimitDeg);

        Camera.main.transform.eulerAngles = oldRotationEuler;
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
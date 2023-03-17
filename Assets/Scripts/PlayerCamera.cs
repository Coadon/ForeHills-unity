using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform playerBody;

    [Header("Sensitivity Settings")]
    [SerializeField] private float mouseXSense;
    [SerializeField] private float mouseYSense;

    public static bool captureCursor = true;

    private float xRot = 0f;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (!captureCursor) {
            Cursor.lockState = CursorLockMode.None;
            return;
        } else
            Cursor.lockState = CursorLockMode.Locked;

        float mouseX = Input.GetAxis("Mouse X") * mouseXSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseYSense * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0.0f, 0.0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}

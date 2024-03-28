using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float xAxis;
    [SerializeField] float yAxis;

    [SerializeField] float xSensitivity;
    [SerializeField] float ySensitivity;
    float xRotation;

    [SerializeField] GameObject playerBody;
    public Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.Mouse.performed += t => LookHandler(t.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LookHandler(Vector2 axis)
    {
        xAxis = axis.x * Time.deltaTime * xSensitivity;
        yAxis = axis.y * Time.deltaTime * ySensitivity;

        xRotation -= yAxis;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, yAxis, transform.localRotation.z);
        playerBody.transform.Rotate(Vector3.up, xAxis);
    }
}
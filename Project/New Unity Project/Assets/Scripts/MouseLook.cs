using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
    private float yRotation;
    private float xRotation;
    private float lookSensitivity;
    private float yRotationVelocity;

    private string xAxisName;
    private string yAxisName;
    private float xRotationVelocity;

    public float curXrotation;
    public float curYrotation;

    public void Awake()
    {
        Initialize();
    }
    private void Update()
    {
        Rotator();
    }

    private void Rotator()
    {
        yRotation += Input.GetAxis(xAxisName) * lookSensitivity;
        xRotation -= Input.GetAxis(yAxisName) * lookSensitivity;

        xRotation = Mathf.Clamp(xRotation, -60, 45);

        curXrotation = Mathf.SmoothDamp(curXrotation, xRotation, ref xRotationVelocity, 0.1f);
        curYrotation = Mathf.SmoothDamp(curYrotation, yRotation, ref yRotationVelocity, 0.1f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void Initialize()
    {
        lookSensitivity = 5f;
        xAxisName = "Mouse X";
        yAxisName = "Mouse Y";
    }
}
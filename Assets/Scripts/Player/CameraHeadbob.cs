using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHeadbob : MonoBehaviour
{
    [Header("Behaviour Curves")]
    public AnimationCurve curveY;
    public AnimationCurve curveX;

    public AnimationCurve walkCurveY;
    public AnimationCurve walkCurveX;

    public AnimationCurve runCurveY;
    public AnimationCurve runCurveX;

    public AnimationCurve crouchCurveY;
    public AnimationCurve crouchCurveX;

    [Header("Important Components")]
    [SerializeField] GameObject targetCamera;
    public PlayerMovementStateMachine stateMachine;
    [SerializeField] Transform headbobStabilizer;

    [Header("Headbob Variables")]
    [SerializeField] float freq;
    public float y;
    public float x;

    [Header("Head Tilt Variables")]
    public float tiltAngle;
    public float tiltSpeed;
    public float horInput;
    public float minimumTilt;
    public float maximumTilt;

    private void Start()
    {
        AssignCurves(walkCurveX, walkCurveY);
        ChangeRotationTiltConstrains(-10, 10);
    }

    private void Update()
    {
        //horInput = Input.GetAxis("Horizontal");

        if(stateMachine.controllerVelocity > 0.2f)
        {
            DoBob();
            StabilizingHeadbob();
        }
    }

    private void LateUpdate()
    {
        RotateCameraOnTilt();
    }

    public void DoBob()
    {
        y = curveY.Evaluate(Time.time * freq);
        x = curveX.Evaluate(Time.time * freq);

        targetCamera.transform.localPosition = new Vector3(x,y,0);
    }

    public void AssignCurves(AnimationCurve xCurve, AnimationCurve yCurve)
    {
        curveX = xCurve;
        curveY = yCurve;
        curveY.postWrapMode = WrapMode.Loop;
        curveX.postWrapMode = WrapMode.Loop;
    }

    public void StabilizingHeadbob()
    {
        targetCamera.transform.LookAt(headbobStabilizer);
    }

    public void ChangeRotationTiltConstrains(float min, float max)
    {
        minimumTilt = min;
        maximumTilt = max;
    }

    public void RotateCameraOnTilt()
    {
        float desiredRotation = Mathf.Clamp(-stateMachine.xAxis * tiltAngle, minimumTilt, maximumTilt);
        float smoothRotation = Mathf.Lerp(targetCamera.transform.rotation.z, desiredRotation, tiltSpeed * Time.fixedDeltaTime);

        targetCamera.transform.localEulerAngles = new Vector3(targetCamera.transform.localRotation.x, targetCamera.transform.localRotation.y, smoothRotation);
    }
}
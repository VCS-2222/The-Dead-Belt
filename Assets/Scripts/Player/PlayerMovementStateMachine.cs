using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public interface IState
{
    public void OnActivated(PlayerMovementStateMachine StateMachine);
    public void OnUpdate();
    public void OnFixedUpdate();
    public void OnDeactivated();
}

[System.Serializable]
public class PlayerMovementStateMachine : MonoBehaviour
{
    [Header("State Status")]
    [SerializeField] string currentStatePlaying;
    [SerializeField] IState currentState;
    [SerializeField] IState lastState;

    public PmIdling idleState = new PmIdling();
    public PmWalking walkState = new PmWalking();
    public PmRunning runState = new PmRunning();
    public PmCrouching crouchState = new PmCrouching();
    public PmCrawling crawlingState = new PmCrawling();

    [Header("Movement Components and Variables")]
    [SerializeField] GameObject cameraHolder;
    [SerializeField] CharacterController controller;
    [SerializeField] GameObject groundCheckObject;
    [SerializeField] float currentSpeed;
    [SerializeField] CameraHeadbob headbob;
    public float zAxis;
    public float xAxis;

    public Vector3 direction;
    public float controllerVelocity;
    public Controls controls;

    private void Awake()
    {
        controls = new Controls();
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
        lastState = null;
        ChangeState(idleState);
    }

    private void Update()
    {
        currentStatePlaying = currentState.ToString();

        controls.Player.Movement.performed += t => AxisHandler(t.ReadValue<Vector2>());

        direction = transform.forward * zAxis + transform.right * xAxis;
        direction = Vector3.ClampMagnitude(direction, currentSpeed);

        if (currentState != null)
        {
            currentState.OnUpdate();
        }

        controllerVelocity = controller.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        controller.Move(direction * currentSpeed);

        if (currentState != null)
        {
            currentState.OnFixedUpdate();
        }
    }

    public void CheckForStateChange()
    {
        if (currentState == crawlingState)
        {
            headbob.AssignCurves(headbob.crawlingCurveX, headbob.crawlingCurveY);
            headbob.ChangeRotationTiltConstrains(-2.5f, 2.5f);
        }

        if (currentState == crouchState)
        {
            headbob.AssignCurves(headbob.crouchCurveX, headbob.crouchCurveY);
            headbob.ChangeRotationTiltConstrains(-5, 5);
        }

        if (currentState == walkState)
        {
            headbob.AssignCurves(headbob.walkCurveX, headbob.walkCurveY);
            headbob.ChangeRotationTiltConstrains(-10, 10);
        }

        if(currentState == runState)
        {
            headbob.AssignCurves(headbob.runCurveX, headbob.runCurveY);
            headbob.ChangeRotationTiltConstrains(-15,15);
        }
    }

    public void AxisHandler(Vector2 axis)
    {
        zAxis = axis.y;
        xAxis = axis.x;
    }

    public void ChangeController(float center, float height)
    {
        Vector3 targetCenterPos = new Vector3(0, center, 0);
        Vector3 startCenterPos = controller.center;

        float startHeight = controller.height;

        controller.center = Vector3.Lerp(startCenterPos, targetCenterPos, 2f * Time.deltaTime);

        controller.height = Mathf.Lerp(startHeight, height, 2f * Time.deltaTime);
    }

    public void ChangeControllerAndCamera(float newCameraPosition, float height)
    {
        cameraHolder.transform.localPosition = new Vector3(0, newCameraPosition, 0);
        controller.height = height;
    }

    public IEnumerator ChangeControllerGradually(float center, float height, float time)
    {
        float targetCenterPos = center;
        float startCenterPos = controller.center.y;

        float startHeight = controller.height;

        float elapse = 0;

        while(elapse < time)
        {
            if(controller.height > height)
            {
                controller.height -= 0.05f * Time.deltaTime;

                if (controller.height < height)
                {
                    controller.height = height;
                }
            }
            else
            {
                controller.height += 0.05f * Time.deltaTime;

                if (controller.height > height)
                {
                    controller.height = height;
                }
            }

            elapse++;

            yield return null;
        }

        //controller.center = new Vector3(0, targetCenterPos, 0);
    }

    public void SetSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }

    public IState ReturnCurrentState()
    {
        return currentState;
    }

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.OnDeactivated();
        }

        lastState = currentState;
        currentState = newState;
        currentState.OnActivated(this);
        CheckForStateChange();
    }
}
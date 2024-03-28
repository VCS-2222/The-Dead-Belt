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

    [Header("Movement Components and Variables")]
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

    public IEnumerator ChangeControllerGradually(float center, float height, float time)
    {
        float targetCenterPos = center;
        float startCenterPos = controller.center.y;

        float startHeight = controller.height;

        float elapse = 0;

        while(elapse < time)
        {
            //float wantedCenter = Mathf.Lerp(startCenterPos, targetCenterPos, 2.5f * Time.deltaTime);
            //controller.center = new Vector3(0, wantedCenter, 0);

            controller.height = startCenterPos - .5f * Time.deltaTime;

            if(controller.height < height)
            {
                controller.height = height;
            }

            yield return new WaitForSeconds(0.05f);

            elapse++;
        }
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
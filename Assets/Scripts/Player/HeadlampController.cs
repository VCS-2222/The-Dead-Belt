using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadlampController : MonoBehaviour
{
    [SerializeField] GameObject lamp;
    [SerializeField] bool isOn;
    public Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Weapons.Torch.performed += tag => SwitchLamp();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void SwitchLamp()
    {
        isOn = !isOn;

        if (isOn)
        {
            lamp.SetActive(true);
        }
        else
        {
            lamp.SetActive(false);
        }
    }
}
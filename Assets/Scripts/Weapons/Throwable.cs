using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [Header("Components and Values")]
    [SerializeField] GameObject throwable;
    [SerializeField] float throwPower;
    [SerializeField] Item itemID;
    public Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Weapons.Stow.performed += t => Stow();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        if (controls.Weapons.Fire.WasPressedThisFrame())
        {
            Throw();
        }
    }

    public void Throw()
    {
        GameObject thr = Instantiate(throwable, transform.position, transform.rotation);

        thr.GetComponent<Rigidbody>().AddForce(transform.forward * throwPower, ForceMode.Impulse);
        
        Inventory.Instance.ThrowSpecificItemAway(itemID);

        Stow();
    }
    public void Stow()
    {
        Inventory.Instance.StowWeaponAway();
    }

}
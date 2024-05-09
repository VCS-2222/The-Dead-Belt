using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUseManager : MonoBehaviour
{
    [SerializeField] GameObject currentWeapon;
    [SerializeField] Transform shootPoint;
    [SerializeField] bool currentWeaponMelee;
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
        
    }

    public void SetCurrentWeapon(GameObject newWeapon)
    {
        if(currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        currentWeapon = newWeapon;
    }

    public void AuthentificationOfCurrentWeapon()
    {
        if (currentWeapon.GetComponent<ClipFedGun>() != null)
        {
            controls.Weapons.Fire.performed += t => currentWeapon.GetComponent<ClipFedGun>().StartCoroutine(currentWeapon.GetComponent<ClipFedGun>().Shoot(shootPoint, 55f, .1f));
        }
        else
        {
            return;
        }
    }
}

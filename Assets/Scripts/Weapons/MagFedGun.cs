using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagFedGun : MonoBehaviour
{
    [Header("Gun Variables")]
    [SerializeField] int currentAmmo;
    [SerializeField] int maxAmmo;
    public Controls controls;
    [SerializeField] float range;
    [SerializeField] float delay;
    [SerializeField] Transform shootPoint;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private void Awake()
    {
        controls = new Controls();
        controls.Weapons.Fire.performed += t => StartCoroutine(Shoot(shootPoint, delay));
        controls.Weapons.Reload.performed += t => Reload();
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

    public void TestShoot()
    {
        print(transform.name + " SHOT");
    }

    public IEnumerator Shoot(Transform origin, float delayToShoot)
    {
        if (currentAmmo <= 0) yield break;

        animator.SetTrigger("shot");

        yield return new WaitForSeconds(delayToShoot);

        RaycastHit hit;
        Physics.Raycast(origin.position, origin.transform.forward, out hit, range);

        currentAmmo--;

        if(currentAmmo == 0)
        {
            animator.SetBool("empty", true);
        }

        if (hit.collider != null)
        {
            print(hit.collider.gameObject.name);
            if (hit.collider.gameObject.GetComponent<ZombieMovement>() != null)
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    public void AssignComponents(Transform shootingPoint)
    {
        shootPoint = shootingPoint;
    }

    public void Reload()
    {
        if(currentAmmo <= 0)
        {
            animator.SetTrigger("reload");
            currentAmmo = maxAmmo;
            animator.SetBool("empty", false);
        }
        else
        {
            animator.SetTrigger("reload");
            currentAmmo = maxAmmo;
            animator.SetBool("empty", false);
        }
    }

    public void Stow()
    {
        Inventory.Instance.StowWeaponAway();
    }
}
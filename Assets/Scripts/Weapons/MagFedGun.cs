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
    [SerializeField] float damagePerBullet;
    bool isShooting;

    [Header("Gun Components")]
    [SerializeField] Item ammo;
    [SerializeField] Transform shootPoint;
    [SerializeField] AudioSource shootSound;
    [SerializeField] AudioClip[] gunShots;

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

    private void Start()
    {
        isShooting = false;
    }

    public IEnumerator Shoot(Transform origin, float delayToShoot)
    {
        if (isShooting) yield break;

        if (currentAmmo <= 0) yield break;

        animator.SetTrigger("shot");

        isShooting = true;

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
            if (hit.collider.tag == "Zombie")
            {
                hit.collider.gameObject.GetComponent<ZombieStats>().TakeDamage(damagePerBullet);
            }
        }

        isShooting = false;
    }

    public void PlayRandomShotSound()
    {
        int ranNum = Random.Range(0, gunShots.Length);
        shootSound.PlayOneShot(gunShots[ranNum]);
    }

    public void AssignComponents(Transform shootingPoint)
    {
        shootPoint = shootingPoint;
    }

    public void Reload()
    {
        if(currentAmmo <= 0 && Inventory.Instance.ReturnAmmo() == true)
        {
            animator.SetTrigger("reload");
            currentAmmo = maxAmmo;
            animator.SetBool("empty", false);
        }
        else if(currentAmmo > 0 && Inventory.Instance.ReturnAmmo() == true)
        {
            animator.SetTrigger("reload");
            currentAmmo = maxAmmo;
            animator.SetBool("empty", false);
        }
    }

    public void Stow()
    {
        if(currentAmmo == maxAmmo)
        {
            Inventory.Instance.AddItem(ammo);
        }

        Inventory.Instance.StowWeaponAway();
    }
}
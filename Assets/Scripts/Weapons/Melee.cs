using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [Header("Weapon Variables")]
    [SerializeField] float range;
    [SerializeField] float delay;
    [SerializeField] float damage;

    [Header("Weapon Components")]
    [SerializeField] Transform attackPoint;
    [SerializeField] AudioSource attackSounds;
    [SerializeField] AudioClip[] attackAudioClips;
    public Controls controls;

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] int randomAnimations;

    private void Awake()
    {
        controls = new Controls();
        controls.Weapons.Fire.performed += t => StartCoroutine(Attack(attackPoint, delay));
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

    public IEnumerator Attack(Transform origin, float delayToShoot)
    {
        animator.SetTrigger("shot");

        animator.SetInteger("randomattack", Random.Range(0, randomAnimations));

        yield return new WaitForSeconds(delayToShoot);

        RaycastHit hit;
        Physics.Raycast(origin.position, origin.transform.forward, out hit, range);

        if (hit.collider != null)
        {
            print(hit.collider.gameObject.name);
            if (hit.collider.tag == "Zombie")
            {
                hit.collider.gameObject.GetComponent<ZombieStats>().TakeDamage(damage);
            }
        }
    }

    public void PlayRandomShotSound()
    {
        int ranNum = Random.Range(0, attackAudioClips.Length);
        attackSounds.PlayOneShot(attackAudioClips[ranNum]);
    }

    public void AssignComponents(Transform shootingPoint)
    {
        attackPoint = shootingPoint;
    }

    public void Stow()
    {
        Inventory.Instance.StowWeaponAway();
    }
}
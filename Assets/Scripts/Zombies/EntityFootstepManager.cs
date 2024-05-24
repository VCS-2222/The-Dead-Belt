using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFootstepManager : MonoBehaviour
{
    [Header("Footstep Lists")]
    [SerializeField] AudioClip[] rockWalk;
    [SerializeField] AudioClip[] woodWalk;
    [SerializeField] AudioClip[] metalWalk;
    [SerializeField] AudioClip[] waterWalk;
    [SerializeField] AudioClip[] gravelWalk;
    [SerializeField] AudioClip[] tileWalk;

    [Header("Needed Components")]
    [SerializeField] AudioSource footstepSource;

    public void PlayFootstep()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            switch (hit.collider.tag)
            {
                case "Rock":
                    footstepSource.PlayOneShot(rockWalk[Random.Range(0, rockWalk.Length - 1)]);
                    break;
                case "Tile":
                    footstepSource.PlayOneShot(tileWalk[Random.Range(0, tileWalk.Length - 1)]);
                    break;
                case "Water":
                    footstepSource.PlayOneShot(waterWalk[Random.Range(0, waterWalk.Length - 1)]);
                    break;
                case "Metal":
                    footstepSource.PlayOneShot(metalWalk[Random.Range(0, metalWalk.Length - 1)]);
                    break;
                case "Wood":
                    footstepSource.PlayOneShot(woodWalk[Random.Range(0, woodWalk.Length - 1)]);
                    break;
                case "Gravel":
                    footstepSource.PlayOneShot(gravelWalk[Random.Range(0, gravelWalk.Length - 1)]);
                    break;
                default:
                    footstepSource.PlayOneShot(rockWalk[Random.Range(0, rockWalk.Length - 1)]);
                    break;
            }
        }
    }
}
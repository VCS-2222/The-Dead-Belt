using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepManager : MonoBehaviour
{
    [Header("Footstep Lists")]
    [SerializeField] AudioClip[] rockWalk;
    [SerializeField] AudioClip[] rockRun;
    [SerializeField] AudioClip[] woodWalk;
    [SerializeField] AudioClip[] woodRun;
    [SerializeField] AudioClip[] metalWalk;
    [SerializeField] AudioClip[] metalRun;
    [SerializeField] AudioClip[] waterWalk;
    [SerializeField] AudioClip[] waterRun;
    [SerializeField] AudioClip[] gravelWalk;
    [SerializeField] AudioClip[] gravelRun;
    [SerializeField] AudioClip[] tileWalk;
    [SerializeField] AudioClip[] tileRun;

    [Header("Components")]
    [SerializeField] CharacterController controller;
    [SerializeField] float playerMagnitude;
    [SerializeField] float crawlingTimer;
    [SerializeField] float crouchTimer;
    [SerializeField] float walkingTimer;
    [SerializeField] float runningTimer;
    [SerializeField] float timer;
    [SerializeField] AudioSource footstepSource;

    private void Start()
    {
        timer = walkingTimer;
    }

    private void Update()
    {
        playerMagnitude = controller.velocity.magnitude;

        timer -= Time.deltaTime;

        PlayFootstep();
    }

    public void PlayFootstep()
    {
        if (playerMagnitude > 0.1f)
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
            {
                if(timer < 0)
                {
                    if (playerMagnitude > 2f)
                    {
                        switch (hit.collider.tag)
                        {
                            case "Rock":
                                footstepSource.PlayOneShot(rockRun[Random.Range(0, rockRun.Length - 1)]);
                                break;
                            case "Tile":
                                footstepSource.PlayOneShot(tileRun[Random.Range(0, tileRun.Length - 1)]);
                                break;
                            case "Water":
                                footstepSource.PlayOneShot(waterRun[Random.Range(0, waterRun.Length - 1)]);
                                break;
                            case "Metal":
                                footstepSource.PlayOneShot(metalRun[Random.Range(0, metalRun.Length - 1)]);
                                break;
                            case "Wood":
                                footstepSource.PlayOneShot(woodRun[Random.Range(0, woodRun.Length - 1)]);
                                break;
                            case "Gravel":
                                footstepSource.PlayOneShot(gravelRun[Random.Range(0, gravelRun.Length - 1)]);
                                break;
                            default:
                                footstepSource.PlayOneShot(rockRun[Random.Range(0, rockRun.Length - 1)]);
                                break;
                        }

                        timer = runningTimer;
                    }
                    else if(playerMagnitude < 2f && playerMagnitude > 1f)
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

                        timer = walkingTimer;
                    }
                    else if (playerMagnitude < 1f)
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

                        timer = crouchTimer;
                    }
                }
            }
        }
    }
}
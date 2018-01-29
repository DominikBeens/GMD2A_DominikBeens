using UnityEngine;
using System.Collections;

// CAREFUL, A BUNCH OF INEFFICIENT, LAZY AND ABSOLUTELY GARBAGE CODE BELOW
public class AI : MonoBehaviour
{

    private Transform snake;
    private GameObject pickup;

    public bool canGoLeft = true;
    public bool canGoRight = true;
    public bool canGoUp = true;
    public bool canGoDown = true;

    public bool collisionFront;
    public bool collisionLeft;
    public bool collisionRight;

    private void Update()
    {
        if (GameManager.instance.autopilot)
        {
            snake = GameManager.instance.snakeMovement.gameObject.transform;
            pickup = PickupSpawner.instance.spawnedPickup;

            if (pickup != null)
            {
                if (Mathf.Abs((pickup.transform.position - snake.position).x) > 0.15f)
                {
                    if (snake.position.x > pickup.transform.position.x)
                    {
                        if (snake.transform.localRotation != Quaternion.Euler(new Vector3(0, 90, 0)) && canGoLeft)
                        {
                            GoLeft();
                        }
                        else
                        {
                            if (canGoLeft)
                            {
                                GoUp();
                                canGoLeft = false;
                                StartCoroutine(GoLeftTimer());
                            }
                        }
                    }
                    else if (snake.position.x < pickup.transform.position.x)
                    {
                        if (snake.transform.localRotation != Quaternion.Euler(new Vector3(0, 270, 0)) && canGoRight)
                        {
                            GoRight();
                        }
                        else
                        {
                            if (canGoRight)
                            {
                                GoUp();
                                canGoRight = false;
                                StartCoroutine(GoRightTimer());
                            }
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs((pickup.transform.position - snake.position).z) > 0.15f)
                    {
                        if (snake.position.z > pickup.transform.position.z)
                        {
                            if (snake.transform.localRotation != Quaternion.Euler(new Vector3(0, 0, 0)) && canGoDown)
                            {
                                GoDown();
                            }
                            else
                            {
                                if (canGoDown)
                                {
                                    GoRight();
                                    canGoDown = false;
                                    StartCoroutine(GoDownTimer());
                                }
                            }
                        }
                        else if (snake.position.z < pickup.transform.position.z)
                        {
                            if (snake.transform.localRotation != Quaternion.Euler(new Vector3(0, 180, 0)) && canGoUp)
                            {
                                GoUp();
                            }
                            else
                            {
                                if (canGoUp)
                                {
                                    GoRight();
                                    canGoUp = false;
                                    StartCoroutine(GoUpTimer());
                                }
                            }
                        }
                    }
                }
            }

            if (collisionFront && !collisionLeft && !collisionRight)
            {
                print("COLLISION FRONT, GO RIGHT");

                if (snake.transform.localRotation == Quaternion.Euler(new Vector3(0, 0, 0)))
                {
                    GoRight();
                    canGoRight = false;
                    StartCoroutine(GoRightTimer());

                    ResetCollisionDetections();

                }
                else if (snake.transform.localRotation == Quaternion.Euler(new Vector3(0, 90, 0)))
                {
                    GoDown();
                    canGoDown = false;
                    StartCoroutine(GoDownTimer());

                    ResetCollisionDetections();

                }
                else if (snake.transform.localRotation == Quaternion.Euler(new Vector3(0, 180, 0)))
                {
                    GoLeft();
                    canGoLeft = false;
                    StartCoroutine(GoLeftTimer());

                    ResetCollisionDetections();

                }
                else if (snake.transform.localRotation == Quaternion.Euler(new Vector3(0, 270, 0)))
                {
                    GoUp();
                    canGoUp = false;
                    StartCoroutine(GoUpTimer());

                    ResetCollisionDetections();

                }

                ResetCollisionDetections();
            }
            else if (collisionFront && collisionLeft && !collisionRight)
            {
                print("COLLISION FRONT AND LEFT, GO RIGHT");

                if (snake.transform.localRotation == Quaternion.Euler(new Vector3(0, 0, 0)))
                {
                    GoRight();
                    canGoRight = false;
                    StartCoroutine(GoRightTimer());

                    ResetCollisionDetections();

                }
                else if (snake.transform.localRotation == Quaternion.Euler(new Vector3(0, 90, 0)))
                {
                    GoDown();
                    canGoDown = false;
                    StartCoroutine(GoDownTimer());

                    ResetCollisionDetections();

                }
                else if (snake.transform.localRotation == Quaternion.Euler(new Vector3(0, 180, 0)))
                {
                    GoLeft();
                    canGoLeft = false;
                    StartCoroutine(GoLeftTimer());

                    ResetCollisionDetections();

                }
                else if (snake.transform.localRotation == Quaternion.Euler(new Vector3(0, 270, 0)))
                {
                    GoUp();
                    canGoUp = false;
                    StartCoroutine(GoUpTimer());

                    ResetCollisionDetections();

                }

                ResetCollisionDetections();
            }
            else if (collisionFront && !collisionLeft && collisionRight)
            {
                print("COLLISION FRONT AND RIGHT, GO LEFT");

                if (snake.transform.localRotation == Quaternion.Euler(new Vector3(0, 0, 0)))
                {
                    GoLeft();
                    canGoLeft = false;
                    StartCoroutine(GoLeftTimer());

                    ResetCollisionDetections();

                }
                else if (snake.transform.localRotation == Quaternion.Euler(new Vector3(0, 90, 0)))
                {
                    GoUp();
                    canGoUp = false;
                    StartCoroutine(GoUpTimer());

                    ResetCollisionDetections();

                }
                else if (snake.transform.localRotation == Quaternion.Euler(new Vector3(0, 180, 0)))
                {
                    GoRight();
                    canGoRight = false;
                    StartCoroutine(GoRightTimer());

                    ResetCollisionDetections();

                }
                else if (snake.transform.localRotation == Quaternion.Euler(new Vector3(0, 270, 0)))
                {
                    GoDown();
                    canGoDown = false;
                    StartCoroutine(GoDownTimer());

                    ResetCollisionDetections();

                }

                ResetCollisionDetections();
            }
            else if (!collisionFront && !collisionLeft && collisionRight)
            {
                print("COLLISION RIGHT, CANT GO THERE");

                canGoRight = false;
                StartCoroutine(GoRightTimer());

                ResetCollisionDetections();
            }
            else if (!collisionFront && collisionLeft && !collisionRight)
            {
                print("COLLISION LEFT, CANT GO THERE");

                canGoLeft = false;
                StartCoroutine(GoLeftTimer());

                ResetCollisionDetections();
            }
            else
            {
                ResetCollisionDetections();
            }
        }
    }

    private void GoUp()
    {
        snake.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        GameManager.instance.snakeMovement.RecordPosition();
    }

    private void GoDown()
    {
        snake.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        GameManager.instance.snakeMovement.RecordPosition();
    }

    private void GoLeft()
    {
        snake.transform.localRotation = Quaternion.Euler(new Vector3(0, 270, 0));
        GameManager.instance.snakeMovement.RecordPosition();
    }

    private void GoRight()
    {
        snake.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
        GameManager.instance.snakeMovement.RecordPosition();
    }

    private IEnumerator GoUpTimer()
    {
        canGoUp = false;

        yield return new WaitForSeconds(1.5f);

        canGoUp = true;
    }

    private IEnumerator GoDownTimer()
    {
        canGoDown = false;

        yield return new WaitForSeconds(1.5f);

        canGoDown = true;
    }

    private IEnumerator GoLeftTimer()
    {
        canGoLeft = false;

        yield return new WaitForSeconds(1.5f);

        canGoLeft = true;
    }

    private IEnumerator GoRightTimer()
    {
        canGoRight = false;

        yield return new WaitForSeconds(1.5f);

        canGoRight = true;
    }

    private void ResetCollisionDetections()
    {
        collisionFront = false;
        collisionLeft = false;
        collisionRight = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{

    public GameObject bullet;
    public Transform bulletSpawn;
    public float bulletFireForce;

    public GameObject explosion;

    public GunMovement gunMovement;

    public Transform player;

    public float shakeDuration;
    public float shakeAmountXY;
    public float shakeAmountZ;
    public float rotateAmount;

    public Transform quitGameButton;
    public Transform playGameButton;

    public Animator introButtonsAnimator;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void PlayGameButton()
    {
        StartCoroutine(PlayGame());
    }

    public void QuitGameButton()
    {
        StartCoroutine(QuitGame());
    }

    private IEnumerator PlayGame()
    {
        Vector3 clickPos = gunMovement.GetWorldPositionOnPlane(Input.mousePosition, player.transform.position.z);

        GameObject bulletInstance = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletFireForce, ForceMode.Impulse);
        Camera.main.transform.GetComponent<CameraShake>().Shake(shakeDuration, shakeAmountXY, shakeAmountZ, rotateAmount);

        while (Vector3.Distance(clickPos, bulletInstance.transform.position) > 1f)
        {
            yield return null;
        }

        Destroy(bulletInstance);
        Instantiate(explosion, quitGameButton.position, Quaternion.identity);
        Camera.main.transform.GetComponent<CameraShake>().Shake(shakeDuration, shakeAmountXY, shakeAmountZ, rotateAmount);
        introButtonsAnimator.enabled = true;

        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator QuitGame()
    {
        Vector3 clickPos = gunMovement.GetWorldPositionOnPlane(Input.mousePosition, player.transform.position.z);

        GameObject bulletInstance = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletFireForce, ForceMode.Impulse);
        Camera.main.transform.GetComponent<CameraShake>().Shake(shakeDuration, shakeAmountXY, shakeAmountZ, rotateAmount);

        while (Vector3.Distance(clickPos, bulletInstance.transform.position) > 1f)
        {
            yield return null;
        }

        Destroy(bulletInstance);
        Instantiate(explosion, quitGameButton.position, Quaternion.identity);
        Camera.main.transform.GetComponent<CameraShake>().Shake(shakeDuration, shakeAmountXY, shakeAmountZ, rotateAmount);
        introButtonsAnimator.enabled = true;

        yield return new WaitForSeconds(0.6f);
        Application.Quit();
    }
}

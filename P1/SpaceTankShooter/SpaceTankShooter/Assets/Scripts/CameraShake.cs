using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    private static bool shake;

    private Vector3 startPos;
    private Vector3 startRot;

    private float shakeTime;

    public float shakeSpeed;

    private float shakeAmountXY;
    private float shakeAmountZ;

    private float rotateAmount;

    private void Update()
    {
        if (shake)
        {
            Vector3 nextPos = new Vector3(Random.Range(startPos.x - shakeAmountXY, startPos.x + shakeAmountXY), 
                                          Random.Range(startPos.y - shakeAmountXY, startPos.y + shakeAmountXY), 
                                          Random.Range(startPos.z - shakeAmountZ, startPos.z + shakeAmountZ));

            Quaternion nextRot = Quaternion.Euler(new Vector3(Random.Range(startRot.x - rotateAmount, startRot.x + rotateAmount),
                                                              Random.Range(startRot.y - rotateAmount, startRot.y + rotateAmount),
                                                              Random.Range(startRot.z - rotateAmount, startRot.z + rotateAmount)));

            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, nextPos, Time.deltaTime * shakeSpeed);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, nextRot, Time.deltaTime * shakeSpeed);

            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
            }
            else
            {
                Camera.main.transform.position = startPos;
                Camera.main.transform.rotation = Quaternion.Euler(startRot);
                shake = false;
            }
        }
        else
        {
            shakeTime = 0;
        }
    }

    public void Shake(float duration, float amountXY, float amountZ, float amountRotate)
    {
        if (!shake)
        {
            startPos = Camera.main.transform.position;
            startRot = Camera.main.transform.rotation.eulerAngles;
            shakeAmountXY = amountXY;
            shakeAmountZ = amountZ;
            rotateAmount = amountRotate;

            shakeTime += duration;
            shake = true;
        }
    }
}

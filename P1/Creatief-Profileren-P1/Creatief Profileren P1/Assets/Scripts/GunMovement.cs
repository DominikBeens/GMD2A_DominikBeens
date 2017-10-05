using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{

    private Vector3 startPosition;

    private Transform weaponHolder;
    private Vector3 weaponHolderDefaultPos;

    [Header("Gun LookAt Object Movement")]
    public float movementSpeed;
    public float movementFallBackSpeed;
    [Space(10)]
    public float maxHorizontalMovement = 3f;
    public float maxVerticalMovement = 3f;

    [Header("Weapon Holder Movement")]
    public float horizontalBumpFallBackSpeed;

    private void Awake()
    {
        startPosition = transform.localPosition;

        weaponHolder = GameObject.FindWithTag("WeaponHolder").transform;
        weaponHolderDefaultPos = weaponHolder.localPosition;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float y = Mathf.Clamp(Input.GetAxis("Mouse X"), -maxHorizontalMovement, maxHorizontalMovement);
        float x = Mathf.Clamp(Input.GetAxis("Mouse Y"), -maxVerticalMovement, maxVerticalMovement);

        Vector3 movementPosition = new Vector3(transform.localPosition.x + y, transform.localPosition.y + x, transform.localPosition.z);

        transform.localPosition = Vector3.Lerp(transform.localPosition, movementPosition, Time.deltaTime * movementSpeed);

        weaponHolder.localPosition = Vector3.Lerp(weaponHolder.localPosition, weaponHolderDefaultPos, Time.deltaTime * horizontalBumpFallBackSpeed);
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * movementFallBackSpeed);
    }

    public void BumpVertical(float distance, float speed)
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y + distance, transform.localPosition.z), Time.deltaTime * speed);
    }

    public void BumpHorizontal(float distance, float speed)
    {
        weaponHolder.localPosition = Vector3.Lerp(weaponHolder.localPosition, new Vector3(weaponHolder.localPosition.x, weaponHolder.localPosition.y, weaponHolder.localPosition.z - distance), Time.deltaTime * speed);
    }
}

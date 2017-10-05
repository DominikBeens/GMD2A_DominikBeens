using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    private Transform player;
    private Transform mainCam;

    private Rigidbody rb;
    private float currentCamRotX;

    public float gravity = 10f;

    [Header("Moving")]
    public float moveSpeed;
    public float walkSpeed;
    public float walkBackSpeed;
    public float runSpeed;
    public float strafeSpeed;

    [Header("Jumping")]
    public float jumpForce;

    [Header("Looking")]
    public float lookSensitivity;
    public float lookRotLimit = 90;

    [Header("Head Bob")]
    public float headBobStrengthMultiplier;

    public float headBobHorizontal;
    public float headBobVerticalA;
    public float headBobVerticalB;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        mainCam = Camera.main.transform;
        rb = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        SetMoveSpeed();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButton("Left Shift"))
        {
            moveSpeed = moveSpeed * 1.5f;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();

        HeadBob();
    }

    private void SetMoveSpeed()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            moveSpeed = walkSpeed;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            moveSpeed = walkBackSpeed;
        }
        else if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            moveSpeed = strafeSpeed;
        }
        else
        {
            moveSpeed = 0;
        }
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 horizontal = player.right * x;
        Vector3 vertical = player.forward * z;

        Vector3 velocity = (horizontal + vertical).normalized * (Time.deltaTime * moveSpeed);

        Vector3 currentPosition = new Vector3(player.position.x, player.position.y, player.position.z);
        currentPosition.y -= gravity * Time.deltaTime;

        rb.MovePosition(currentPosition + velocity);
    }

    private void Rotate()
    {
        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");

        Vector3 playerRot = new Vector3(player.rotation.x, y, player.rotation.z) * lookSensitivity;
        float lookX = x * lookSensitivity;

        rb.MoveRotation(player.rotation * Quaternion.Euler(playerRot));

        currentCamRotX -= lookX;
        currentCamRotX = Mathf.Clamp(currentCamRotX, -lookRotLimit, lookRotLimit);
        mainCam.transform.localEulerAngles = new Vector3(currentCamRotX, 0, 0);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

    private void HeadBob()
    {
        headBobVerticalA = (2 * moveSpeed) * headBobStrengthMultiplier;

        float t = Time.time;
        float x = Mathf.Sin(t) * headBobHorizontal;
        float y = Mathf.Cos(t * headBobVerticalA) * headBobVerticalB;
        mainCam.transform.localPosition = new Vector3(x, y, 0);
    }
}

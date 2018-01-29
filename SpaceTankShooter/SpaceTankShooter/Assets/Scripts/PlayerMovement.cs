using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement instance;

    private Rigidbody rb;
    private Animator anim;

    public GunMovement gunMovement;

    public float moveSpeed;

    public float jumpForce;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");

        Vector3 horizontal = transform.right * x;

        Vector3 velocity = (horizontal).normalized * (Time.deltaTime * moveSpeed);

        Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //currentPosition.y -= gravity * Time.deltaTime;

        rb.MovePosition(currentPosition + velocity);

        anim.SetFloat("moveSpeed", x);
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}

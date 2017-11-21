using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;
    private Animator anim;

    public float moveSpeed;

    public float jumpForce;

    [Header("Dash")]
    public float dashTime;
    public float dashSpeed;

    public float dashTimer;
    private float dashCooldown;

    private float dashCooldownFill;
    private float dashCooldownAmount;

    public ParticleSystem dashTrailParticle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        DashCooldownToUI();

        if (Input.GetButtonDown("Jump"))
        {
            //Jump();
        }

        if (Input.GetButtonDown("LeftShift") && Time.time >= dashCooldown)
        {
            dashCooldown = Time.time + 1f / dashTimer;
            dashCooldownAmount = dashCooldown - Time.time;
            StartCoroutine(Dash(dashTime));
        }
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

    private IEnumerator Dash(float time)
    {
        dashCooldownFill = 0;
        dashTrailParticle.Play();

        float curSpeed = moveSpeed;
        moveSpeed = dashSpeed;

        yield return new WaitForSeconds(time);

        moveSpeed = curSpeed;
    }

    private void DashCooldownToUI()
    {
        if (dashCooldownFill < 1)
        {
            dashCooldownFill += 1 / dashCooldownAmount * Time.deltaTime;
            UIManager.instance.dashCooldown.fillAmount = dashCooldownFill;
        }
        else
        {
            dashCooldownFill = 1;
        }
    }
}

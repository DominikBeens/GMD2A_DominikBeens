using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    private Transform player;
    private Transform target;

    public enum Type
    {
        Enemy,
        Dummy
    }
    public Type type;

    public float moveSpeed;

    public float health;

    public int damage;

    public GameObject healthPanel;
    public Text healthText;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        if (type == Type.Enemy)
        {
            target = Altar.instance.altarObjectPosition;
        }
    }

    private void Update()
    {
        healthPanel.transform.LookAt(2 * transform.position - player.position);
        healthText.text = "HP: " + health;

        if (health <= 0)
        {
            Die();
        }

        if (type == Type.Enemy)
        {
            if (Vector3.Distance(target.position, transform.position) > target.localScale.x)
            {
                transform.LookAt(target);
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed);
                //transform.Translate(target.position * (Time.deltaTime * moveSpeed));
            }
            else
            {
                Altar.instance.currentHealth -= damage;
                Destroy(gameObject);
            }
        }
    }

    public void Hit(float damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
    }

    private void Die()
    {
        if (type == Type.Enemy)
        {
            Spawner.instance.aliveEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}

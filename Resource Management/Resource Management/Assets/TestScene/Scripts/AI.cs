using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    public Text enumText;
    private Animator anim;

    public enum State
    {
        Idle,
        Walking,
        Jumping,
        Dancing
    }
    public State state;

    public NavMeshAgent agent;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        enumText.text = "Current State: " + state.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Ground")
                {
                    print(hit.point);
                    agent.SetDestination(hit.point);
                }
            }
        }
    }

    public void SwitchStates()
    {
        int randomInt = Random.Range(0, System.Enum.GetValues(typeof(State)).Length);

        if (randomInt == (int)state)
        {
            SwitchStates();
            return;
        }

        switch (randomInt)
        {
            case 0:
                state = State.Idle;
                anim.SetTrigger("Idle");
                break;
            case 1:
                state = State.Walking;
                anim.SetTrigger("Walking");
                break;
            case 2:
                state = State.Jumping;
                anim.SetTrigger("Jumping");
                break;
            case 3:
                state = State.Dancing;
                anim.SetTrigger("Dancing");
                break;
        }
    }
}

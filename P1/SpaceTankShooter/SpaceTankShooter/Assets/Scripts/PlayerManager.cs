using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    public static Transform player;
    [HideInInspector]
    public PlayerMovement playerMovement;

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

        player = GameObject.FindWithTag("Player").transform;
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }
}

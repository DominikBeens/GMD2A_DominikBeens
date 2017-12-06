using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public Image weaponCharge;

    public Image leftMouseButtonCooldown;
    public Image rightMouseButtonCooldown;

    public GameObject introPanel;
    public GameObject pausePanel;

    public GameObject timerTextObject;
    [HideInInspector]
    public TextMeshProUGUI timerText;

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

        timerText = timerTextObject.GetComponent<TextMeshProUGUI>();
    }
}

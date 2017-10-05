using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{

    private RectTransform rectTransform;
    private Text textComponent;

    private bool grow = true;

    public string text;

    public float startSize;
    public float desiredSize;
    public float maxSize;

    public float growSpeed;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textComponent = GetComponent<Text>();
    }

    private void Start()
    {
        textComponent.text = text;
    }

    private void Update()
    {
        Animate(startSize, desiredSize, maxSize, growSpeed);
    }

    private void Animate(float startSize, float desiredSize, float maxSize, float growSpeed)
    {
        if (grow)
        {
            if (textComponent.color.a < 1)
            {
                textComponent.color += new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, Time.deltaTime * growSpeed);
            }

            rectTransform.localScale += new Vector3(Time.deltaTime * growSpeed, Time.deltaTime * growSpeed);

            if (rectTransform.localScale.x > maxSize)
            {
                grow = false;
            }
        }
        else
        {
            if (desiredSize < rectTransform.localScale.x)
            {
                rectTransform.localScale -= new Vector3(Time.deltaTime * growSpeed, Time.deltaTime * growSpeed);
            }
            else
            {
                textComponent.color -= new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, Time.deltaTime);
            }

            if (textComponent.color.a < 0.15f)
            {
                Destroy(gameObject);
            }
        }
    }
}

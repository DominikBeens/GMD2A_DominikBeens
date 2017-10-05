using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{

    private Enemy previousTarget;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            if (hit.transform.GetComponent<Enemy>() != null)
            {
                if (previousTarget != null)
                {
                    previousTarget.GetComponent<Enemy>().healthPanel.SetActive(false);
                }
                previousTarget = hit.transform.GetComponent<Enemy>();

                hit.transform.GetComponent<Enemy>().healthPanel.SetActive(true);
            }
            else
            {
                if (previousTarget != null)
                {
                    previousTarget.GetComponent<Enemy>().healthPanel.SetActive(false);
                }
            }

        }

        //RaycastHit hithit;
        //if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hithit))
        //{
        //    if (hit.transform.GetComponent<Button>() != null)
        //    {
        //        hit.transform.GetComponent<Button>().onClick.Invoke();
        //    }
        //    //print(hit.transform.name);

        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [Header("Worker Info")]
    public GameObject workerInfoPanel;
    public Text workerStatsText;
    public Text workerJobDescriptionText;
    [Space(10)]
    // Displays the workers health.
    public Text workerHealthText;
    public Image workerHealthFill;
    [Space(10)]
    // Displays the workers hunger.
    public Text workerHungerText;
    public Image workerHungerFill;

    private static Worker selectedWorker;

    [Header("Resource Panel")]
    public Text oreText;
    public Text woodText;
    public Text grainText;
    public Text breadText;

    // Singleton.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        // If the player clicks, it shoots a ray to where he clicked. If the player clicked on a worker it will show that workers stats.
        // If the player clicked on something else it will deselect the current selected worker if there is one.
        if (Input.GetMouseButtonDown(0))
        {
            // Shoot the ray.
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                // Check what we hit.
                if (hit.transform.GetComponent<Worker>() != null)
                {
                    SelectWorker(hit.transform.GetComponent<Worker>());
                }
                else
                {
                    DeselectWorker();
                }
            }
        }

        // If the workerInfoPanel is active it means that there is a selected worker. If theres a selected worker it should update the worker panel to accurately update and show his current stats.
        if (workerInfoPanel.activeInHierarchy)
        {
            UpdateWorkerPanel();

            // Extra button to deselect a worker.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DeselectWorker();
            }
        }
    }

    // Shows an UI panel with a workers stats when clicked on.
    public void SelectWorker(Worker worker)
    {
        // Deselects the previous worker first if there is one.
        DeselectWorker();

        selectedWorker = worker;
        selectedWorker.selectedObject.SetActive(true);
        workerInfoPanel.SetActive(true);
    }

    // Sets the worker UI panel inactive.
    public void DeselectWorker()
    {
        if (selectedWorker != null)
        {
            selectedWorker.selectedObject.SetActive(false);
            workerInfoPanel.SetActive(false);
            selectedWorker = null;
        }
    }

    private void UpdateWorkerPanel()
    {
        // Displays health.
        workerHealthText.text = "Health: " + selectedWorker.stats.health.currentAmount.ToString();
        workerHealthFill.fillAmount = selectedWorker.stats.health.currentAmount / selectedWorker.stats.health.maxAmount;

        // Displays hunger.
        workerHungerText.text = "Hunger: " + selectedWorker.stats.hunger.currentAmount.ToString();
        workerHungerFill.fillAmount = selectedWorker.stats.hunger.currentAmount / selectedWorker.stats.hunger.maxAmount;

        // Displays the workers current job.
        string job = "";
        switch (selectedWorker.state)
        {
            case Worker.State.Wandering:

                job = "Wandering around";
                break;
            case Worker.State.ChoppingTrees:

                job = "Chopping trees";
                break;
            case Worker.State.Mining:

                job = "Mining";
                break;
            case Worker.State.Smithing:

                job = "Smithing";
                break;
            case Worker.State.HarvestingGrain:

                job = "Harvesting Grain";
                break;
            case Worker.State.Baking:

                job = "Baking";
                break;
        }
        workerJobDescriptionText.text = job;

        // Rotates the 'selected image' to always face the player.
        selectedWorker.selectedObject.transform.LookAt(Camera.main.transform);
    }

    // Sets the resource text with the current resource amount.
    public void UpdateResourcePanel()
    {
        if (ResourceManager.instance.inventory.ContainsSpecificItem("ore"))
        {
            oreText.text = "Ore: " + ResourceManager.instance.inventory.GetSpecificItem("ore").quantity.ToString();
        }
        if (ResourceManager.instance.inventory.ContainsSpecificItem("wood"))
        {
            woodText.text = "Wood: " + ResourceManager.instance.inventory.GetSpecificItem("wood").quantity.ToString();
        }
        if (ResourceManager.instance.inventory.ContainsSpecificItem("grain"))
        {
            grainText.text = "Grain: " + ResourceManager.instance.inventory.GetSpecificItem("grain").quantity.ToString();
        }
        if (ResourceManager.instance.inventory.ContainsSpecificItem("bread"))
        {
            breadText.text = "Bread: " + ResourceManager.instance.inventory.GetSpecificItem("bread").quantity.ToString();
        }
    }

    // Toggle to speed up time or set it back to default.
    public void SpeedTime()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 5;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}

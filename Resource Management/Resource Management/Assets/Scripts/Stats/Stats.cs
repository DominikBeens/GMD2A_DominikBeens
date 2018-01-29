using UnityEngine;

[System.Serializable]
public class Stats
{

    // Basic stats.
    public Stat health = new Stat();
    public Stat hunger = new Stat();
    [Space(10)]
    // The amount of time between every StatDepletion() update.
    public float statUpdateRate;

    // This function gets called on a timer in Entity.
    public void StatDepletion()
    {
        // Deplete hunger, if hunger is zero, deplete health.
        if (hunger.currentAmount > 0)
        {
            hunger.currentAmount -= hunger.drainRate;
        }
        else
        {
            health.currentAmount -= health.drainRate;
        }

        // If hunger is above a certain amount, gain health.
        if (hunger.currentAmount > 75)
        {
            if (health.currentAmount < health.maxAmount)
            {
                health.currentAmount += health.gainRate;
            }
        }
    }
}

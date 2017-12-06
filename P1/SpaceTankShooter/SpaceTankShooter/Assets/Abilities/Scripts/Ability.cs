using UnityEngine;

public class Ability : ScriptableObject
{

    public string abilityName;

    public enum AbilityState
    {
        Initializing,
        Using,
        Waiting
    }
    public AbilityState state;

    [Header("Cooldown")]
    public float timer;

    public virtual void Initialize()
    {

    }

    public virtual void Use()
    {

    }
}

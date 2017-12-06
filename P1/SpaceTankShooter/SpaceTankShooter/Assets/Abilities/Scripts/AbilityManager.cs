using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    public static AbilityManager instance;

    public AbilityHolder abilityShift;
    public AbilityHolder abilityE;

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
    }

    public IEnumerator Timer(Ability ability, float time)
    {
        ability.state = Ability.AbilityState.Waiting;

        yield return new WaitForSeconds(time);

        ability.state = Ability.AbilityState.Using;
    }

    public void StartACoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }
}

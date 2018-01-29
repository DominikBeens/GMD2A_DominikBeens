using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySelecter : MonoBehaviour
{

    public enum Type
    {
        Shift,
        E
    }
    public Type type;

    public GameObject abilityList;

    public TextMeshProUGUI selectAbilityText;
    public Animator selectAbilityAnim;

    private void Update()
    {
        if (type == Type.Shift)
        {
            if (AbilityManager.instance.abilityShift.ability != null)
            {
                selectAbilityText.text = AbilityManager.instance.abilityShift.ability.abilityName;
            }
        }
        else
        {
            if (AbilityManager.instance.abilityE.ability != null)
            {
                selectAbilityText.text = AbilityManager.instance.abilityE.ability.abilityName;
            }
        }
    }

    public void ToggleAbilityList()
    {
        abilityList.SetActive(!abilityList.activeInHierarchy);
    }

    public void SelectAbility(Ability ability)
    {
        if (type == Type.Shift)
        {
            AbilityManager.instance.abilityShift.ability = ability;
        }
        else
        {
            AbilityManager.instance.abilityE.ability = ability;
        }

        abilityList.SetActive(false);
        selectAbilityAnim.SetTrigger("Selected");
    }
}

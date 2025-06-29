using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fan : CommonPossessable, IPossessable
{
    public GameObject strengthButton;
    public GameObject wind;
    public int strength = 0;

    void Start()
    {
        strengthButton.GetComponent<Press>().onClick.AddListener(SetStrength);
    }

    void SetStrength()
    {
        if (!transform.Find("Player"))
        {
            return;
        }
        strength = strength == 0 ? 1 : 0;
        wind.SetActive(strength == 1 ? true : false);
    }

    public new void Unpossess()
    {
        possessables[strength].Possess();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : CommonPossessable, IPossessable
{
    public SpriteRenderer self;
    public GameObject wind;

    void Start()
    {
        self = GetComponent<SpriteRenderer>();
    }

    public new void Possess()
    {
        base.Possess();

        self.enabled = true;
        wind.SetActive(true);
    }
}

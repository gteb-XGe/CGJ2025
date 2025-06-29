using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Contract1 : CommonPossessable, IPossessable
{
    public Fan fan;
    public Transform destination;
    public GameObject another;
    public SpriteRenderer self;
    public List<Sprite> sprites;

    void Start()
    {
        self = GetComponent<SpriteRenderer>();
    }

    public new void Possess()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.SetParent(transform);
            player.transform.DOMove(transform.position, playerMove).SetEase(easeType).OnComplete(() =>
                { if (fan.strength == 1) Repair(); }
            );
        }
    }

    public void Repair()
    {
        transform.DOMove(destination.position, 1f).OnComplete(() => { self.sprite = sprites[1]; another.SetActive(false); });
        self.sprite = sprites[0];
        another.GetComponent<SpriteRenderer>().sprite = sprites[2];
    }
}

using UnityEngine;
using DG.Tweening;

public class LaserPen : CommonPossessable, IPossessable
{
    public GameObject laser;
    public GameObject screen;


    public new void Possess()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.SetParent(transform);
            player.transform.DOMove(transform.position, playerMove).SetEase(easeType).OnComplete(() => Act());
        }
    }

    private void Act()
    {
        laser.SetActive(true);
        screen.SetActive(true);
    }

}

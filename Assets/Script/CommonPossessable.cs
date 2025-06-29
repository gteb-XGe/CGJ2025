using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CommonPossessable : MonoBehaviour, IPossessable
{
    public int NextTarget;
    public List<GameObject> possessableObjects;
    protected List<IPossessable> possessables;
    public float playerMove = 1f;
    public Ease easeType = Ease.Linear;
    
    private void Awake()
    {
        possessables = new List<IPossessable>();
        foreach (var obj in possessableObjects)
        {
            var possessable = obj.GetComponent<IPossessable>();
            if (possessable != null)
            {
                possessables.Add(possessable);
            }
        }
    }

    public void ChangeState(int dir = 1)
    {
        if (possessables.Count == 0) return;

        NextTarget += dir;
        NextTarget = (NextTarget % possessables.Count + possessables.Count) % possessables.Count;
    }

    public void Possess()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.SetParent(transform);
            player.transform.DOMove(transform.position, playerMove).SetEase(easeType);
        }
    }

    public void Unpossess()
    {
        possessables[NextTarget].Possess();
    }
}

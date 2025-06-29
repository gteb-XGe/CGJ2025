using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class MoveWhenStart : CommonPossessable, IPossessable
{
    public Transform targetPosition;
    public float delayTime = 1f;
    public float moveDuration = 1f;
    //public float playerMove = 1f;
    //public Ease easeType = Ease.Linear;
    public float jumpHeight = 2f;
    public float rotationCount = 1f;

    public List<GameObject> nextTargetObject;
    private List<IPossessable> nextTarget;
    public int target = 0;

    public bool isLocking;

    void Awake()
    {
        nextTarget = new List<IPossessable>();
        if (nextTargetObject != null && nextTargetObject.Count > 0)
        {
            foreach (var obj in nextTargetObject)
            {
                if (obj != null)
                {
                    nextTarget.Add(obj.GetComponent<IPossessable>());
                }
            }
        }
    }

    void Start()
    {
        Possess();
        isLocking = true;
        Invoke("MoveToTarget", delayTime);
    }

    void MoveToTarget()
    {
        Vector3 endPos = targetPosition.position;
        Vector3 midPos = (transform.position + endPos) * 0.5f;
        midPos.y += jumpHeight;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOPath(new Vector3[] { transform.position, midPos, endPos }, moveDuration, PathType.CatmullRom)
            .SetEase(easeType));
        seq.Join(transform.DORotate(new Vector3(0, 0, 360 * rotationCount), moveDuration, RotateMode.FastBeyond360)
            .OnComplete(() => ChangeState(1)));
    }
    public new void Possess()
    {
        if (isLocking)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.SetParent(transform);
            player.transform.DOMove(transform.position, playerMove).SetEase(easeType);
        }
    }

    public new void Unpossess()
    {
        nextTarget[target].Possess();
    }

    public new void ChangeState(int dir)
    {
        target++;
        isLocking = false;
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBeGhost : MonoBehaviour
{
    public GameObject c1;
    public GameObject c2;
    public GameObject player;

    private void Awake()
    {
        ChangerC1();
    }

    public void ChangerC1()
    {
        var image = c1.GetComponent<SpriteRenderer>();
        var a = DOTween.Sequence();
        a.Append(image.DOFade(1, 1f));
        a.Append(image.DOFade(0, 1f));
        a.OnComplete(() => ChangeC2());
    }

    public void ChangeC2()
    {
        var image = c2.GetComponent<SpriteRenderer>();
        var a = DOTween.Sequence();
        a.Append(image.DOFade(1, 1f));
        a.Append(image.DOFade(0, 1f));
        a.OnComplete(()=>ChangeP());
    }

    public void ChangeP()
    {
        var image=player.GetComponent<SpriteRenderer>();
        var a = DOTween.Sequence();
        a.Append(image.DOFade(1, 1f));
        a.OnComplete(() => TextUIManager.Instance.SetTextFade("左键：附着并拖动  \r\n空格：松手让灵魂飞  \r\n右键：在钟表上回溯", 3f));
        CRT_trigger.__isOver = true;
    }

}

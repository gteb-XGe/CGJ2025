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
        a.OnComplete(() => TextUIManager.Instance.SetTextFade("��������Ų��϶�  \r\n�ո�����������  \r\n�Ҽ������ӱ��ϻ���", 3f));
        CRT_trigger.__isOver = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Manager;
using DG.Tweening;
using System;

public class TextUIManager : _Manager<TextUIManager>
{
    public Text text;
    public Image image;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void SetPanel()
    {
        image.gameObject.SetActive(true);
    }

    public void OverPanel()
    {
        image.gameObject.SetActive(false);
    }
    public void SetText(string value,float time)
    {
        text.gameObject.SetActive(true);
        text.DOText(value, time);
    }

    public void SetTextFade(string value,float time)
    {
        text.gameObject.SetActive(true);
        text.text = value;
        var color = text.color;
        color.a = 0f;
        text.color = color;
        text.DOFade(1, time);
    }

    public void OverText()
    {
        text.text = "";
        text.gameObject.SetActive(false);
    }

}

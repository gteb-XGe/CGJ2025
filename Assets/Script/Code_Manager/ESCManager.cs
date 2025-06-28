using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.UI;
using DG.Tweening;

public class ESCManager : _Manager<ESCManager>
{
    public Image image;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1) { Time.timeScale = 0; image.DOFade(0.608f, 0.1f).SetUpdate(true); }
            else if (Time.timeScale == 0) { Time.timeScale = 1;image.DOFade(0,0.1f); }
        }
    }
}

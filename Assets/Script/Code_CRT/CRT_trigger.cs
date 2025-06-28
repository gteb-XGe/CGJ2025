using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public interface IAttachable
{
    void OnAttach();
    void OnDrag();
    void OnDetach();
}

public class CRT_trigger : MonoBehaviour,IAttachable
{
    public Text CRT_Text; //CRT初始交互提示文本
    [Header("教程UI")]
    public Text _教程引导;//透明度初始为0
    [SerializeField]private float FadeTime=1.0f;

    [Header("交互参数")]
    [SerializeField] private float triggerTime = 3.0f; // 触发时间阈值
    private float timer;
    bool isMouse = false;
    [SerializeField] int FlashNum = 3; //闪烁次数
    [SerializeField] private GameObject LightningEffectPrefab; //闪电特效Prefab

    [Header("场景切换参数")]
    // 光圈收缩动画时长，可根据需求调整
    [SerializeField] float fadeOutDuration =3f;
    // 场景切换延迟，可用于配合动画
    [SerializeField] float switchDelay = 0.5f;

    public void OnAttach()
    {
        throw new System.NotImplementedException();
    }

    public void OnDetach()
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        timer = triggerTime;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!isMouse)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                TriggerLight();
            }
        }

    }

    private void TriggerLight()//未点后的闪电
    {
        isMouse = true;
        StartCoroutine(CRTFlashEffect());
        _教程引导Text();
        // 实例化闪电特效
        if (LightningEffectPrefab != null)
        {
            Instantiate(LightningEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(LightningEffectPrefab,0.3f);
    }

    private void OnMouseDown()//鼠标点击事件
    {
        isMouse = true;
        StartCoroutine(CRTFlashEffect());
        _教程引导Text();
    }

    IEnumerator CRTFlashEffect()// 触发CRT闪烁效果,进入游戏时触发
    {
        // CRT闪烁效果
        Material mat = GetComponent<MeshRenderer>().material;
        Color originalColor = mat.color;

        for (int i = 0; i <FlashNum; i++)
        {
            mat.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            mat.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void _教程引导Text()
    {
        var r = DOTween.Sequence();
        r.Append(_教程引导.DOFade(1, FadeTime));
        r.Append(_教程引导.DOFade(0, FadeTime * 2));//淡入淡出效果
        r.OnComplete(() => Destroy(_教程引导.gameObject));
    }

    public void S2()
    {
        SceneManager.LoadScene("S2");//加载S2场景
    }

    public void SwitchToNewScene()
    {
        // 禁用玩家控制
        //隐藏Player?


        //屏幕变黑后切换
        Camera.main.DOColor(Color.black, fadeOutDuration).OnComplete(() =>
        {
            // 延迟一段时间后切换场景
            Invoke("LoadTargetScene", switchDelay);
        });

        Camera newCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        S2();
    }

}

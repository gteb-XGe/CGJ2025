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

public class CRT_trigger : MonoBehaviour, IAttachable
{
    public Text CRT_Text; //CRT初始交互提示文本
    public static bool __isOver;
    public GameObject player;

    [Header("交互参数")]
    [SerializeField] private float triggerTime = 3.0f; // 触发时间阈值
    private float timer;
    private bool isMouse = false;
    [SerializeField] int FlashNum = 3; //闪烁次数
    [SerializeField] private GameObject LightningEffectPrefab; //闪电特效Prefab

    [Header("场景切换参数")]
    [SerializeField] float switchDelay = 0.5f;
    [SerializeField] private float fadeInDuration = 2f;
    public Image img;

    [Header("镜头抖动参数")]
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeStrength = 0.5f;
    [SerializeField] private int shakeVibrato = 10;
    [SerializeField] private float shakeRandomness = 90f;

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
        if(__isOver)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                var a=player.transform.GetComponent<Rigidbody2D>();
                a.DOMove(new Vector2(0,-0.5f),2f);
                SwitchToNewScene("First Scene");
            }
        }
    }

    /*private void TriggerLight()//未点后的闪电
    {
        isMouse = true;
        StartCoroutine(CRTFlashEffect());
        _教程引导Text();
        CameraShake();
        if (LightningEffectPrefab != null)
        {
            Instantiate(LightningEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(LightningEffectPrefab, 0.3f);
    }*/

    private void OnMouseDown()//鼠标点击事件
    {
        //TextUIManager.Instance.SetTextFade("左键：附着并拖动  \r\n空格：松手让灵魂飞  \r\n右键：在钟表上回溯",3f);
        //SwitchToNewScene("First Scene");
        isMouse = true;
        //StartCoroutine(CRTFlashEffect());
        //_教程引导Text();
    }

    /*IEnumerator CRTFlashEffect()// 触发CRT闪烁效果,进入游戏时触发
    {
        Material mat = GetComponent<MeshRenderer>().material;
        Color originalColor = mat.color;

        for (int i = 0; i < FlashNum; i++)
        {
            mat.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            mat.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }*/



    public void SwitchToNewScene(string name)
    {
        var r = DOTween.Sequence();
        CameraShake();
        r.Append(img.DOFade(1, fadeInDuration));
        r.AppendInterval(switchDelay);
        r.OnComplete(() =>SceneManager.LoadScene(name));
        Camera newCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void CameraShake()
    {
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            mainCam.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
        }
    }
}

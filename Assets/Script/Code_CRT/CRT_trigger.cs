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
    public Text CRT_Text; //CRT��ʼ������ʾ�ı�
    public static bool __isOver;
    public GameObject player;

    [Header("��������")]
    [SerializeField] private float triggerTime = 3.0f; // ����ʱ����ֵ
    private float timer;
    private bool isMouse = false;
    [SerializeField] int FlashNum = 3; //��˸����
    [SerializeField] private GameObject LightningEffectPrefab; //������ЧPrefab

    [Header("�����л�����")]
    [SerializeField] float switchDelay = 0.5f;
    [SerializeField] private float fadeInDuration = 2f;
    public Image img;

    [Header("��ͷ��������")]
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

    /*private void TriggerLight()//δ��������
    {
        isMouse = true;
        StartCoroutine(CRTFlashEffect());
        _�̳�����Text();
        CameraShake();
        if (LightningEffectPrefab != null)
        {
            Instantiate(LightningEffectPrefab, transform.position, Quaternion.identity);
        }
        Destroy(LightningEffectPrefab, 0.3f);
    }*/

    private void OnMouseDown()//������¼�
    {
        //TextUIManager.Instance.SetTextFade("��������Ų��϶�  \r\n�ո�����������  \r\n�Ҽ������ӱ��ϻ���",3f);
        //SwitchToNewScene("First Scene");
        isMouse = true;
        //StartCoroutine(CRTFlashEffect());
        //_�̳�����Text();
    }

    /*IEnumerator CRTFlashEffect()// ����CRT��˸Ч��,������Ϸʱ����
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

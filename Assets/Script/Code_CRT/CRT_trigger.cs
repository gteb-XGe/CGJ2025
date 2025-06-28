using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface IAttachable
{
    void OnAttach();
    void OnDrag();
    void OnDetach();
}

public class CRT_trigger : MonoBehaviour,IAttachable
{
    public Text CRT_Text; //CRT��ʼ������ʾ�ı�
    [Header("�̳�UI")]
    public Text _�̳�����;//͸���ȳ�ʼΪ0
    [SerializeField]private float FadeTime=1.0f;

    [Header("��������")]
    private float timer;
    [SerializeField] private float triggerTime = 3.0f; // ����ʱ����ֵ
    bool isMouse = false;
    [SerializeField] int FlashNum = 3; //��˸����
    [SerializeField] private GameObject LightningEffectPrefab; //������ЧPrefab

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

    private void TriggerLight()//δ��������
    {
        isMouse = true;
        StartCoroutine(CRTFlashEffect());
        _�̳�����Text();
        // ʵ����������Ч
        if (LightningEffectPrefab != null)
        {
            Instantiate(LightningEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnMouseDown()//������¼�
    {
        isMouse = true;
        StartCoroutine(CRTFlashEffect());
        _�̳�����Text();
    }

    IEnumerator CRTFlashEffect()// ����CRT��˸Ч��,������Ϸʱ����
    {
        // CRT��˸Ч��
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

    public void _�̳�����Text()
    {
        var r = DOTween.Sequence();
        r.Append(_�̳�����.DOFade(1, FadeTime));
        r.Append(_�̳�����.DOFade(0, FadeTime * 2));//���뵭��Ч��
        r.OnComplete(() => Destroy(_�̳�����.gameObject));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEventRight : MonoBehaviour, BaseEvent
{
    private bool fnished = false;
    public GameObject targetObject;  //����
    public float maxForce = 5f;     // 
    public float maxDistance = 5f;  // 
    private bool isFanActive = false; //

    [SerializeField] float duration = 2f;  // �ӳ�ʱ�䣨ģ�ⴰ����/�ع��̣�
    [SerializeField] Transform holder;     // �Կɱ���������
    [SerializeField] GameObject Open, Close;

    [SerializeField] bool IsOpening = true;
    private bool isWaiting = false;
    private const string RainInside = "Audio/Level1/Level_01_Amb_Rain_Inside";
    private const string RainOutside = "Audio/Level1/Level_01_Amb_Rain_Outside";
    private const string WindowOpenSound = "Audio/Level1/Level_01_WindowOpen";
    private void Start()
    {
       
            // ����������������������ѭ����
            MusicManager.Instance.PlayAudio("RainInside", RainInside, volume: 0.3f, loop: true);
       
    }
    public void StartEvent(GameObject player)
    {
        targetObject = player;
    
        if (!fnished)
        {
            StageLoop.instance.finish();
            fnished = true;
        }
      
        if (isWaiting) return;
        StartCoroutine(HandleWindowEvent());
        isFanActive = !isFanActive;

    }
    public void EndEvent(GameObject player)
    {

    }
    private void FixedUpdate()
    {



        if (isFanActive && targetObject != null)
        {
            ApplyWindForce();
        }
    }
    void ApplyWindForce()
    {
        Rigidbody2D rb = targetObject.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        float distance = Vector3.Distance(targetObject.transform.position, transform.position);
        // ���ݾ������Բ�ֵ������ԽԶ����ԽС
        float forceMag = Mathf.Lerp(maxForce, 0, distance / maxDistance);

        // ��Ϊ���ҷ������
        rb.AddForce(new Vector2(forceMag, 0));
    }
    private IEnumerator HandleWindowEvent()
    {
        isWaiting = true;

        // ���Ŵ�����������
        MusicManager.Instance.PlaySFX(WindowOpenSound);

        // �ȴ� duration �루����ԭ������ת��
        yield return new WaitForSeconds(duration);

        if (IsOpening)
        {
            // �رմ������ر����������ָ�������
            MusicManager.Instance.StopAudio("RainOutside");
            MusicManager.Instance.SetAudioVolume("RainInside", 0.3f);

            // ��ʾ�ر�״̬�����ش�״̬
            Open.SetActive(false);
            Close.SetActive(true);
        }
        else
        {
            // �򿪴���������������������������
            MusicManager.Instance.PlayAudio("RainOutside", RainOutside, volume: 0.6f, loop: true);
            MusicManager.Instance.SetAudioVolume("RainInside", 0.1f);

            // ��ʾ��״̬�����عر�״̬
            Open.SetActive(true);
            Close.SetActive(false);
        }

        // ״̬�л�
        IsOpening = !IsOpening;
        isWaiting = false;
    }
}

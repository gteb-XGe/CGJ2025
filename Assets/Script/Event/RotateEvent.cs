using UnityEngine;
using System.Collections;

public class RotateEvent : MonoBehaviour, BaseEvent
{
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
        if (isWaiting) return;
        StartCoroutine(HandleWindowEvent());
    }

    public void EndEvent(GameObject player)
    {
        // ����չ
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
            Open.SetActive(true );
            Close.SetActive(false);
        }

        // ״̬�л�
        IsOpening = !IsOpening;
        isWaiting = false;
    }
}

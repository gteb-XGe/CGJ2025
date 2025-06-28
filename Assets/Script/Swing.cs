using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{//�Ӱ�Ч��
    public float maxAngle = 30f;  // ���Ƕ�,����SwingEvent������ã������벻Ҫ���ھ�ʮ��
    public float speed = 1f;      // �ٶ�
    private bool hasPlayedAtZero = false;  // ����Ƿ��Ѿ��ڱ����ڲ��Ź�

    private float angle;
    private string[] clockSounds = new string[]
   {
        "Audio/Level1/Level_01_Clock_01",
        "Audio/Level1/Level_01_Clock_02",
        "Audio/Level1/Level_01_Clock_03",
        "Audio/Level1/Level_01_Clock_04"
   };
    void Update()
    {
        // ��-maxAngle �� maxAngle ֮��仯
        angle = maxAngle * Mathf.Sin(Time.time * speed);
        // ��ת�Ƕ�
        transform.localRotation = Quaternion.Euler(0, 0, angle);
        // ���Z��ǶȽӽ�0���һ�û������Ч���򲥷�
        if (Mathf.Abs(angle) < 0.5f) // ��ֵԽСԽ�ӽ���ʵ�е�
        {
            if (!hasPlayedAtZero)
            {
                PlayRandomClockSound();
                hasPlayedAtZero = true;
            }
        }
        else
        {
            // �뿪�е��������ô�����־
            hasPlayedAtZero = false;
        }
    }
    void PlayRandomClockSound()
    {
        int index = Random.Range(0, clockSounds.Length);
        string path = clockSounds[index];
        MusicManager.Instance.PlaySFX(path);
    }
}

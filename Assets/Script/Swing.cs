using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{//�Ӱ�Ч��
    public float maxAngle = 30f;  // ���Ƕ�,����SwingEvent������ã������벻Ҫ���ھ�ʮ��
    public float speed = 1f;      // �ٶ�

    private float angle;

    void Update()
    {
        // ��-maxAngle �� maxAngle ֮��仯
        angle = maxAngle * Mathf.Sin(Time.time * speed);
        // ��ת�Ƕ�
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}

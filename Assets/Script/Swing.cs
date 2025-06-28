using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{//钟摆效果
    public float maxAngle = 30f;  // 最大角度,由于SwingEvent相关设置，这里请不要大于九十度
    public float speed = 1f;      // 速度

    private float angle;

    void Update()
    {
        // 在-maxAngle 到 maxAngle 之间变化
        angle = maxAngle * Mathf.Sin(Time.time * speed);
        // 旋转角度
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}

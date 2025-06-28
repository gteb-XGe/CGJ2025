using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{//钟摆效果
    public float maxAngle = 30f;  // 最大角度,由于SwingEvent相关设置，这里请不要大于九十度
    public float speed = 1f;      // 速度
    private bool hasPlayedAtZero = false;  // 标记是否已经在本周期播放过

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
        // 在-maxAngle 到 maxAngle 之间变化
        angle = maxAngle * Mathf.Sin(Time.time * speed);
        // 旋转角度
        transform.localRotation = Quaternion.Euler(0, 0, angle);
        // 如果Z轴角度接近0并且还没播放音效，则播放
        if (Mathf.Abs(angle) < 0.5f) // 阈值越小越接近真实中点
        {
            if (!hasPlayedAtZero)
            {
                PlayRandomClockSound();
                hasPlayedAtZero = true;
            }
        }
        else
        {
            // 离开中点区域，重置触发标志
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

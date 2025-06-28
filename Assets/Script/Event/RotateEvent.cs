using System.Collections;
using UnityEngine;

public class RotateEvent : MonoBehaviour, BaseEvent
{
    [SerializeField] Quaternion start, end;
    [SerializeField] float duration = 2f;  // 旋转时间
    [SerializeField] Transform holder;
    private bool isClosing = true;
    private bool isRotating = false;
    private const string RainInside = "Audio/Level1/Level_01_Amb_Rain_Inside";
    private const string RainOutside = "Audio/Level1/Level_01_Amb_Rain_Outside";
    private const string WindowOpenSound = "Audio/Level1/Level_01_WindowOpen";
    private void Start()
    {
        holder.rotation = start;

        // 播放室内雨声（低音量，循环）
        MusicManager.Instance.PlayAudio("RainInside", RainInside, volume: 0.3f, loop: true);
    }

    public void StartEvent(GameObject player)
    {
        if (isRotating) return;
        MusicManager.Instance.PlaySFX(WindowOpenSound);
        // 切换窗户状态
        if (isClosing)
        {
            // 窗户关闭：关闭外雨声，恢复室内雨声
            MusicManager.Instance.StopAudio("RainOutside");
            MusicManager.Instance.SetAudioVolume("RainInside", 0.3f);
        }
        else
        {
            // 窗户打开：播放外雨声，减弱室内雨声
            MusicManager.Instance.PlayAudio("RainOutside", RainOutside, volume: 0.6f, loop: true);
            MusicManager.Instance.SetAudioVolume("RainInside", 0.1f);
        }

        // 开始旋转动画
        StartCoroutine(RotateOverTime(holder, isClosing ? start : end, isClosing ? end : start, duration));

        isClosing = !isClosing;
    }

    public void EndEvent(GameObject player)
    {
        // 可扩展
    }

    private IEnumerator RotateOverTime(Transform targetTransform, Quaternion fromRot, Quaternion toRot, float time)
    {
        isRotating = true;

        float elapsed = 0f;
        while (elapsed < time)
        {
            float t = elapsed / time;
            targetTransform.rotation = Quaternion.Slerp(fromRot, toRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        targetTransform.rotation = toRot;

        isRotating = false;
    }
}

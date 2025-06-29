using UnityEngine;
using System.Collections;

public class RotateEvent : MonoBehaviour, BaseEvent
{
    [SerializeField] float duration = 2f;  // 延迟时间（模拟窗户开/关过程）
    [SerializeField] Transform holder;     // 仍可保留，备用
    [SerializeField] GameObject Open, Close;

    [SerializeField] bool IsOpening = true;
    private bool isWaiting = false;

    private const string RainInside = "Audio/Level1/Level_01_Amb_Rain_Inside";
    private const string RainOutside = "Audio/Level1/Level_01_Amb_Rain_Outside";
    private const string WindowOpenSound = "Audio/Level1/Level_01_WindowOpen";

    private void Start()
    {
        // 播放室内雨声（低音量，循环）
        MusicManager.Instance.PlayAudio("RainInside", RainInside, volume: 0.3f, loop: true);
    }

    public void StartEvent(GameObject player)
    {
        if (isWaiting) return;
        StartCoroutine(HandleWindowEvent());
    }

    public void EndEvent(GameObject player)
    {
        // 可扩展
    }

    private IEnumerator HandleWindowEvent()
    {
        isWaiting = true;

        // 播放窗户开启声音
        MusicManager.Instance.PlaySFX(WindowOpenSound);

        // 等待 duration 秒（代替原来的旋转）
        yield return new WaitForSeconds(duration);

        if (IsOpening)
        {
            // 关闭窗户：关闭外雨声，恢复内雨声
            MusicManager.Instance.StopAudio("RainOutside");
            MusicManager.Instance.SetAudioVolume("RainInside", 0.3f);

            // 显示关闭状态，隐藏打开状态
            Open.SetActive(false);
            Close.SetActive(true);
        }
        else
        {
            // 打开窗户：播放外雨声，降低内雨声
            MusicManager.Instance.PlayAudio("RainOutside", RainOutside, volume: 0.6f, loop: true);
            MusicManager.Instance.SetAudioVolume("RainInside", 0.1f);

            // 显示打开状态，隐藏关闭状态
            Open.SetActive(true );
            Close.SetActive(false);
        }

        // 状态切换
        IsOpening = !IsOpening;
        isWaiting = false;
    }
}

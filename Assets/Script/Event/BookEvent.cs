using UnityEngine;
using System.Collections;

public class BookEvent : MonoBehaviour, BaseEvent
{
    [SerializeField] private bool Right = true;       // true 向右倒
    [SerializeField] private float fallHeight = 1f;   // Y轴向下位移
    [SerializeField] private float horizontalOffset = 0.5f; // X轴位移
    [SerializeField] private float duration = 1f;     // 动画时长

    private bool hasFallen = false;

    private readonly string[] bookSounds = {
        "Audio/Level1/Level_01_Book_01",
        "Audio/Level1/Level_01_Book_02"
    };

    public void StartEvent(GameObject player)
    {
        if (hasFallen) return;
        hasFallen = true;

        // 播放随机书本掉落音效
        string soundToPlay = bookSounds[Random.Range(0, bookSounds.Length)];
        MusicManager.Instance.PlaySFX(soundToPlay);

        // 计算目标位置（同时下落和水平偏移）
        Vector3 startPos = transform.position;
        float xOffset = Right ? horizontalOffset : -horizontalOffset;
        Vector3 endPos = startPos + new Vector3(xOffset, -fallHeight, 0f);

        // 计算目标旋转
        Quaternion startRot = transform.rotation;
        float angle = Right ? -90f : 90f;
        Quaternion endRot = startRot * Quaternion.AngleAxis(angle, Vector3.forward);

        // 启动动画协程
        StartCoroutine(FallAndRotate(startPos, endPos, startRot, endRot, duration));
    }

    public void EndEvent(GameObject player) { }

    private IEnumerator FallAndRotate(Vector3 fromPos, Vector3 toPos, Quaternion fromRot, Quaternion toRot, float time)
    {
        float elapsed = 0f;
        while (elapsed < time)
        {
            float t = elapsed / time;
            transform.position = Vector3.Lerp(fromPos, toPos, t);
            transform.rotation = Quaternion.Slerp(fromRot, toRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = toPos;
        transform.rotation = toRot;
    }

}

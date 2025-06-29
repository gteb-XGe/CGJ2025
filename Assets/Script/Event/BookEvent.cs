using UnityEngine;
using System.Collections;

public class BookEvent : MonoBehaviour, BaseEvent
{
    [SerializeField] private bool Right = true;       // true ���ҵ�
    [SerializeField] private float fallHeight = 1f;   // Y������λ��
    [SerializeField] private float horizontalOffset = 0.5f; // X��λ��
    [SerializeField] private float duration = 1f;     // ����ʱ��

    private bool hasFallen = false;

    private readonly string[] bookSounds = {
        "Audio/Level1/Level_01_Book_01",
        "Audio/Level1/Level_01_Book_02"
    };

    public void StartEvent(GameObject player)
    {
        if (hasFallen) return;
        hasFallen = true;

        // ��������鱾������Ч
        string soundToPlay = bookSounds[Random.Range(0, bookSounds.Length)];
        MusicManager.Instance.PlaySFX(soundToPlay);

        // ����Ŀ��λ�ã�ͬʱ�����ˮƽƫ�ƣ�
        Vector3 startPos = transform.position;
        float xOffset = Right ? horizontalOffset : -horizontalOffset;
        Vector3 endPos = startPos + new Vector3(xOffset, -fallHeight, 0f);

        // ����Ŀ����ת
        Quaternion startRot = transform.rotation;
        float angle = Right ? -90f : 90f;
        Quaternion endRot = startRot * Quaternion.AngleAxis(angle, Vector3.forward);

        // ��������Э��
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

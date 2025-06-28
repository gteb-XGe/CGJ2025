using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateEvent : MonoBehaviour,BaseEvent
{
    [SerializeField]  Quaternion start, end;
    [SerializeField] float duration = 2f;  // ��תʱ�䣬���޸�
    [SerializeField] Transform holder;
    private void Start()
    {
        holder.rotation=start;
    }
    public void EndEvent(GameObject player)
    {
      
    }

    public void StartEvent(GameObject player)
    {
        StartCoroutine(RotateOverTime(holder, start, end, duration));
    }

    private IEnumerator RotateOverTime(Transform targetTransform, Quaternion fromRot, Quaternion toRot, float time)
    {
        float elapsed = 0f;

        while (elapsed < time)
        {
            float t = elapsed / time;
            targetTransform.rotation = Quaternion.Slerp(fromRot, toRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // ȷ����ת��Ŀ��Ƕ�
        targetTransform.rotation = toRot;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent : MonoBehaviour,BaseEvent
{
    [SerializeField]Transform target, current;//目标位置，初始位置
    [SerializeField] float duration=1f;
    public void EndEvent(GameObject player)
    {
        throw new System.NotImplementedException();
    }


    public void StartEvent(GameObject player)
    {
        StartCoroutine(MoveToTarget(player));
    }

    private IEnumerator MoveToTarget(GameObject player)
    {
  
        float elapsed = 0f;

        Vector3 startPosition = current.position;
        Vector3 endPosition = target.position;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
         
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

       
      transform.position = endPosition;

        // 交换current和target
        Transform temp = current;
        current = target;
        target = temp;
    }
    // Start is called before the first frame update

}

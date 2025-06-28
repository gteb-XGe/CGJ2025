using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventt : MonoBehaviour, BaseEvent
{
    public GameObject targetObject;  // 被吹风的对象
    public float maxForce = 5f;     // 最近距离时的最大风力
    public float maxDistance = 5f;  // 风扇的最大作用距离
    private bool isFanActive = false; //开关状态
    public void StartEvent(GameObject player)
    {
        targetObject = player;
        isFanActive = !isFanActive;
       // Debug.Log("111");
       if(isFanActive) MusicManager.Instance.PlaySFX("Audio/Level1/Level_01_Amb_AirConditioner");


    }
    public void EndEvent(GameObject player)
    {

    }
    private void FixedUpdate()
    {



        if (isFanActive && targetObject != null)
        {
            ApplyWindForce();
        }
    }
    void ApplyWindForce()
    {
        Rigidbody2D rb = targetObject.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        float distance = Vector3.Distance(targetObject.transform.position, transform.position);
        // 距离越远，力越小
        float forceMag = Mathf.Lerp(maxForce, 0, distance / maxDistance);

        // 施加向上的力
        rb.AddForce(new Vector2(forceMag, 0));
    }

}

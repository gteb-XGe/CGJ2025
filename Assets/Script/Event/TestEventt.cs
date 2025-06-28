using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventt : MonoBehaviour, BaseEvent
{
    public GameObject targetObject;  // ������Ķ���
    public float maxForce = 5f;     // �������ʱ��������
    public float maxDistance = 5f;  // ���ȵ�������þ���
    private bool isFanActive = false; //����״̬
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
        // ����ԽԶ����ԽС
        float forceMag = Mathf.Lerp(maxForce, 0, distance / maxDistance);

        // ʩ�����ϵ���
        rb.AddForce(new Vector2(forceMag, 0));
    }

}

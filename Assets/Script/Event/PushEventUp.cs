using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PushEventUp :  MonoBehaviour ,BaseEvent
    //�ƶ�Ч�����յ�������
{  private bool fnished=false;
    public GameObject targetObject;  // ������Ķ���
    public float maxForce = 5f;     // �������ʱ��������
    public float maxDistance = 5f;  // ���ȵ�������þ���
    private bool isFanActive = false; // ���ȿ���״̬
    private const string FanSoundPath = "Audio/Level1/Level_01_SmallFan";
    public void StartEvent(GameObject player)
    {
        targetObject = player;
        isFanActive = !isFanActive;
        if (!fnished)
        {
            StageLoop.instance.finish();
            fnished = true;
        }
        if (isFanActive)
        {
        
            MusicManager.Instance.PlaySFX(FanSoundPath,0.2f);
        }
        // Debug.Log("111");

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
        rb.AddForce(new Vector2(0, forceMag));
    }

  
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float gettingTime = 1f;
    [SerializeField] float moveSpeed = 3f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    [SerializeField]float elapsedTime = 0.3f;
  //  private bool isMoving = true;
    private Coroutine moveCoroutine;
    void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))//����������
        {
            GetIn();
        }
        if (Input.GetKeyDown(KeyCode.Space))//������Ʒ�¼�
        {
            if (target != null)
            {
                target.GetComponent<BaseEvent>().StartEvent();
            }
        }
        // �ƶ��ű����ƶ���ʽ�ݶ��������������������ƶ���
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");    
        Vector3 moveDirection = new Vector3(horizontal, vertical, 0f);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

    }


    void GetIn()    //����
    {
        Debug.Log("111");
        if (target != null)
        {
            startPosition = transform.position;
            targetPosition = target.transform.position;

           
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            // �����ƶ�Э��
            moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
        }
    }
    private IEnumerator MoveToTargetCoroutine()
    {
        elapsedTime = 0.3f;//�����ƶ����ٶȣ�Խ�ӽ�1Խ��
        while (elapsedTime < gettingTime)
        {
            
            float t = elapsedTime / gettingTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // ȷ������Ŀ��λ��
        transform.position = targetPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)//��������
    {
        target=collision.gameObject;
   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
          
            target = null;
        }
    }
}

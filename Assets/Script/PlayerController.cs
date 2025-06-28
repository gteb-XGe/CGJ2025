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
        if (Input.GetKeyDown(KeyCode.F))//触发附身方法
        {
            GetIn();
        }
        if (Input.GetKeyDown(KeyCode.Space))//触发物品事件
        {
            if (target != null)
            {
                target.GetComponent<BaseEvent>().StartEvent();
            }
        }
        // 移动脚本（移动方式暂定，这里先用上下左右移动）
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");    
        Vector3 moveDirection = new Vector3(horizontal, vertical, 0f);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

    }


    void GetIn()    //附身
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
            // 启动移动协程
            moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
        }
    }
    private IEnumerator MoveToTargetCoroutine()
    {
        elapsedTime = 0.3f;//调整移动的速度，越接近1越快
        while (elapsedTime < gettingTime)
        {
            
            float t = elapsedTime / gettingTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 确保到达目标位置
        transform.position = targetPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)//附身物体
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

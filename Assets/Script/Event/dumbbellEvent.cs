using UnityEngine;
using System.Collections;

public class DumbbellMove : MonoBehaviour, BaseEvent
{
    [SerializeField] private Transform target;  // 目标位置
    [SerializeField] private Transform current; // 初始位置
    [SerializeField] private float duration = 1f;
    [SerializeField]Animator animator;
    private bool fnished=false;
    private const string MoveSound = "Audio/Level1/Level_01_Dumbbell_Rolling";
    private const string StopSound = "Audio/Level1/Level_01_Dumbbell_Stop";

    private bool isMoving = false;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void StartEvent(GameObject player)
    {
        if (!fnished)
        {
            StageLoop.instance.finish();
            fnished = true;
        }
        if (isMoving) return;

        isMoving = true;

        // 播放滚动音效
        MusicManager.Instance.PlaySFX(MoveSound);
        animator.SetBool("IsRolling", true);
        // 开始移动
        StartCoroutine(MoveToTarget());
    }

    public void EndEvent(GameObject player)
    {
        // 暂无实现
    }

    private IEnumerator MoveToTarget()
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

        // 播放停止音效
        MusicManager.Instance.PlaySFX(StopSound);

        // 交换 current 和 target（可选：实现往返移动）
        Transform temp = current;
        current = target;
        target = temp;

        isMoving = false;
    }
}

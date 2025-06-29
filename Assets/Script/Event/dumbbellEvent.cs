using UnityEngine;
using System.Collections;

public class DumbbellMove : MonoBehaviour, BaseEvent
{
    [SerializeField] private Transform target;  // Ŀ��λ��
    [SerializeField] private Transform current; // ��ʼλ��
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

        // ���Ź�����Ч
        MusicManager.Instance.PlaySFX(MoveSound);
        animator.SetBool("IsRolling", true);
        // ��ʼ�ƶ�
        StartCoroutine(MoveToTarget());
    }

    public void EndEvent(GameObject player)
    {
        // ����ʵ��
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

        // ����ֹͣ��Ч
        MusicManager.Instance.PlaySFX(StopSound);

        // ���� current �� target����ѡ��ʵ�������ƶ���
        Transform temp = current;
        current = target;
        target = temp;

        isMoving = false;
    }
}

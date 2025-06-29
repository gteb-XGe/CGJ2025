using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageLoop : MonoBehaviour
{
    public static StageLoop instance;

    [SerializeField] float maxTasks = 6;
    [SerializeField] int finishedTasks = 0;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI textMeshPro;

    private float currrentValue = 0f;              // Ŀ��ֵ
    private bool isWaitingToPlayBeep = false;      // �ȴ���������������

    private const string BeepSound = "Audio/Level1/Level_01_ControlerBeep";

    void Start()
    {
       
        instance = this;
        slider.value = 0;
        currrentValue = 0;
        textMeshPro.text = $"Task {finishedTasks}/{(int)maxTasks}";
        MusicManager.Instance.PlayMainMusic("Audio/BGM/loop", volume: 0.6f, loop: true);
    }

    void Update()
    {
        // Debug ģ���������
        if (Input.GetKeyDown(KeyCode.G))
        {
            finish();
        }

        // ƽ������������
        if (slider.value < currrentValue)
        {
            slider.value += Time.deltaTime * 0.3f;
            if (slider.value > currrentValue)
            {
                slider.value = currrentValue;
            }
        }

        // �������������󣬸����ı���������Ч
        if (isWaitingToPlayBeep && Mathf.Approximately(slider.value, currrentValue))
        {
            MusicManager.Instance.PlaySFX(BeepSound);
            textMeshPro.text = $"Task {finishedTasks}/{(int)maxTasks}";
            isWaitingToPlayBeep = false;
        }
    }

    public void finish()
    {
        if (finishedTasks >= maxTasks) return;

        finishedTasks++;
        currrentValue = (float)finishedTasks / maxTasks;
        isWaitingToPlayBeep = true;

        if (finishedTasks == maxTasks)
        {
            // �ȴ�����������������ɺ����л�
            StartCoroutine(WaitAndLoadScene());
        }
    }
    private IEnumerator WaitAndLoadScene()
    {
        // �ȴ���Ч������ UI �������ɵ�����
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Scene2");
    }

}

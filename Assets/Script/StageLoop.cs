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

    private float currrentValue = 0f;              // 目标值
    private bool isWaitingToPlayBeep = false;      // 等待进度条涨满后处理

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
        // Debug 模拟完成任务
        if (Input.GetKeyDown(KeyCode.G))
        {
            finish();
        }

        // 平滑增长进度条
        if (slider.value < currrentValue)
        {
            slider.value += Time.deltaTime * 0.3f;
            if (slider.value > currrentValue)
            {
                slider.value = currrentValue;
            }
        }

        // 当进度条涨满后，更新文本并播放音效
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
            // 等待进度条动画播放完成后再切换
            StartCoroutine(WaitAndLoadScene());
        }
    }
    private IEnumerator WaitAndLoadScene()
    {
        // 等待音效播放与 UI 动画（可调整）
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Scene2");
    }

}

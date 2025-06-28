using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneFadeIn : MonoBehaviour
{
    [SerializeField] private float fadeInDuration = 1f;
    Image img;
    private void Awake()
    {
        img = GetComponent<Image>();
    }
    void Start()
    {
        img.color = new Color(0, 0, 0, 1); // 确保初始为全黑
        img.DOFade(0, fadeInDuration).OnComplete(() => Destroy(gameObject));
    }
}
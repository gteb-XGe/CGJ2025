using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Quaternion originalRotation;
    private Vector3 originalScale;
    private float lastPressTime;
    private float cooldownDuration = 1f;

    void Start()
    {
        originalRotation = transform.localRotation;
        originalScale = transform.localScale;
        lastPressTime = -cooldownDuration;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastPressTime >= cooldownDuration)
        {
            lastPressTime = Time.time;
            IPossessable possessable = GetComponentInParent<IPossessable>();
            if (possessable != null)
            {
                possessable.Unpossess();
                transform.localRotation = originalRotation;
                transform.localScale = originalScale;
            }
        }
    }
}

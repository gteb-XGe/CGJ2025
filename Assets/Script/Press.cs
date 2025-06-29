using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Press : MonoBehaviour
{
    public UnityEvent onClick;

    void OnMouseDown()
    {
        onClick.Invoke();
        Debug.Log("Clicked: " + gameObject.name);
    }
}

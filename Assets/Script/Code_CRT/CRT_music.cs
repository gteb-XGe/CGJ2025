using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRT_music : MonoBehaviour
{
    private void Awake()
    {
        MusicManager.Instance.PlayAudio("1",Globals.BGM_Start);
        MusicManager.Instance.PlayAudio("2", Globals.BGM_Main,1,false);
        MusicManager.Instance.PlayAudio("3", Globals.BGM_MainLoop, 1, true);
    }
}

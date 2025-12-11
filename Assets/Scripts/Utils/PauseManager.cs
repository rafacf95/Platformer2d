using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using UnityEngine;

public class PauseManager : Singleton<PauseManager>
{

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }
}

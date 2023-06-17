using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static void Pause()
    {
        Time.timeScale = 0f; // set time scale to 0
    }

    public static void Resume()
    {
        Time.timeScale = 1f; // set time scale to 1
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton Instance  Init
    public static GameManager GM;

    private void Awake()
    {
        Time.timeScale = 1;
        if (GM == null)
        {
            GM = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Game Flow Flags & Reset Logic
    public bool IS_GAME_STARTED;
    public bool IS_GAME_OVER;
    public bool IS_GAME_COMPLETED;
    public bool IS_GAME_PAUSED;

    public void ResetVals()
    {
        Time.timeScale = 1;
        IS_GAME_STARTED = false;
        IS_GAME_OVER = false;
        IS_GAME_PAUSED = false;
        IS_GAME_PAUSED = false;
    }
    #endregion
}

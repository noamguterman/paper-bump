using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int num = Mathf.Min(Const.TOTAL_LEVEL, Prefs.UnlockedLevel);
        SceneManager.LoadScene("Level_" + num);
    }
}

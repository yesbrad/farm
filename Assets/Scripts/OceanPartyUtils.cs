#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class OceanPartyUtils : MonoBehaviour
{
    [MenuItem("Ocean Party/Clear PlayerPrefs")]
    static void ClearPlayerPrefs()
    {
        Debug.Log("Cleared Player Prefs!");
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Ocean Party/ReloadScene")]
    static void ReloadScene()
    {
        Debug.Log("ReloadedScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
#endif

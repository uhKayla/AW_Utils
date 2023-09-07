using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System;

[InitializeOnLoad]
public class AW_AutoSave : MonoBehaviour
{
    private static DateTime lastSaveTime;
    private static float saveIntervalMinutes = 5f; // mins between saves

    static AW_AutoSave()
    {
        EditorApplication.playModeStateChanged += SaveOnPlayModeChange;
    }

    [MenuItem("AutoSave/Save Now")]
    private static void SaveNow()
    {
        SaveScene();
    }

    private static void SaveOnPlayModeChange(PlayModeStateChange state)
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
        {
            SaveScene();
        }
    }

    private static void SaveScene()
    {
        if ((DateTime.Now - lastSaveTime).TotalMinutes >= saveIntervalMinutes)
        {
            lastSaveTime = DateTime.Now;
            EditorSceneManager.SaveOpenScenes();
            Debug.Log("Auto-Saved scene at: " + DateTime.Now.ToString());
        }
    }
}
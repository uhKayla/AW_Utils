using UnityEditor;
using UnityEditor.SceneManagement;

namespace ANGELWARE.AW_Platform.Utils
{
    public class AW_SceneReload : UnityEditor.Editor
    {
        [MenuItem("ANGELWARE/Reload Scene")]
        public static void ReloadScene()
        {
            // Save the changes in the currently open scene (optional)
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

            // Reload the currently open scene
            EditorSceneManager.OpenScene(EditorSceneManager.GetActiveScene().path);
        }
    }
}

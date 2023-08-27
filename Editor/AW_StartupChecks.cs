using UnityEditor;

namespace ANGELWARE.AW_Utils.Editor
{

    [InitializeOnLoad]
    public class StartupTrigger
    {
        static StartupTrigger()
        {
            EditorApplication.delayCall += StartVersionCheck;
        }

        private static void StartVersionCheck()
        {
            AW_SDKValidator.CheckSDKVersion();
            AW_FuryValidator.CheckSDKVersion();
        }
    }
}
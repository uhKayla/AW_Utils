using UnityEditor;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ANGELWARE.AW_Utils.Editor
{
    //TODO Merge this into the AW_SDKVersionValidator, this should not be it's own script, the other one should be modular and allow checking of multiple defined packages for ease of use.

    public class AW_FuryValidator
    {
        [Serializable]
        public class VpmManifest
        {
            public Dictionary<string, PackageInfo> dependencies;
        }

        [Serializable]
        public class PackageInfo
        {
            public string version;
        }

        public static void CheckSDKVersion()
        {
            string requiredFuryVersion = "1.0.0";
            string requiredPackageName = "com.vrcfury.vrcfury";
            string installLink = "vcc://vpm/addRepo?url=https%3A%2F%2Fvcc.vrcfury.com";

            string vpmManifestPath = Path.Combine(Application.dataPath, "..", "Packages", "vpm-manifest.json");

            if (File.Exists(vpmManifestPath))
            {
                string vpmManifestContent = File.ReadAllText(vpmManifestPath);
                VpmManifest vpmManifest = JsonConvert.DeserializeObject<VpmManifest>(vpmManifestContent);

                if (vpmManifest.dependencies != null && vpmManifest.dependencies.ContainsKey(requiredPackageName))
                {
                    PackageInfo packageInfo = vpmManifest.dependencies[requiredPackageName];
                    string packageVersion = packageInfo.version;

                    if (IsVersionGreater(packageVersion, requiredFuryVersion))
                    {
                        Debug.Log(requiredPackageName + " version is greater than " + requiredFuryVersion);
                    }
                    else
                    {
                        Debug.LogError(requiredPackageName + " version is not greater than " + requiredFuryVersion +
                                       ". Please update VRCFury.");

                        // Display an editor dialog
                        if (EditorUtility.DisplayDialog("SDK Version Check",
                                "The " + requiredPackageName + " version is lower than " + requiredFuryVersion +
                                ". Please update VRCFury.", "OK"))
                        {
                            // User clicked OK
                        }
                    }
                }
                else
                {
                    string errorMessage = requiredPackageName +
                                          " not found in vpm-manifest.json. Do you want to install it?";
                    DisplayErrorDialog(errorMessage, "OK", installLink);
                }
            }
            else
            {
                Debug.LogError("vpm-manifest.json file not found.");
            }
        }

        private static bool IsVersionGreater(string versionA, string versionB)
        {
            System.Version vA = new System.Version(versionA);
            System.Version vB = new System.Version(versionB);

            return vA.CompareTo(vB) > 0;
        }

        private static void DisplayErrorDialog(string message, string confirmButtonText, string installLink)
        {
            if (installLink != null)
            {
                if (EditorUtility.DisplayDialog("SDK Version Check", message, confirmButtonText, "Install"))
                {
                    // User clicked OK
                }
                else
                {
                    // User clicked Install, open the install link
                    Application.OpenURL(installLink);
                }
            }
            else
            {
                if (EditorUtility.DisplayDialog("SDK Version Check", message, confirmButtonText))
                {
                    // User clicked OK
                }
            }
        }
    }
}

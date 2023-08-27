using UnityEditor;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ANGELWARE.AW_Utils.Editor
{

    public class AW_SDKValidator
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
            string requiredSDKVersion = "3.2.2";
            string requiredPackageName = "com.vrchat.avatars";

            string vpmManifestPath = Path.Combine(Application.dataPath, "..", "Packages", "vpm-manifest.json");

            if (File.Exists(vpmManifestPath))
            {
                string vpmManifestContent = File.ReadAllText(vpmManifestPath);
                VpmManifest vpmManifest = JsonConvert.DeserializeObject<VpmManifest>(vpmManifestContent);

                if (vpmManifest.dependencies != null && vpmManifest.dependencies.ContainsKey(requiredPackageName))
                {
                    PackageInfo packageInfo = vpmManifest.dependencies[requiredPackageName];
                    string packageVersion = packageInfo.version;

                    if (IsVersionGreater(packageVersion, requiredSDKVersion))
                    {
                        Debug.Log(requiredPackageName + " version is greater than " + requiredSDKVersion);
                    }
                    else
                    {
                        Debug.LogError(requiredPackageName + " version is not greater than " + requiredSDKVersion +
                                       ". Please update the VRChat SDK.");

                        // Display an editor dialog
                        if (EditorUtility.DisplayDialog("SDK Version Check",
                                "The " + requiredPackageName + " version is lower than " + requiredSDKVersion +
                                ". Please update the SDK.", "OK"))
                        {
                            // User clicked OK
                        }
                    }
                }
                else
                {
                    Debug.LogError(requiredPackageName + " not found in vpm-manifest.json.");
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
    }
}

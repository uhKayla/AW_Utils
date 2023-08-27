using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using PackageInfo = ANGELWARE.AW_Platform.Editor.ScriptableObjects.PackageInfo;

namespace ANGELWARE.AW_Platform.Editor.Utils
{
    public class AW_ParameterManager : UnityEditor.Editor
    {
        private static int CalculateTotalValue(VRCExpressionParameters expressionParameters)
        {
            // value type definition, i don't think i actually needed to write this, but its here lol.
            var paramList = expressionParameters.parameters;
            var totalValue = 0;

            foreach (var param in paramList)
                if (param.networkSynced)
                {
                    var valueTypeValue = 0;
                    switch (param.valueType)
                    {
                        case VRCExpressionParameters.ValueType.Int:
                            valueTypeValue = 8;
                            break;
                        case VRCExpressionParameters.ValueType.Float:
                            valueTypeValue = 8;
                            break;
                        case VRCExpressionParameters.ValueType.Bool:
                            valueTypeValue = 1;
                            break;
                    }

                    totalValue += valueTypeValue;
                }

            return totalValue;
        }

        // calc total expression memory
        public static int CalculateCombinedTotalValue(VRCExpressionParameters expressionParameters1,
            string expressionParametersAssetPath)
        {
            var totalValue1 = CalculateTotalValue(expressionParameters1);
            var expressionParameters2 =
                AssetDatabase.LoadAssetAtPath<VRCExpressionParameters>(expressionParametersAssetPath);

            if (expressionParameters2 == null) return totalValue1;
            var totalValue2 = CalculateTotalValue(expressionParameters2);
            return totalValue1 + totalValue2;
        }

        public static VRCExpressionParameters GetExpressionParameters(GameObject avatarObject)
        {
            var avatarDescriptor = avatarObject.GetComponent<VRCAvatarDescriptor>();

            if (avatarDescriptor == null)
            {
                Debug.LogWarning("No AvatarDescriptor found on the Avatar.");
                return null;
            }

            if (avatarDescriptor.expressionParameters == null)
                Debug.LogWarning("No Expressions Parameters found on the Avatar");

            return avatarDescriptor.expressionParameters;
        }

        public static VRCExpressionsMenu GetMainMenu(GameObject avatarObject)
        {
            var avatarDescriptor = avatarObject.GetComponent<VRCAvatarDescriptor>();

            if (avatarDescriptor == null)
            {
                Debug.LogWarning("No Avatar Descriptor found on the Avatar.");
                return null;
            }

            if (avatarDescriptor.expressionsMenu == null)
            {
                Debug.LogWarning("No Expressions Menu found on the Avatar.");
                return null;
            }

            return avatarDescriptor.expressionsMenu;
        }

        // This method reads the JSON file and returns the parameter assets paths
        public static string GetParameterAssetPathFromJson(string package_name)
        {
            var jsonPath = "Assets/ANGELWARE/COMMON/NET-DATA/packages.json";
            var jsonText = File.ReadAllText(jsonPath);
            var packageInfos = JsonConvert.DeserializeObject<List<PackageInfo>>(jsonText);
            var targetPackage = packageInfos.FirstOrDefault(p => p.package_name == package_name);
            if (jsonText == "")
            {
                Debug.LogError("Packages file missing!");
                return null;
            }

            if (targetPackage != null) return targetPackage.parameter_assets;
            return string.Empty;
        }

        // load parameter lists
        public static VRCExpressionParameters LoadParameterAsset(string path)
        {
            return AssetDatabase.LoadAssetAtPath<VRCExpressionParameters>(path);
        }

        public static void RemoveParameters(string prefix, VRCExpressionParameters expressionParameters)
        {
            // creates a new parameters list, minus prefixed entries. also backs up list to avoid losing things.
            var backupParams = Instantiate(expressionParameters);
            backupParams.name = expressionParameters.name + "_Backup";
            var originalPath = AssetDatabase.GetAssetPath(expressionParameters);
            var backupPath = Path.GetDirectoryName(originalPath) + "/" + backupParams.name + ".asset";
            AssetDatabase.CreateAsset(backupParams, backupPath);
            var paramsToKeep = new List<VRCExpressionParameters.Parameter>();
            foreach (var param in expressionParameters.parameters)
                if (!param.name.StartsWith(prefix))
                    paramsToKeep.Add(param);
            expressionParameters.parameters = paramsToKeep.ToArray();
            Debug.Log(paramsToKeep);
        }

        public static void RemoveSubMenu(VRCExpressionsMenu mainMenu)
        {
            // creates a new main menu list, copies needed components minus gogoloco, specifically for full bunnysuit.
            var updatedControls = mainMenu.controls.ToList();
            var controlsToKeep = new List<VRCExpressionsMenu.Control>();
            updatedControls.RemoveAll(control =>
                control.type == VRCExpressionsMenu.Control.ControlType.SubMenu && control.name == "GoGo Loco");
            controlsToKeep.AddRange(updatedControls);
            mainMenu.controls = controlsToKeep;
        }
    }
}
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ANGELWARE.Utils.Editor
{
    public class AW_GuidList : EditorWindow
    {
        [MenuItem("ANGELWARE/Tools/Generate GUID List")]
        private static void Init()
        {
            var window = (AW_GuidList)GetWindow(typeof(AW_GuidList));
            window.Show();
        }

        private bool _includeQuotes;
        private string _guidList = "";

        private void OnGUI()
        {
            GUILayout.Label("Selected Assets GUIDs", EditorStyles.boldLabel);
            
            _includeQuotes = EditorGUILayout.Toggle("Include Quotes", _includeQuotes);
            
            if (GUILayout.Button("Generate GUID List"))
            {
                var selectedAssets = Selection.assetGUIDs;

                var guidStrings = selectedAssets.Select(assetGuid => _includeQuotes ? '"' + assetGuid + '"' : assetGuid).ToList();

                _guidList = string.Join(", ", guidStrings);
            }

            EditorGUILayout.TextArea(_guidList, GUILayout.Height(100));
        }
    }

}
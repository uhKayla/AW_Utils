using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

//created to make assets for working with blendtrees after dealing with godrevenger's weirdness
namespace ANGELWARE.Utils.Editor
{
    public class AW_BlendTreeAssets : MonoBehaviour
    {
        [MenuItem("Assets/Create/BlendTree")]
        private static void CreateNewBlendTree()
        {
            // find selected folder :3
            var selectedFolderPath = "Assets";
            var selectedObject = Selection.activeObject;
            if (selectedObject != null && AssetDatabase.Contains(selectedObject))
            {
                var assetPath = AssetDatabase.GetAssetPath(selectedObject);
                if (!string.IsNullOrEmpty(assetPath) && System.IO.Directory.Exists(assetPath))
                {
                    selectedFolderPath = assetPath;
                }
            }

            // create & save blendtree at the assetpath
            var blendTree = new BlendTree
            {
                name = "NewBlendTree"
            };

            var blendTreeAssetPath = System.IO.Path.Combine(selectedFolderPath, "NewBlendTree.asset");

            AssetDatabase.CreateAsset(blendTree, blendTreeAssetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
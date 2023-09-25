using System.Collections.Generic;
using Thry;
using UnityEditor;
using UnityEngine;

namespace ANGELWARE.Utils.Editor
{
    public static class AW_MaterialEditor{
        // #if AW_DEBUG
        //[MenuItem("ANGELWARE/Assign Alpha Mask Texture")]
        // #endif
        public static void AssignClippingMaskTexture()
        {
            // using constants here, doesnt really matter as im just gonna adapt the script if i need to re-use it
            // yeah non-modular shit, but its better than nothing.
            const string targetMaterialGuid = "c39cbfaa50bf3924eb74c2bb1f768850"; // body material EX
            const string target2MaterialGuid = "e599c988cbb9e004a96ffff5d58e13ad"; // body material
            const string clippingMaskTextureGuid = "d653264ffc39fbd4cae88ad8d1a5fd0f"; // alpha mask

            // load assets
            Material targetMaterial = AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(targetMaterialGuid));
            Material target2Material = AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(target2MaterialGuid));
            Texture clippingMaskTexture = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(clippingMaskTextureGuid));

            if (targetMaterial != null && clippingMaskTexture != null)
            {
                // unlock the material before editing.
                ShaderOptimizer.SetLockedForAllMaterials(new List<Material> { targetMaterial }, 0, true);

                // record undo and assign alpha texture
                Undo.RecordObject(targetMaterial, "Assign Alpha Mask Texture");
                targetMaterial.SetTexture("_ClippingMask", clippingMaskTexture);
                
                // poiyomi uses tags, not peroperties to set the animated values, this was a fucking headache to figure out.
                targetMaterial.SetOverrideTag("_AlphaModAnimated", "1");
                

                EditorUtility.SetDirty(targetMaterial);
                
                // lock material when we are done to prevent crashes. i thought about checking for this first, but
                // with this package i am going to just save time and assume people need to lock it.
                ShaderOptimizer.SetLockedForAllMaterials(new List<Material> { targetMaterial }, 1, true);
            }
            else
            {
                Debug.LogWarning("Missing Body Material or Alpha Mask.");
            }
            // yeah... im doing it
            if (target2Material != null && clippingMaskTexture != null)
            {
                // unlock the material before editing.
                ShaderOptimizer.SetLockedForAllMaterials(new List<Material> { target2Material }, 0, true);

                // record undo and assign alpha texture
                Undo.RecordObject(target2Material, "Assign Alpha Mask Texture");
                target2Material.SetTexture("_ClippingMask", clippingMaskTexture);
                
                // poiyomi uses tags, not peroperties to set the animated values, this was a fucking headache to figure out.
                target2Material.SetOverrideTag("_AlphaModAnimated", "1");
                

                EditorUtility.SetDirty(target2Material);
                
                // lock material when we are done to prevent crashes. i thought about checking for this first, but
                // with this package i am going to just save time and assume people need to lock it.
                ShaderOptimizer.SetLockedForAllMaterials(new List<Material> { target2Material }, 1, true);
            }
            else
            {
                Debug.LogWarning("Missing Body Material or Alpha Mask.");
            }
        }
        
        // this will lock all materials for the revenger by guid. hopefully will fix crashing, we can use it as a precaution
        // since we are modifying mats already. keeping the menu as it might be useful lol.
        [MenuItem("ANGELWARE/Tools/Lock Revenger Materials")]
        public static void LockCrashPrevention()
        {
            {
                // this is all (i think) of the revenger's poiyomi mats. its ugly but this is how thry wants it, and we are calling from there.
                var materialGuids = new List<string>{"5c06fd225342b18408dfe4608459c6be", "efc4f623c0a1ee2489be63673efcd898", "a7a15984959c60f449c7191252596494", "7689a0943059df1469b6811f14f7ec0b", "2d2090d5ccff3904397f5e31ba782eba", "56e7d0e5f498d8d4396fc9f1c7b67fbb", "e92f4256601b604499fb75438bc30575", "07e71ec6b0d004641a63874a36d54258", "fa5c47e6d0c79ac4cae0ce5b5ad396a0", "e19a4d18cc817fc43993dcad3b9bca1a", "eac10088603f090439ea36d03397976a", "c39cbfaa50bf3924eb74c2bb1f768850", "3a61adbb4e181fd47a607a27463be3de", "2ac4ffeb3eb056f4d888c37a41954747", "e2c4cbec8ec6c484eb3b23f2558903e6", "30eac75ec12eab8439ac098de607812d", "4431b27eb5b7aff46b6bd9368c04e4cb", "81a4307feda973342ab715bda5afa8a3", "270d3540539099442abb0134cfadf6fc", "7e81049283fbd3f4c81e054569bf2e02", "3cb870232528b6f4192a385e79748da5", "2a00a844019538a4c8437619635aa0c7", "670caca40b806bd40986a6c66c0a6a67", "62b14db4ac554be4a8ceb5677bf6e0a6", "7f4572f4332e3114eb91da897b9f39d4", "5a54a7353018f374ba6cbb89555b15b7", "f9ea1416356d3674cb5f712099301021", "d95f3396e5ccb734c8d7f9905d94f0bd", "508f6ff6b72da844981e0ff20f00fe40", "bf7bb1974b29c5649b829cd1d7287740", "3a1827a735139054094b0d9d7a005332", "31f0ef3883b3d8a45af21203026929d1", "e599c988cbb9e004a96ffff5d58e13ad", "afc8da986a37a154c99a5894a7892d1d", "bcb95369143d7b540bb84536bb03d5bb", "822fe50a130cc74488f405ce5222bf9c", "1f66bc1b96ca869489b0149413741e24", "e1ac452b1440f884db7c4c10a744f655", "0d39b5eb1f2f1384a962cc6c691f2c8c", "77515f2c56abd6e4d9db4dcb22b3c03d", "b086dd4ccf7e3af419a713511a797a73", "74da738c7d035e848ba5523af955582b", "c88233adc9b1d194a923e11210788f38", "e7201e2e53bffe44fa49a5f5600535c5", "7aaea43fe7204f44fb088c9c01021fd2"};
                var materials = new List<Material>();

                foreach (var guidString in materialGuids)
                {
                        Material material = AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(guidString));
                        if (material != null)
                        {
                            materials.Add(material);
                        }
                        else
                        {
                            Debug.LogWarning("Material with GUID " + guidString + " not found.");
                        }
                }
                ShaderOptimizer.SetLockedForAllMaterials(materials, 1, true);
            }
        }
    }
}

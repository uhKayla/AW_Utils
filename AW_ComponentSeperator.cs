#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using VRC.SDKBase;

namespace ANGELWARE.AW_Utils
{
    public class AW_ComponentSeperator : MonoBehaviour,IEditorOnly
    {
        // Actual component class for the AW_Seperator script.
        [HideInInspector] public string Label = "Custom Label Text";
        [HideInInspector] public string Notes = "";
    }
}
#endif
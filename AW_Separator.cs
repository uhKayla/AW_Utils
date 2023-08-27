#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using VRC.SDKBase;

namespace ANGELWARE.AW_Utils
{
    [CustomEditor(typeof(AW_ComponentSeperator))]
    public class AW_Seperator : Editor, IEditorOnly
    {
        // This class defines a super simple component that I use for organisation and literally nothing else.
        // You can add notes that *should* automatically fold out when text is present, and label the separator
        // component however you like.
        // IEditorOnly is used here to define that the script should be removed upon upload to VRChat.
        private SerializedProperty customLabelProperty;
        private SerializedProperty notesProperty;
        private bool showNotesFoldout;

        private void OnEnable()
        {
            customLabelProperty = serializedObject.FindProperty("Label");
            notesProperty = serializedObject.FindProperty("Notes");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            AW_ComponentSeperator separatorComponent = (AW_ComponentSeperator)target;

            DrawDefaultInspector();

            GUILayout.Space(10);
            // Title Style
            GUIStyle separatorStyle = new GUIStyle(GUI.skin.label);
            separatorStyle.fontStyle = FontStyle.Bold;
            separatorStyle.fontSize = 14;
            separatorStyle.alignment = TextAnchor.MiddleCenter;

            GUILayout.Label("================ Separator ================", separatorStyle);

            GUILayout.Space(5);
            // Label Field
            EditorGUI.BeginChangeCheck();
            string newCustomLabel = EditorGUILayout.TextField("Label:", customLabelProperty.stringValue);
            if (EditorGUI.EndChangeCheck())
            {
                customLabelProperty.stringValue = newCustomLabel;
                serializedObject.ApplyModifiedProperties();
            }

            GUILayout.Space(10);
            // Notes Foldout
            showNotesFoldout = EditorGUILayout.Foldout(showNotesFoldout, "Notes");
            if (showNotesFoldout)
            {
                EditorGUI.indentLevel++;
                string newNotes = EditorGUILayout.TextArea(notesProperty.stringValue, GUILayout.Height(60));
               // Do not foldout if text is not present.
                if (newNotes != notesProperty.stringValue)
                {
                    notesProperty.stringValue = newNotes;
                    serializedObject.ApplyModifiedProperties();
                }
                EditorGUI.indentLevel--;
            }

            GUILayout.Space(10);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif

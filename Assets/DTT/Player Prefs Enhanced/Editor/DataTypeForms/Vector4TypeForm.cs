using UnityEngine;
using UnityEditor;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Class used to draw the CRUD content for a <see cref="Vector4"/> datatype.
    /// </summary>
    internal class Vector4TypeForm : AbstractTypeForm<Vector4>
    {
        /// <summary>
        /// Set the default values to variables if the <see cref="PlayerPrefModel"/> is unknown or not needed.
        /// </summary>
        public override void SetKeyValue()
        {
            p_key = "";
            p_value = Vector4.zero;
        }

        /// <summary>
        /// Draws the content required to read the player pref data.
        /// </summary>
        public override void DrawReadingFields()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            EditorGUILayout.SelectableLabel($"X: {p_value.x}", GUILayout.Height(p_labelHeight));
            EditorGUILayout.SelectableLabel($"Y: {p_value.y}", GUILayout.Height(p_labelHeight));
            EditorGUILayout.SelectableLabel($"Z: {p_value.z}", GUILayout.Height(p_labelHeight));
            EditorGUILayout.SelectableLabel($"W: {p_value.w}", GUILayout.Height(p_labelHeight));
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws the content required to update the player pref data.
        /// </summary>
        public override void DrawUpdateFields()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            p_value = EditorGUILayout.Vector4Field("", p_value, GUILayout.Height(p_labelHeight));
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws the content required to create a new player pref.
        /// </summary>
        public override void DrawCreateFields()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Key name:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            p_key = EditorGUILayout.TextArea(p_key);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            p_value = EditorGUILayout.Vector4Field("", p_value, GUILayout.Height(p_labelHeight));
            EditorGUILayout.EndHorizontal();

            DrawKeyWarnings();
        }
    }
}

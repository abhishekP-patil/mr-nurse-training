using UnityEngine;
using UnityEditor;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Class used to draw the CRUD content for a <see cref="Color"/> datatype.
    /// </summary>
    internal class ColorTypeForm : AbstractTypeForm<Color>
    {
        /// <summary>
        /// Set the default values to variables if the <see cref="PlayerPrefModel"/> is unknown or not needed.
        /// </summary>
        public override void SetKeyValue()
        {
            p_key = "";
            p_value = Color.white;
        }

        /// <summary>
        /// Draws the content required to read the player pref data.
        /// </summary>
        public override void DrawReadingFields()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            EditorGUILayout.SelectableLabel($"R: {p_value.r}", GUILayout.Height(p_labelHeight));
            EditorGUILayout.SelectableLabel($"G: {p_value.g}", GUILayout.Height(p_labelHeight));
            EditorGUILayout.SelectableLabel($"B: {p_value.b}", GUILayout.Height(p_labelHeight));
            EditorGUILayout.SelectableLabel($"A: {p_value.a}", GUILayout.Height(p_labelHeight));
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws the content required to update the player pref data.
        /// </summary>
        public override void DrawUpdateFields()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            p_value = EditorGUILayout.ColorField("", p_value, GUILayout.Height(p_labelHeight));
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
            p_value = EditorGUILayout.ColorField("", p_value, GUILayout.Height(p_labelHeight));
            EditorGUILayout.EndHorizontal();

            DrawKeyWarnings();
        }
    }
}
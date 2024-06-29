using UnityEngine;
using UnityEditor;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Class used to draw the CRUD content for a <see cref="bool"/> datatype.
    /// </summary>
    internal class BoolTypeForm : AbstractTypeForm<bool>
    {
        /// <summary>
        /// Variable to represent the playerpref value.
        /// </summary>
        private bool _value = true;

        /// <summary>
        /// Set the default values to variables if the <see cref="PlayerPrefModel"/> is unknown or not needed.
        /// </summary>
        public override void SetKeyValue()
        {
            p_key = "";
            _value = true;
        }

        /// <summary>
        /// Draws the content required to read the player pref data.
        /// </summary>
        public override void DrawReadingFields()
        {
            string boolValue = _value? "True" : "False";

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            EditorGUILayout.SelectableLabel(boolValue, GUILayout.Height(p_labelHeight));
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws the content required to update the player pref data.
        /// </summary>
        public override void DrawUpdateFields()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            _value = EditorGUILayout.Toggle(_value);
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
            _value = EditorGUILayout.Toggle(_value);
            EditorGUILayout.EndHorizontal();

            DrawKeyWarnings();
        }
    }
}
using DTT.PlayerPrefsEnhanced;
using UnityEditor;
using UnityEngine;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Class used to draw the content to read a player new player pref.
    /// </summary>
    internal class ReadPlayerPrefForm : AbstractPrefForm
    {
        /// <summary>
        /// Sets the Editor Window used to display content on.
        /// </summary>
        /// <param name="window">The target Editor Window.</param>
        public override void SetWindow(EditorWindow window)
        { 
            base.SetWindow(window);
            p_window.titleContent = new GUIContent($"Reading Pref: {p_playerPref.Key}");
        }

        /// <summary>
        /// Draws the content to be displayed on the Editor Window.
        /// </summary>
        public override void DrawContent()
        {
            if (p_dataTypeForm == null)
            {
                EditorGUILayout.HelpBox("Reading of this DataType is currently not supported.", MessageType.Warning);

                return;
            }

            string boolValue = "False";
            if (p_prefIsEncrypted)
                boolValue = "True";

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Encrypted:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            EditorGUILayout.SelectableLabel(boolValue, GUILayout.Height(p_labelHeight));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Key name:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            EditorGUILayout.SelectableLabel(p_playerPref.Key, GUILayout.Height(p_labelHeight));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Data Type:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            EditorGUILayout.SelectableLabel(p_playerPref.FullDataTypeName, GUILayout.Height(p_labelHeight));
            EditorGUILayout.EndHorizontal();

            p_dataTypeForm.DrawReadingFields();
        }
    }
}
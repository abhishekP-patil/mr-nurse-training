using UnityEditor;
using UnityEngine;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Class used to draw the content to update a player new player pref.
    /// </summary>
    internal class UpdatePlayerPrefForm : AbstractPrefForm
    {
        /// <summary>
        /// Sets the icon associated with the forms opperation.
        /// </summary>
        /// <param name="icon">Associated icon</param>
        public UpdatePlayerPrefForm(Texture2D icon) => p_icon = icon;

        /// <summary>
        /// Sets the Editor Window used to display content on.
        /// </summary>
        /// <param name="window">The target Editor Window.</param>
        public override void SetWindow(EditorWindow window)
        {
            base.SetWindow(window);
            p_window.titleContent = new GUIContent($"Update Pref: {p_playerPref.Key}");
        }

        /// <summary>
        /// Draws the content to be displayed on the Editor Window.
        /// </summary>
        public override void DrawContent()
        {
            if (p_dataTypeForm == null)
            {
                EditorGUILayout.HelpBox("Editing of this DataType is currently not supported.", MessageType.Warning);

                return;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Encrypted:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            p_prefIsEncrypted = EditorGUILayout.Toggle(p_prefIsEncrypted, GUILayout.Height(p_labelHeight));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Key name:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            string key = EditorGUILayout.TextField(p_dataTypeForm.Key);
            if (p_dataTypeForm.Key != key)
                p_dataTypeForm.Key = key;
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Data Type:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            EditorGUILayout.SelectableLabel(p_playerPref.FullDataTypeName, GUILayout.Height(p_labelHeight));
            EditorGUILayout.EndHorizontal();

            p_dataTypeForm.DrawUpdateFields();

            bool keyChanged = p_playerPref.Key != p_dataTypeForm.Key;
            if (keyChanged)
                p_dataTypeForm.DrawKeyWarnings();

            if (GUILayout.Button("Save"))
            {
                if (keyChanged)
                {
                    if (!p_dataTypeForm.NewPlayerPrefValidation())
                        return;

                    EnhancedPrefs.DeletePlayerPref(p_playerPref.Key);
                }

                p_dataTypeForm.SavePlayerPref(p_prefIsEncrypted);
                p_window.Close();
            }
        }
    }
}
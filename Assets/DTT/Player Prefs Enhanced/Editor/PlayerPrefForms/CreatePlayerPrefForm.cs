using UnityEditor;
using UnityEngine;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Class used to draw the content to create a player new player pref based on an existing player pref.
    /// </summary>
    internal class CreatePlayerPrefForm : AbstractPrefForm
    {
        /// <summary>
        /// Sets the Editor Window used to display content on.
        /// </summary>
        /// <param name="window">The target Editor Window.</param>
        public override void SetWindow(EditorWindow window)
        {
            base.SetWindow(window);
            p_window.titleContent = new GUIContent($"Create Pref from: {p_playerPref.Key}");
        }

        /// <summary>
        /// Draws the content to be displayed on the Editor Window.
        /// </summary>
        public override void DrawContent()
        {
            if (p_dataTypeForm == null)
            {
                EditorGUILayout.HelpBox("Creating PlayerPrefs from this DataType is currently not supported.", MessageType.Warning);

                return;
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Encrypted:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            p_prefIsEncrypted = EditorGUILayout.Toggle(p_prefIsEncrypted, GUILayout.Height(p_labelHeight));
            EditorGUILayout.EndHorizontal();

            p_dataTypeForm.DrawCreateFields();

            if (GUILayout.Button("Save"))
            {
                if (p_dataTypeForm.NewPlayerPrefValidation())
                {
                    p_dataTypeForm.SavePlayerPref(p_prefIsEncrypted);
                    p_window.Close();
                }
            }
        }

        /// <summary>
        /// Sets the key and varibale values to default values.
        /// </summary>
        protected override void SetTypeFormValues() => p_dataTypeForm.SetKeyValue();
    }
}
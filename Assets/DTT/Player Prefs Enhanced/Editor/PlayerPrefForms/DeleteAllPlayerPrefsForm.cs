using UnityEditor;
using UnityEngine;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Class used to draw the content to delete all player prefs.
    /// </summary>
    internal class DeleteAllPlayerPrefsForm : AbstractPrefForm
    {
        /// <summary>
        /// Whether only prefs created using this asset or all prefs should be deleted.
        /// </summary>
        private bool _deleteOnlyEnhancedPlayerPrefs = true;

        /// <summary>
        /// Draws the content to be displayed on the Editor Window.
        /// </summary>
        public override void DrawContent()
        {
            EditorGUILayout.HelpBox("Are you sure you want to delete all Prefs?", MessageType.Warning);
            EditorGUILayout.HelpBox("This might take a while.\nA manual refresh may be needed.", MessageType.Warning);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Delete Only Enhanced Player Prefs:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth * 3), GUILayout.Height(p_labelHeight));
            _deleteOnlyEnhancedPlayerPrefs = EditorGUILayout.Toggle(_deleteOnlyEnhancedPlayerPrefs, GUILayout.Height(p_labelHeight));
            EditorGUILayout.EndHorizontal();


            if (GUILayout.Button("Delete all"))
            {
                if (!_deleteOnlyEnhancedPlayerPrefs)
                    EnhancedPrefs.DeleteAllPlayerPrefs();
                else
                    EnhancedPrefs.DeleteAllEnhancedPlayerPrefs();

                p_window.Close();
            }
        }
    }
}
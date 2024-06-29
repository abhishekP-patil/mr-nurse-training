using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Class used to draw the content to create a player new player pref.
    /// </summary>
    internal class CreateNewPlayerPrefForm : CreatePlayerPrefForm
    {
        /// <summary>
        /// Used to decide the <see cref="IDataTypeForm"/> to display.
        /// </summary>
        private DataTypes _selectedDataType = DataTypes.STRING;

        /// <summary>
        /// Index of currently selected datatype name.
        /// </summary>
        private int _selectedIndex = 0;

        /// <summary>
        /// List of supported data type names.
        /// </summary>
        private List<string> _dataTypeOptions = new List<string>();

        /// <summary>
        /// Sets the Editor Window used to display content on.
        /// </summary>
        /// <param name="window">The target Editor Window.</param>
        public override void SetWindow(EditorWindow window)
        {
            p_window = window;

            p_window.titleContent = new GUIContent($"Create New Player Pref");

            p_prefIsEncrypted = true;

            SetDataTypeForm(_selectedDataType);

            AsignSelectionNames();
        }

        /// <summary>
        /// Draws the content to be displayed on the Editor Window.
        /// </summary>
        public override void DrawContent()
        {
            GUI.changed = false;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Data Type:", EditorStyles.boldLabel, GUILayout.Width(p_labelWidth), GUILayout.Height(p_labelHeight));
            _selectedIndex = EditorGUILayout.Popup(_selectedIndex, _dataTypeOptions.ToArray(), GUILayout.Height(p_labelHeight));
            _selectedDataType = (DataTypes) _selectedIndex;
            EditorGUILayout.EndHorizontal();

            if (GUI.changed)
                SetDataTypeForm(_selectedDataType);

            base.DrawContent();
        }

        /// <summary>
        /// Sets the key and varibale values to default values.
        /// </summary>
        protected override void SetTypeFormValues()
        {
            if (string.IsNullOrEmpty(p_dataTypeForm.Key))
            {
                p_dataTypeForm.SetKeyValue();
                return;
            }

            string prefKey = p_dataTypeForm.Key;

            if (EnhancedPrefs.HasPlayerPref(prefKey))
                prefKey = "";

            p_dataTypeForm.Key = prefKey;
        }

        /// <summary>
        /// Create data type option names based on <see cref="DataTypes"/>.
        /// </summary>
        private void AsignSelectionNames()
        {
            _dataTypeOptions.Clear();

            foreach (DataTypes dataType in Enum.GetValues(typeof(DataTypes)))
            {
                if (dataType == DataTypes.NOT_SUPPORTED)
                    continue;

                string name = dataType.ToString().ToLower();
                string firstLetter = name.Substring(0, 1);
                string restLetters = name.Substring(1, name.Length - 1);
                name = firstLetter.ToUpper() + restLetters;
                _dataTypeOptions.Add(name);
            }
        }
    }
}
using System;
using UnityEditor;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Abstract class that contains shared information between the different <see cref="IDataTypeForm"/> instances.
    /// </summary>
    internal abstract class AbstractTypeForm<T> : IDataTypeForm
    {
        /// <summary>
        /// The player pref key used to get the stored player pref value.
        /// </summary>
        public string Key
        {
            get => p_key;
            set
            {
                p_key = value;

                if (EnhancedPrefs.HasPlayerPref(p_key))
                    p_value = EnhancedPrefs.GetPlayerPref(p_key, default(T));
            }
        }

        /// <summary>
        /// The player pref key used to get the stored player pref value.
        /// </summary>
        protected static string p_key;

        /// <summary>
        /// Variable to represent the playerpref value.
        /// </summary>
        protected static T p_value;

        /// <summary>
        /// The height of the labels and inputfields.
        /// </summary>
        protected int p_labelHeight = 20;

        /// <summary>
        /// The width of the labels and inputfields prefixes.
        /// </summary>
        protected int p_labelWidth = 80;

        /// <summary>
        /// Set the default values to variables if the <see cref="PlayerPrefModel"/> is unknown or not needed.
        /// </summary>
        public abstract void SetKeyValue();

        /// <summary>
        /// Draws the content required to read the player pref data.
        /// </summary>
        public abstract void DrawReadingFields();

        /// <summary>
        /// Draws the content required to update the player pref data.
        /// </summary>
        public abstract void DrawUpdateFields();

        /// <summary>
        /// Draws the content required to create a new player pref.
        /// </summary>
        public abstract void DrawCreateFields();

        /// <summary>
        /// Saves the player pref with the current values.
        /// </summary>
        public void SavePlayerPref(bool encrypt) => EnhancedPrefs.SetPlayerPref(p_key, p_value, encrypt);

        /// <summary>
        /// Validates the current values that are getting saved.
        /// </summary>
        /// <returns>True if values are valid.</returns>
        public virtual bool NewPlayerPrefValidation()
        {
            bool validated = true;

            if (string.IsNullOrEmpty(p_key))
                validated = false;

            if (EnhancedPrefs.HasPlayerPref(p_key))
                validated = false;

            return validated;
        }

        /// <summary>
        /// Draws warnings for when a key is empty or already exists.
        /// </summary>
        public void DrawKeyWarnings()
        {
            if (EnhancedPrefs.HasPlayerPref(p_key))
                EditorGUILayout.HelpBox("Key name already in use!", MessageType.Warning);

            if (string.IsNullOrEmpty(p_key))
                EditorGUILayout.HelpBox("Key name cannot be empty!", MessageType.Warning);
        }
    }
}
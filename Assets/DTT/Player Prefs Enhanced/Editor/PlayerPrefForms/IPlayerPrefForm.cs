using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTT.PlayerPrefsEnhanced;
using UnityEditor;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Interface that is used by the <see cref="PlayerPrefsEnhancedEditorWindow"/> to open CRUD opperations in an uniform wayfor a player pref.
    /// </summary>
    internal interface IPlayerPrefForm
    {
        /// <summary>
        /// Gets the icon sotred in the form.
        /// </summary>
        /// <returns>The icon corresponding to the form.</returns>
        Texture2D GetIcon();

        /// <summary>
        /// Sets the Editor Window used to display content on.
        /// </summary>
        /// <param name="window">The target Editor Window.</param>
        void SetWindow(EditorWindow window);

        /// <summary>
        /// Set the player pref model the CRUD opperations are used on.
        /// </summary>
        /// <param name="playerPref">The target Player Pref model.</param>
        void SetPlayerPref(PlayerPrefModel playerPref);

        /// <summary>
        /// Sets the <see cref="IDataTypeForm"/> based on the <see cref="DataTypes"/> provided.
        /// </summary>
        /// <param name="type">The Datatype the form should use.</param>
        void SetDataTypeForm(DataTypes type);

        /// <summary>
        /// Draws the content to be displayed on the Editor Window.
        /// </summary>
        void DrawContent();
    }
}
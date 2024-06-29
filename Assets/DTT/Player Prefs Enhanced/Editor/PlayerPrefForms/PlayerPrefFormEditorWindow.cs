using DTT.PlayerPrefsEnhanced;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Editor window used to draw the diffrent <see cref="IPlayerPrefForm"/>s on.
    /// </summary>
    internal class PlayerPrefFormEditorWindow : EditorWindow
    {
        /// <summary>
        /// The current <see cref="IPlayerPrefForm"/> to be drawn.
        /// </summary>
        private static IPlayerPrefForm _playerPrefForm;

        /// <summary>
        /// The open <see cref="PlayerPrefFormEditorWindow"/>.
        /// </summary>
        private static PlayerPrefFormEditorWindow _window;

        /// <summary>
        /// Size of the window.
        /// </summary>
        private static Vector2 _windowSize = new Vector2(300, 150);

        /// <summary>
        /// Sets the window size and the to display <see cref="IPlayerPrefForm"/>.
        /// </summary>
        /// <param name="playerPrefForm">The form to display on this window.</param>
        /// <returns>Player pref enhanced editor window.</returns>
        public static EditorWindow OpenForm(IPlayerPrefForm playerPrefForm)
        {
            _playerPrefForm = playerPrefForm;
            _window = GetWindow<PlayerPrefFormEditorWindow>();
            _window.titleContent = new GUIContent($"Player Pref Form");
            _window.minSize = _windowSize;
            _window.maxSize = _windowSize;
            _window.Show();
            return _window;
        }

        /// <summary>
        /// OnGUI method, draws the <see cref="IPlayerPrefForm"/> content on the window.
        /// </summary>
        public virtual void OnGUI() => _playerPrefForm.DrawContent();

        /// <summary>
        /// Refreshes the playre pref list of <see cref="PlayerPrefsEnhancedEditorWindow"/>.
        /// </summary>
        private void OnDestroy()
        {
            PlayerPrefsEnhancedEditorWindow playerPrefsWindow = GetWindow<PlayerPrefsEnhancedEditorWindow>();
            playerPrefsWindow.RefreshPrefsList();
        }
    }
}
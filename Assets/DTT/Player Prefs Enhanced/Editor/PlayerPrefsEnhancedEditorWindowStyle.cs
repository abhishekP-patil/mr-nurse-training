using DTT.Utils.EditorUtilities;
using UnityEngine;


namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Caching style for <see cref="PlayerPrefsEnhancedEditorWindow"/> .
    /// </summary>
    public class PlayerPrefsEnhancedEditorWindowStyle: GUIStyleCache
    {
        /// <summary>
        /// Style for a simple button component.
        /// </summary>
        public GUIStyle Button => base[nameof(Button)];
        
        /// <summary>
        /// Style for a button with a large padding.
        /// </summary>
        public GUIStyle ButtonLargePadding => base[nameof(ButtonLargePadding)];

        /// <summary>
        /// Style for right aligned label.
        /// </summary>
        public GUIStyle LabelRightAligned => base[nameof(LabelRightAligned)];
        
        /// <summary>
        /// Style for middle aligned label.
        /// </summary>
        public GUIStyle LabelMiddleAligned => base[nameof(LabelMiddleAligned)];
        
        /// <summary>
        /// Style for a centered label.
        /// </summary>
        public GUIStyle LabelCenterAligned => base[nameof(LabelCenterAligned)];
        
        /// <summary>
        /// Constructor for the style cache, instantiate the dictionary with each style.
        /// </summary>
        public PlayerPrefsEnhancedEditorWindowStyle()
        {
            Add(nameof(Button), () => new GUIStyle(GUI.skin.button)
                {
                    margin = new RectOffset(0, 0, 2, 0), 
                    padding = new RectOffset(2, 2, 2, 2)
                });
            Add(nameof(LabelRightAligned), () => new GUIStyle("Label")
            {
                alignment = TextAnchor.MiddleRight
            });
            Add(nameof(LabelMiddleAligned), () => new GUIStyle("Label")
            {
                alignment = TextAnchor.MiddleLeft
            });
            Add(nameof(LabelCenterAligned), () => new GUIStyle("Label")
            {
                alignment = TextAnchor.UpperLeft
            });
            Add(nameof(ButtonLargePadding), () => new GUIStyle(GUI.skin.button)
            {
                margin = new RectOffset(0, 0, 2, 0), 
                padding = new RectOffset(4, 4, 4, 4)
            });
        }
    }
}
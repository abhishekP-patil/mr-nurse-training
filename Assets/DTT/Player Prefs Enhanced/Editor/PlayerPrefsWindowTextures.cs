using System.IO;
using DTT.PublishingTools;
using DTT.Utils.Optimization;
using UnityEditor;
using UnityEngine;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Class used to reference icons from a specific folder.
    /// </summary>
    internal class PlayerPrefsWindowTextures : LazyTexture2DCache
    {
        /// <summary>
        /// Gets the trash icon assouciated with the current unity skin mode.
        /// </summary>
        public Texture2D TrashBin => EditorGUIUtility.isProSkin ? TrashBinIconLight : TrashBinIconDark;

        /// <summary>
        /// Get the dark trash icon.
        /// </summary>
        public Texture2D TrashBinIconDark => base[nameof(TrashBinIconDark)];

        /// <summary>
        /// Get the light trash icon.
        /// </summary>
        public Texture2D TrashBinIconLight => base[nameof(TrashBinIconLight)];

        /// <summary>
        /// Gets the edit icon assouciated with the current unity skin mode.
        /// </summary>
        public Texture2D Edit => EditorGUIUtility.isProSkin ? EditLight : EditDark;

        /// <summary>
        /// Get the dark edit icon.
        /// </summary>
        public Texture2D EditDark => base[nameof(EditDark)];

        /// <summary>
        /// Get the light edit icon.
        /// </summary>
        public Texture2D EditLight => base[nameof(EditLight)];

        /// <summary>
        /// Path used to load the resources from in the assets folder.
        /// </summary>
        private readonly string _assetPath = Path.Combine("Assets", "DTT", "Player Prefs Enhanced", "Editor", "Icons");

        /// <summary>
        /// Base path used to load the resources from.
        /// </summary>
        private readonly string _packagePath = Path.Combine("Packages", "dtt.player-prefs-enhanced", "Editor", "Icons");

        /// <summary>
        /// Adds icons to the dictionairy.
        /// </summary>
        public PlayerPrefsWindowTextures()
        {
            AssetJson asset = DTTEditorConfig.GetAssetJson("dtt.player-prefs-enhanced");
            string path = asset.assetStoreRelease ? _assetPath : _packagePath;
            Add(nameof(TrashBinIconDark), () => AssetDatabase.LoadAssetAtPath<Texture2D>(Path.Combine(path, "Bin Dark.png")));
            Add(nameof(TrashBinIconLight), () => AssetDatabase.LoadAssetAtPath<Texture2D>(Path.Combine(path, "Bin Light.png")));
            Add(nameof(EditDark), () => AssetDatabase.LoadAssetAtPath<Texture2D>(Path.Combine(path, "Edit Dark.png")));
            Add(nameof(EditLight), () => AssetDatabase.LoadAssetAtPath<Texture2D>(Path.Combine(path, "Edit Light.png")));
        }
    }
}
using System;
using UnityEngine;

namespace DTT.PlayerPrefsEnhanced
{
    /// <summary>
    /// Class used to contain the Json value of an player pref value and data type.
    /// </summary>
    [Serializable]
    internal class PlayerPrefWrapper
    {
        /// <summary>
        /// Full name of the datatype that the contained pref value has.
        /// </summary>
        public string FullDataTypeName => _fullDataTypeName;

        /// <summary>
        /// Json value of the player pref.
        /// </summary>
        public string TextValue => _textValue;

        /// <summary>
        /// Whether the player pref data is encrypted.
        /// </summary>
        public bool IsEncrypted => _isEncrypted;

        /// <summary>
        /// Whether the player pref data is a favorite.
        /// </summary>
        public bool IsFavorite => _isFavorite;

        /// <summary>
        /// Whether the player pref data is encrypted.
        /// </summary>
        [SerializeField]
        private bool _isEncrypted;

        /// <summary>
        /// Whether the player pref data is a favorite.
        /// </summary>
        [SerializeField]
        private bool _isFavorite;

        /// <summary>
        /// Full name of the datatype that the contained pref value has.
        /// </summary>
        [SerializeField]
        private string _fullDataTypeName;

        /// <summary>
        /// Json value of the player pref.
        /// </summary>
        [SerializeField]
        private string _textValue;

        /// <summary>
        /// Creates a <see cref="PlayerPrefWrapper"/> from the provided data.
        /// </summary>
        /// <param name="isEncrypted">Whether the player pref should be encrypted.</param>
        /// <param name="isFavorite">Whether the player pref is set as a favorite.</param>
        /// <param name="fullDataTypeName">Full name of the datatype that the pref value has.</param>
        /// <param name="jsonValue">Json value of the player pref value.</param>
        public PlayerPrefWrapper(bool isEncrypted, bool isFavorite, string fullDataTypeName, string jsonValue)
        {
            _isFavorite = isFavorite;
            _isEncrypted = isEncrypted;
            _fullDataTypeName = fullDataTypeName;
            _textValue = jsonValue;
        }
    }
}
using System;
using System.Linq;

namespace DTT.PlayerPrefsEnhanced
{
    /// <summary>
    /// Class to represent a player prefs data.
    /// </summary>
    [Serializable]
    internal class PlayerPrefModel
    {
        /// <summary>
        /// Key value of the stored player pref.
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// True if values are encrypted.
        /// </summary>
        public bool IsEncrypted
        {
            get;
            set;
        }

        /// <summary>
        /// The player prefs value as a Json string.
        /// </summary>
        public string JsonValue
        {
            get;
            set;
        }

        /// <summary>
        /// Assigned <see cref="DataTypes"/> based on the player prefs values datatype name.
        /// </summary>
        public DataTypes DataType
        {
            get;
            set;
        }

        /// <summary>
        /// The full name of the player prefs value datatype.
        /// </summary>
        public string FullDataTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// The short name of the player prefs value datatype.
        /// </summary>
        public string ShortDataTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// True is favourite, false is not favourite.
        /// </summary>
        public bool Favourite
        {
            get;
            set;
        }

        /// <summary>
        /// Assigns the attribute vallues with the provided data.
        /// </summary>
        /// <param name="key">Key value of the stored player pref.</param>
        /// <param name="value">The player prefs value as a Json string.</param>
        /// <param name="fullDataTypeName">The full name of the player prefs value datatype.</param>
        /// <param name="dataType">Asigned <see cref="DataTypes"/> based on the player prefs datatype name.</param>
        public PlayerPrefModel(string key, bool isEncrypted, bool isFavourite, string value, string fullDataTypeName, DataTypes dataType)
        {
            Key = key;
            IsEncrypted = isEncrypted;
            JsonValue = value;
            FullDataTypeName = fullDataTypeName;
            ShortDataTypeName = FullDataTypeName.Split('.').Last();
            DataType = dataType;
            Favourite = isFavourite;
        }
    }
}
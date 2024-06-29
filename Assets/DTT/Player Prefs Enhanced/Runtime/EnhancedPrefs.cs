using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.CompilerServices;

#if UNITY_EDITOR
using UnityEditor;
#endif

[assembly: InternalsVisibleTo("DTT.PlayerPrefsEnhanced.Editor")]
[assembly: InternalsVisibleTo("DTT.PlayerPrefsEnhanced.Tests.Editor")]
namespace DTT.PlayerPrefsEnhanced
{
    /// <summary>
    /// Stores and accesses player preferences between game sessions.
    /// </summary>
    public static class EnhancedPrefs
    {
        /// <summary>
        /// The encryptor used to encrypt and decrypt the player prefs.
        /// </summary>
        private static IEncryptor<string, string> _encryptor = new EncryptorAES();

        /// <summary>
        /// Dictionary that links a datatypes full name to the <see cref="DataTypes"/>.
        /// Used as a look up table.
        /// </summary>
        private static Dictionary<string, DataTypes> _dataTypesDict = new Dictionary<string, DataTypes>()
        {
            { typeof(string).FullName, DataTypes.STRING },
            { typeof(float).FullName, DataTypes.FLOAT },
            { typeof(int).FullName, DataTypes.INT },
            { typeof(double).FullName, DataTypes.DOUBLE },
            { typeof(bool).FullName, DataTypes.BOOL },
            { typeof(long).FullName, DataTypes.LONG },
            { typeof(Vector2).FullName, DataTypes.VECTOR2 },
            { typeof(Vector3).FullName, DataTypes.VECTOR3 },
            { typeof(Vector4).FullName, DataTypes.VECTOR4 },
            { typeof(Quaternion).FullName, DataTypes.QUATERNION },
            { typeof(Color).FullName, DataTypes.COLOR }
        };

        /// <summary>
        /// Sets the value of the player preference identified by the key.
        /// </summary>
        /// <typeparam name="T">Datatype of the value being stored.</typeparam>
        /// <param name="key">The key the preference is identified by.</param>
        /// <param name="value">The value to be set.</param>
        public static void SetPlayerPref<T>(string key, T value) => SetPlayerPref(key, value, true);

        /// <summary>
        /// Sets the value of the player preference identified by the key.
        /// </summary>
        /// <typeparam name="T">Datatype of the value being stored.</typeparam>
        /// <param name="key">The key the preference is identified by.</param>
        /// <param name="value">The value to be set.</param>
        /// <param name="isEncrypted">False if you wish the pref no to be encrypted.</param>
        public static void SetPlayerPref<T>(string key, T value, bool isEncrypted)
        {
            bool isFavorite = false;

#if UNITY_EDITOR
            if (HasPlayerPref(key))
            {
                PlayerPrefWrapper wrapper = GetPlayerPrefWrapper(key);
                if (wrapper != null)
                    isFavorite = wrapper.IsFavorite;
            }
#endif

            SetPlayerPref(key, value, isEncrypted, isFavorite);
        }

        /// <summary>
        /// Sets the value of the player preference identified by the key.
        /// </summary>
        /// <typeparam name="T">Datatype of the value being stored.</typeparam>
        /// <param name="key">The key the preference is identified by.</param>
        /// <param name="value">The value to be set.</param>
        /// <param name="isEncrypted">False if you wish the pref no to be encrypted.</param>
        /// <param name="isFavorite">Whether the player pref should appear as a 'favorite' player pref.</param>
        public static void SetPlayerPref<T>(string key, T value, bool isEncrypted, bool isFavorite)
        {
            // Convert value into plaintext value.
            PlayerPrefValueContainer<T> playerPref = new PlayerPrefValueContainer<T>(value);
            string prefJsonValue = JsonUtility.ToJson(playerPref);

            SetJsonPlayerPref<T>(key, prefJsonValue, isEncrypted, isFavorite);
        }

        /// <summary>
        /// Sets the value of the player preference identified by the key.
        /// </summary>
        /// <typeparam name="T">Datatype of the value being stored.</typeparam>
        /// <param name="key">The key the preference is identified by.</param>
        /// <param name="jsonValue">The value to be set in json format.</param>
        /// <param name="isEncrypted">False if you wish the pref no to be encrypted.</param>
        /// <param name="isFavorite">Whether the player pref should appear as a 'favorite' player pref.</param>
        internal static void SetJsonPlayerPref<T>(string key, string jsonValue, bool isEncrypted, bool isFavorite)
        {
            string dataType = typeof(T).FullName;

            // Encrypt data type and plaintext value.
            if (isEncrypted)
            {
                jsonValue = _encryptor.Encrypt(jsonValue);
                jsonValue = _encryptor.Encrypt(jsonValue);
                dataType = _encryptor.Encrypt(dataType);
            }

            // Wrap it in a container holding the dadatype name.
            PlayerPrefWrapper prefContainer = new PlayerPrefWrapper(isEncrypted, isFavorite, dataType, jsonValue);
            string plainTextValue = JsonUtility.ToJson(prefContainer);

            // Convert pref Json value into value.
            string prefSerializationCheck = prefContainer.TextValue;
            if (isEncrypted)
            {
                prefSerializationCheck = _encryptor.Decrypt(prefSerializationCheck);
                prefSerializationCheck = _encryptor.Decrypt(prefSerializationCheck);
            }

            PlayerPrefValueContainer<T> playerPrefCheck = JsonUtility.FromJson<PlayerPrefValueContainer<T>>(prefSerializationCheck);

            if (playerPrefCheck.Value == null)
                throw new PlayerPrefsException($"Value could not be serialized properly.");

            // Store the Encrypted text.
            PlayerPrefs.SetString(key, plainTextValue);
            SavePlayerPrefs();
        }

        /// <summary>
        /// Sets the value of the player preference identified by the key.
        /// </summary>
        /// <param name="key">The key the preference is identified by.</param>
        /// <param name="jsonValue">The value to be set in json format.</param>
        /// <param name="isEncrypted">False if you wish the pref no to be encrypted.</param>
        /// <param name="isFavorite">Whether the player pref should appear as a 'favorite' player pref.</param>
        /// <param name="dataType">Name of the data type.</param>
        internal static void SetJsonPlayerPref(string key, string jsonValue, bool isEncrypted, bool isFavorite, string dataType)
        {
            // Encrypt data type and plaintext value.
            if (isEncrypted)
            {
                jsonValue = _encryptor.Encrypt(jsonValue);
                jsonValue = _encryptor.Encrypt(jsonValue);
                dataType = _encryptor.Encrypt(dataType);
            }

            // Wrap it in a container holding the dadatype name.
            PlayerPrefWrapper prefContainer = new PlayerPrefWrapper(isEncrypted, isFavorite, dataType, jsonValue);
            string plainTextValue = JsonUtility.ToJson(prefContainer);

            // Store the Encrypted text.
            PlayerPrefs.SetString(key, plainTextValue);
            SavePlayerPrefs();
        }

        /// <summary>
        /// Returns the value corresponding to the key in the player preference file if it exists.
        /// </summary>
        /// <typeparam name="T">Datatype of the value being requested.</typeparam>
        /// <param name="key">The key the preference is identified by.</param>
        /// <param name="defaultValue">The value returned if the provided key doesnt exist.</param>
        /// <returns>
        /// If key is found, returns the stored value associated with the provided key.
        /// If key not found, returns the d=provided default value.
        /// </returns>
        public static T GetPlayerPref<T>(string key, T defaultValue)
        {
            // Check if key exists.
            if (!HasPlayerPref(key))
                return defaultValue;

            // Convert plaintext into wrapper
            PlayerPrefWrapper prefWrapper = GetPlayerPrefWrapper(key);

            string prefJsonValue = prefWrapper.TextValue;
            string dataType = prefWrapper.FullDataTypeName;

            // Decrypt data type and pref json value..
            if (prefWrapper.IsEncrypted)
            {
                prefJsonValue = _encryptor.Decrypt(prefJsonValue);
                prefJsonValue = _encryptor.Decrypt(prefJsonValue);
                dataType = _encryptor.Decrypt(dataType);
            }

            string givenTypeName = typeof(T).FullName;

            if (!dataType.Equals(givenTypeName))
                throw new PlayerPrefsException($"Stored PlayerPref value is not of type {givenTypeName}.");

            // Convert pref Json value into value.
            PlayerPrefValueContainer<T> playerPref = JsonUtility.FromJson<PlayerPrefValueContainer<T>>(prefJsonValue);

            // Return playerpref value.
            return playerPref.Value;
        }

        /// <summary>
        /// Gets the <see cref="PlayerPrefWrapper"/> of the given key.
        /// </summary>
        /// <param name="key">Player pref key.</param>
        /// <returns>The <see cref="PlayerPrefWrapper"/> of the given key.</returns>
        internal static PlayerPrefWrapper GetPlayerPrefWrapper(string key)
        {
            // Get wrapper containing datatype and pref value in Json.
            string plainTextValue = PlayerPrefs.GetString(key);

            // Convert plaintext into wrapper
            return JsonUtility.FromJson<PlayerPrefWrapper>(plainTextValue);
        }

        /// <summary>
        /// Checks if the key exists in the stored player preferences.
        /// </summary>
        /// <param name="key">The key the preference is identified by.</param>
        /// <returns>True if the key exists in the preferences.</returns>
        public static bool HasPlayerPref(string key) => PlayerPrefs.HasKey(key);

        /// <summary>
        /// Writes all modified changes to disk.
        /// </summary>
        public static void SavePlayerPrefs() => PlayerPrefs.Save();

        /// <summary>
        /// Removes key and corresponding value from the player preferences.
        /// </summary>
        /// <param name="key">The key the preference is identified by.</param>
        public static void DeletePlayerPref(string key)
        {
            // Check if key exists.
            if (!HasPlayerPref(key))
                return;

            PlayerPrefs.DeleteKey(key);
        }

        /// <summary>
        /// Removes all keys and values from the player preferences.
        /// Use with caution!
        /// </summary>
        public static void DeleteAllPlayerPrefs() => PlayerPrefs.DeleteAll();

#if UNITY_EDITOR

        /// <summary>
        /// Removes all keys and values from the player preferences that are created using EnhancedPrefs.
        /// Use with caution!
        /// </summary>
        public static void DeleteAllEnhancedPlayerPrefs()
        {
            List<PlayerPrefModel> playerPrefs = GetAllPlayerPrefs();
            foreach (PlayerPrefModel playerPref in playerPrefs)
                DeletePlayerPref(playerPref.Key);
        }

        /// <summary>
        /// Retrieves all player preferences from the preference file and provides them in the <see cref="PlayerPrefModel"/> format.
        /// </summary>
        /// <returns>The list of player preferences in <see cref="PlayerPrefModel"/> format, exluding Unity default keys.</returns>
        internal static List<PlayerPrefModel> GetAllPlayerPrefs()
        {
            List<PlayerPrefModel> playerprefs = new List<PlayerPrefModel>();
            Dictionary<string, string> plainTextPrefs = new Dictionary<string, string>();

            string companyName = PlayerSettings.companyName;
            string productName = PlayerSettings.productName;
            // File location and name that store the player prefs differ based on platform and Unity version.
            if (Application.platform == RuntimePlatform.WindowsEditor)
                plainTextPrefs = GetWindowsPlayerPrefs(companyName, productName);
            else if (Application.platform == RuntimePlatform.OSXEditor)
                plainTextPrefs = GetMacOSPlayerPrefs(companyName, productName);
            else
                throw new NotSupportedException("This Unity Editor platform is not supported by PlayerPrefsEnhanced.");

            foreach (KeyValuePair<string, string> plainTextPref in plainTextPrefs)
            {
                PlayerPrefWrapper prefWrapper = null;

                // Convert jsonPrefWrapper into prefwrapper
                try
                {
                    prefWrapper = JsonUtility.FromJson<PlayerPrefWrapper>(plainTextPref.Value);
                    if (prefWrapper == null)
                        throw new PlayerPrefsException($"Value could not be serialized properly.");
                }
                catch (Exception)
                {
                    continue;
                }

                string prefJsonValue = prefWrapper.TextValue;
                string dataTypeName = prefWrapper.FullDataTypeName;

                if (dataTypeName == null)
                    continue;

                // Decrypt data type and pref json value..
                if (prefWrapper.IsEncrypted)
                {
                    prefJsonValue = _encryptor.Decrypt(prefJsonValue);
                    prefJsonValue = _encryptor.Decrypt(prefJsonValue);
                    dataTypeName = _encryptor.Decrypt(dataTypeName);
                }

                // Determine datatype based on name.
                DataTypes dataType;

                if (_dataTypesDict.ContainsKey(dataTypeName))
                    dataType = _dataTypesDict.FirstOrDefault(x => x.Key.Equals(dataTypeName)).Value;
                else
                    dataType = DataTypes.NOT_SUPPORTED;

                // Fill in model with pref data.
                PlayerPrefModel playerPref = new PlayerPrefModel(plainTextPref.Key, prefWrapper.IsEncrypted, prefWrapper.IsFavorite, prefJsonValue, dataTypeName, dataType);

                playerprefs.Add(playerPref);
            }

            return playerprefs;
        }

        /// <summary>
        /// Reads out the amount of player prefs from the player pref file.
        /// </summary>
        /// <returns>The amount of player prefs excluding the default ones.</returns>
        internal static int GetPlayerPrefAmount()
        {
            string companyName = PlayerSettings.companyName;
            string productName = PlayerSettings.productName;

            // File location and name that store the player prefs differ based on platform and Unity version.
            if (Application.platform == RuntimePlatform.WindowsEditor)
                return GetWindowsPlayerPrefAmount(companyName, productName);
            else if (Application.platform == RuntimePlatform.OSXEditor)
                return GetMacOSPlayerPrefAmount(companyName, productName);
            else
                throw new NotSupportedException("This Unity Editor platform is not supported by PlayerPrefsEnhanced.");
        }

        /// <summary>
        /// Read the amount of player preferences from the Windows registry.
        /// </summary>
        /// <param name="companyName">Company name stored in player settings.</param>
        /// <param name="productName">Product name stored in player settings.</param>
        /// <returns>The amount of player prefs excluding the default ones.</returns>
        private static int GetWindowsPlayerPrefAmount(string companyName, string productName)
        {
            int playerPrefAmount = 0;

#if UNITY_EDITOR_WIN

            string plistFilePath;

#if UNITY_5_5_OR_NEWER
            // On Windows, PlayerPrefs are stored in HKCU\Software\Unity\UnityEditor\ExampleCompanyName\ExampleProductName key.
            plistFilePath = string.Format("Software\\Unity\\UnityEditor\\" + companyName + "\\" + productName);
#else
            // Before Unity 5.5 editor player prefs were stored on another location.
            plistFilePath = string.Format("Software\\" + companyName + "\\" + productName);
#endif

            if (string.IsNullOrEmpty(plistFilePath))
                return playerPrefAmount;

#if NET_4_6
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(plistFilePath);

            if (registryKey == null)
                return playerPrefAmount;

            string[] registryValueNames = registryKey.GetValueNames();

            foreach (string registryValueName in registryValueNames)
                playerPrefAmount++;
#endif

#endif
            return playerPrefAmount;
        }

        /// <summary>
        /// Read the amount of player preferences from the iOS library.
        /// </summary>
        /// <param name="companyName">Company name stored in player settings.</param>
        /// <param name="productName">Product name stored in player settings.</param>
        /// <returns>The amount of player prefs excluding the default ones.</returns>
        private static int GetMacOSPlayerPrefAmount(string companyName, string productName)
        {
            int playerPrefAmount = 0;

#if UNITY_EDITOR_OSX

            //On macOS, PlayerPrefs are stored in /Library/Preferences/[bundle identifier].plist.
            string plistFilename = string.Format("unity.{0}.{1}.plist", companyName, productName);
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string playerPrefsPath = Path.Combine(Path.Combine(folderPath, "Library/Preferences"), plistFilename);

            if (!File.Exists(playerPrefsPath))
                return playerPrefAmount;

            // Parse the plist then cast it to a Dictionary
            object plist = Plist.readPlist(playerPrefsPath);

            Dictionary<string, object> parsedPlist = plist as Dictionary<string, object>;

            foreach (KeyValuePair<string, object> keyValuePair in parsedPlist)
                playerPrefAmount++;
#endif
            return playerPrefAmount;
        }

        /// <summary>
        /// Reading out the player preferences from the Windows registry.
        /// </summary>
        /// <param name="companyName">Company name stored in player settings.</param>
        /// <param name="productName">Product name stored in player settings.</param>
        /// <returns>A list of player preferences in <see cref="PlayerPrefModel"/> format, exluding Unity default keys.</returns>
        private static Dictionary<string, string> GetWindowsPlayerPrefs(string companyName, string productName)
        {
            Dictionary<string, string> plainTextPrefs = new Dictionary<string, string>();

#if UNITY_EDITOR_WIN

            string plistFilePath;

#if UNITY_5_5_OR_NEWER
            // On Windows, PlayerPrefs are stored in HKCU\Software\Unity\UnityEditor\ExampleCompanyName\ExampleProductName key.
            plistFilePath = string.Format("Software\\Unity\\UnityEditor\\" + companyName + "\\" + productName);
#else
            // Before Unity 5.5 editor player prefs were stored on another location.
            plistFilePath = string.Format("Software\\" + companyName + "\\" + productName);
#endif

            if (string.IsNullOrEmpty(plistFilePath))
                return plainTextPrefs;
#if NET_4_6
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(plistFilePath);

            if (registryKey == null)
                return plainTextPrefs;

            string[] registryValueNames = registryKey.GetValueNames();

            foreach (string registryValueName in registryValueNames)
            {
                // Remove the _xxxxxxxxx style suffix used on player pref keys in Windows registry
                int index = registryValueName.LastIndexOf("_");
                string key = registryValueName.Remove(index, registryValueName.Length - index);
                string jsonPrefWrapper = PlayerPrefs.GetString(key);

                plainTextPrefs.Add(key, jsonPrefWrapper);
            }
#endif

#endif
            return plainTextPrefs;
        }

        /// <summary>
        /// Reading out the player preferences from the iOS library.
        /// </summary>
        /// <param name="companyName">Company name stored in player settings.</param>
        /// <param name="productName">Product name stored in player settings.</param>
        /// <returns>A list of player preferences in <see cref="PlayerPrefModel"/> format, exluding Unity default keys.</returns>
        private static Dictionary<string, string> GetMacOSPlayerPrefs(string companyName, string productName)
        {
            Dictionary<string, string> plainTextPrefs = new Dictionary<string, string>();

#if UNITY_EDITOR_OSX

            //On macOS, PlayerPrefs are stored in /Library/Preferences/[bundle identifier].plist.
            string plistFilename = string.Format("unity.{0}.{1}.plist", companyName, productName);
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string playerPrefsPath = Path.Combine(Path.Combine(folderPath, "Library/Preferences"), plistFilename);

            if (!File.Exists(playerPrefsPath))
                return plainTextPrefs;

            // Parse the plist then cast it to a Dictionary
            object plist = Plist.readPlist(playerPrefsPath);

            Dictionary<string, object> parsedPlist = plist as Dictionary<string, object>;

            foreach (KeyValuePair<string, object> keyValuePair in parsedPlist)
            {
                // Exclude default player prefs made by Unity.
                string keyToLower = keyValuePair.Key.ToLower();
                if (keyToLower.Contains("unity.player") ||
                    keyToLower.Contains("unitygraphicsquality") ||
                    keyToLower.Contains("unity.cloud"))
                    continue;

                string key = keyValuePair.Key;
                string jsonPrefWrapper;

                // Convert object to string or skip.
                try
                {
                    jsonPrefWrapper = (string)keyValuePair.Value;
                }
                catch (Exception)
                {
                    continue;
                }

                plainTextPrefs.Add(key, jsonPrefWrapper);
                }
#endif
            return plainTextPrefs;
        }

#endif
    }
}
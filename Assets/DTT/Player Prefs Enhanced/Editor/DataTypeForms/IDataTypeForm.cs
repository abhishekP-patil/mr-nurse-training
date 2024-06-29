namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Interface used by an <see cref="AbstractPrefForm"/> to draw the content fitting a certain data type.
    /// </summary>
    internal interface IDataTypeForm
    {
        /// <summary>
        /// The player pref key used to get the stored player pref value.
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Set the default values to variable if the <see cref="PlayerPrefModel"/> is unknown or not needed.
        /// </summary>
        void SetKeyValue();

        /// <summary>
        /// Draws the content required to read the player pref data.
        /// </summary>
        void DrawReadingFields();

        /// <summary>
        /// Draws the content required to update the player pref data.
        /// </summary>
        void DrawUpdateFields();

        /// <summary>
        /// Draws the content required to create a new player pref with the same data type as the selected player pref.
        /// </summary>
        void DrawCreateFields();

        /// <summary>
        /// Saves the player pref with the current values.
        /// </summary>
        /// <param name="Encrypt">True if pref should be encrypted</param>
        void SavePlayerPref(bool Encrypt);

        /// <summary>
        /// Validates the current values that are getting saved.
        /// </summary>
        /// <returns>True if values are valid.</returns>
        bool NewPlayerPrefValidation();

        /// <summary>
        /// Draws warnings related to the player pref keys.
        /// </summary>
        void DrawKeyWarnings();
    }
}
namespace DTT.PlayerPrefsEnhanced
{
    /// <summary>
    /// interface used as a base for Encryptor classes.
    /// </summary>
    public interface IEncryptor<TFrom, TTo>
    {
        /// <summary>
        /// Initialize the crypto service provider settings.
        /// </summary>
        void InitializeServiceProvider();

        /// <summary>
        /// Encrypts a given string.
        /// </summary>
        /// <param name="data">String to encrypt.</param>
        /// <returns>The encrypted string value.</returns>
        TTo Encrypt(TFrom data);

        /// <summary>
        /// Decrypts a given string.
        /// </summary>
        /// <param name="data">String to decrypt.</param>
        /// <returns>The decrypted string value.</returns>
        TFrom Decrypt(TTo data);
    }
}
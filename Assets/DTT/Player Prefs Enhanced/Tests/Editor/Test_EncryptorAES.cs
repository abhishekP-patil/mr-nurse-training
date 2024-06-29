#if TEST_FRAMEWORK

using NUnit.Framework;

namespace DTT.PlayerPrefsEnhanced.Tests.Runtime
{
    /// <summary>
    /// Class used to test the <see cref="EncryptorAES"/> functions.
    /// </summary>
    public class Test_EncryptorAES
    {
#region Test_EncryptString

        /// <summary>
        /// Expects the value of the string to change after encryption.
        /// </summary>
        [Test]
        public void Test_EncryptString_StringShouldBeAlteredAfterEncryption()
        {
            // Arrange.
            IEncryptor<string,string> encryptor = new EncryptorAES();
            string beforeEncryption = "Test_EncryptString_StringShouldBeAlteredAfterEncryption";
            string afterEncryption = beforeEncryption;

            // Act.
            afterEncryption = encryptor.Encrypt(afterEncryption);

            // Assert.
            Assert.IsFalse(afterEncryption.Equals(beforeEncryption), "String didn't change after encryption.");
        }

        /// <summary>
        /// Expects the value of the string to change everytime its encrypted.
        /// </summary>
        [Test]
        public void Test_EncryptString_StringShouldBeAlteredAfterMultiEncryption()
        {
            // Arrange.
            IEncryptor<string, string> encryptor = new EncryptorAES();
            string beforeEncryption = "Test_EncryptString_StringShouldBeAlteredAfterMultiEncryption";
            string afterOneTimeEncryption;
            string afterTwoTimeEncryption;

            // Act
            afterOneTimeEncryption = encryptor.Encrypt(beforeEncryption);

            afterTwoTimeEncryption = afterOneTimeEncryption;

            afterTwoTimeEncryption = encryptor.Encrypt(afterOneTimeEncryption);

            // Assert.
            Assert.IsFalse(beforeEncryption.Equals(afterTwoTimeEncryption), "Sting is back to it original value after a second encryptiion.");
            Assert.IsFalse(afterTwoTimeEncryption.Equals(afterOneTimeEncryption), "String didn't change after encrypting again.");
        }

        /// <summary>
        /// Expects the encryption of identical nearly identical strings to not be nearly identical.
        /// </summary>
        [Test]
        public void Test_EncryptString_IdenticalStringsShouldHaveDifferentEncryption()
        {
            // Arrange.
            IEncryptor<string, string> encryptor = new EncryptorAES();

            string beforeEncryptionOne = "Test_EncryptString_IdenticalStringsShouldHaveDifferentEncryption" + 1;
            string beforeEncryptionTwo = "Test_EncryptString_IdenticalStringsShouldHaveDifferentEncryption" + 2;

            string afterEncryptionOne;
            string afterEncryptionTwo;

            string firstHaldEncryptionOne;
            string firstHaldEncryptionTwo;

            // Act.
            afterEncryptionOne = encryptor.Encrypt(beforeEncryptionOne);
            afterEncryptionTwo = encryptor.Encrypt(beforeEncryptionTwo);

            firstHaldEncryptionOne = afterEncryptionOne.Substring(0, afterEncryptionOne.Length / 2);
            firstHaldEncryptionTwo = afterEncryptionTwo.Substring(0, afterEncryptionTwo.Length / 2);

            // Assert.
            Assert.False(afterEncryptionOne.Equals(afterEncryptionTwo), "Encryption strings are the same.");
        }

#endregion

#region Test_DecryptString

        /// <summary>
        /// Expects the value of the string to be the same after encryption and decryption.
        /// </summary>
        [Test]
        public void Test_DecryptString_StringShouldBeEqualAfterEncryptionAndDecryption()
        {
            // Arrange.
            IEncryptor<string, string> encryptor = new EncryptorAES();
            string beforeEncryption = "Test_DecryptString_StringShouldBeTheSameAfterEncryptionAndDecryption";
            string afterEncryption = beforeEncryption;

            // Act.
            afterEncryption = encryptor.Encrypt(afterEncryption);

            afterEncryption = encryptor.Decrypt(afterEncryption);

            // Assert.
            Assert.IsTrue(afterEncryption.Equals(beforeEncryption), "String is not the same after decryption.");
        }

        /// <summary>
        /// Expects the value of the string to be the same after encryption and decryption it for the same amount of times.
        /// </summary>
        [Test]
        public void Test_DecryptString_StringShouldBeEqualAfterMultiEncryptionAndDecryption()
        {
            // Arrange.
            IEncryptor<string, string> encryptor = new EncryptorAES();
            string beforeEncryption = "Test_DecryptString_StringShouldBeTheSameAfterMultiEncryptionAndDecryption";
            string afterEncryption = beforeEncryption;

            // Act.
            afterEncryption = encryptor.Encrypt(afterEncryption);
            afterEncryption = encryptor.Encrypt(afterEncryption);

            afterEncryption = encryptor.Decrypt(afterEncryption);
            afterEncryption = encryptor.Decrypt(afterEncryption);

            // Assert.
            Assert.IsTrue(afterEncryption.Equals(beforeEncryption), "String is not the same after decryption.");
        }

#endregion
    }
}

#endif
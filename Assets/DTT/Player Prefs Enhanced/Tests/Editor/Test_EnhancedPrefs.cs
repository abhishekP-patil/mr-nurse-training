#if TEST_FRAMEWORK

using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace DTT.PlayerPrefsEnhanced.Tests.Runtime
{
    /// <summary>
    /// Class used to test the <see cref="EnhancedPrefs"/>
    /// </summary>
    public class Test_EnhancedPrefs
    {
#region Test_GetPlayerPrefAmount

        /// <summary>
        /// Expects GetPlayerPrefAmount to return 0 if all prefs are deleted.
        /// </summary>
        [Test]
        public void Test_GetPlayerPrefAmount_AfterDeletingAllPrefsShouldBeZero()
        {
            //Arrange.
            PlayerPrefs.DeleteAll();
            int amountOfPlayerPrefs;

            //Act.
            amountOfPlayerPrefs = EnhancedPrefs.GetPlayerPrefAmount();

            //Assert.
            Assert.Zero(amountOfPlayerPrefs);
        }

        /// <summary>
        /// Expects GetPlayerPrefAmount to not return 0 if prefs have been added.
        /// </summary>
        [Test]
        public void Test_GetPlayerPrefAmount_AfterAddingPrefsShouldNotBeZero()
        {
            //Arrange.
            PlayerPrefs.DeleteAll();
            int amountOfPlayerPrefs = 0;

            // To avoid duplicate keys while testing the key should include as the metehod name.
            string testPrefKey = "Test_GetPlayerPrefAmount_AfterAddingPrefsShouldNotBeZero";

            //Act.
            PlayerPrefs.SetInt(testPrefKey, amountOfPlayerPrefs);
            amountOfPlayerPrefs = EnhancedPrefs.GetPlayerPrefAmount();

            //Assert.
            Assert.NotZero(amountOfPlayerPrefs);
        }

        /// <summary>
        /// Expects GetPlayerPrefAmount to return the same amount as the total amount of prefs that have been added.
        /// </summary>
        [Test]
        public void Test_GetPlayerPrefAmount_ShouldBeTheExactAmountOfTotalAddedPrefs()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();

            // To avoid duplicate keys while testing the key should include the metehod name.
            string testPrefKey = "Test_GetPlayerPrefAmount_ShouldBeTheExactAmountOfTotalAddedPrefs";

            int amountOfPlayerPrefs = 0;
            int amountOfPrefsToAdd = 3;

            // Act.
            for (int i = 0; i < amountOfPrefsToAdd; i++)
            {
                PlayerPrefs.SetInt($"{testPrefKey} - {i + 1}", i + 1);
            }

            amountOfPlayerPrefs = EnhancedPrefs.GetPlayerPrefAmount();

            // Assert.
            Assert.True(amountOfPlayerPrefs == amountOfPrefsToAdd);
        }

#endregion

#region Test_DeleteAllPlayerPrefs

        /// <summary>
        /// Expects the amount of player prefs to be zero after deleting all player prefs.
        /// </summary>
        [Test]
        public void Test_DeleteAllPlayerPrefs_AfterDeletePlayerPrefAmountIsZero()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();

            // To avoid duplicate keys while testing the key should include the metehod name.
            string testPrefKey = "Test_DeleteAllPlayerPrefs_AfterDeletePlayerPrefAmountIsZero";

            int amountOfPlayerPrefsBefore;
            int amountOfPlayerPrefsAfter;
            int amountOfPrefsToAdd = 3;

            // Act.
            for (int i = 0; i < amountOfPrefsToAdd; i++)
                PlayerPrefs.SetInt($"{testPrefKey} - {i + 1}", i);

            amountOfPlayerPrefsBefore = EnhancedPrefs.GetPlayerPrefAmount();

            EnhancedPrefs.DeleteAllPlayerPrefs();

            amountOfPlayerPrefsAfter = EnhancedPrefs.GetPlayerPrefAmount();

            // Assert.
            Assert.NotZero(amountOfPlayerPrefsBefore);
            Assert.Less(amountOfPlayerPrefsAfter, amountOfPlayerPrefsBefore);
            Assert.Zero(amountOfPlayerPrefsAfter);
        }

#endregion

#region Test_DeletePlayerPref

        /// <summary>
        /// Expects no prefs to be deleted if the key doesn't exist.
        /// </summary>
        [Test]
        public void Test_DeletePlayerPref_NothingDeletedWhenKeyNotFound()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();
            IEncryptor<string, string> encryptor = new EncryptorAES();

            // To avoid duplicate keys while testing the key should include the metehod name.
            string testPrefKey = "Test_DeletePlayerPref_NothingDeletedWhenKeyNotFound_Existing";
            string notExistingPrefKey = "Test_DeletePlayerPref_NothingDeletedWhenKeyNotFound_NotExistingPrefKey";

            bool testPrefExistsBefore;
            bool testPrefExistsAfter;

            int amountOfPrefsBefore;
            int amountOfPrefsAfter;

            // Act.
            PlayerPrefs.SetInt(testPrefKey, 0);

            testPrefExistsBefore = PlayerPrefs.HasKey(testPrefKey);
            amountOfPrefsBefore = EnhancedPrefs.GetPlayerPrefAmount();

            EnhancedPrefs.DeletePlayerPref(notExistingPrefKey);

            testPrefExistsAfter = PlayerPrefs.HasKey(testPrefKey);
            amountOfPrefsAfter = EnhancedPrefs.GetPlayerPrefAmount();

            // Assert.
            Assert.True(testPrefExistsBefore);
            Assert.True(testPrefExistsAfter);
            Assert.True(amountOfPrefsBefore == amountOfPrefsAfter);
        }

        /// <summary>
        /// Expects only the pref with the same key to be deleted.
        /// </summary>
        [Test]
        public void Test_DeletePlayerPref_OnlyDeletePrefWithThisKey()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();
            IEncryptor<string, string> encryptor = new EncryptorAES();

            // To avoid duplicate keys while testing the key should include the metehod name.
            string testPrefKeyToBeDeleted = "Test_DeletePlayerPref_OnlyDeletePrefWithThisKey_Delete";
            string testPrefKeyToRemain = "Test_DeletePlayerPref_OnlyDeletePrefWithThisKey_Remain";

            bool testPrefKeyToBeDeletedExistsBefore;
            bool testPrefKeyToRemainExistsBefore;

            bool testPrefKeyToBeDeletedExistsAfter;
            bool testPrefKeyToRemainExistsAfter;

            // Act.
            PlayerPrefs.SetInt(testPrefKeyToBeDeleted, 0);
            PlayerPrefs.SetInt(testPrefKeyToRemain, 1);

            testPrefKeyToBeDeletedExistsBefore = PlayerPrefs.HasKey(testPrefKeyToBeDeleted);
            testPrefKeyToRemainExistsBefore = PlayerPrefs.HasKey(testPrefKeyToRemain);

            EnhancedPrefs.DeletePlayerPref(testPrefKeyToBeDeleted);

            testPrefKeyToBeDeletedExistsAfter = PlayerPrefs.HasKey(testPrefKeyToBeDeleted);
            testPrefKeyToRemainExistsAfter = PlayerPrefs.HasKey(testPrefKeyToRemain);

            // Assert.
            Assert.True(testPrefKeyToBeDeletedExistsBefore);
            Assert.True(testPrefKeyToRemainExistsBefore);

            Assert.False(testPrefKeyToBeDeletedExistsAfter);
            Assert.True(testPrefKeyToBeDeletedExistsBefore);
        }

        /// <summary>
        /// Expects the amount of prefs to decrease by the amount of deleted prefs.
        /// </summary>
        [Test]
        public void Test_DeletePlayerPref_PrefAmountDecreaseByAmountOfDeletedPrefs()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();
            IEncryptor<string, string> encryptor = new EncryptorAES();

            // To avoid duplicate keys while testing the key should include the metehod name.
            string testPrefKey = "Test_DeletePlayerPref_PrefAmountDecreaseByAmountOfDeletedPrefs";
            string[] testprefkeys;

            int amountOfPrefsToAdd = 6;
            int amountOfPrefsToDelete = 3;

            int amountOfPlayerPrefsBefore;
            int amountOfPlayerPrefsAfter;
            int differenceInAmountOfPrefs;

            // Act.
            testprefkeys = new string[amountOfPrefsToAdd];

            for (int i = 0; i < amountOfPrefsToAdd; i++)
            {
                testprefkeys[i] = $"{testPrefKey}_{i + 1}";

                PlayerPrefs.SetInt(testprefkeys[i], i);
            }

            amountOfPlayerPrefsBefore = EnhancedPrefs.GetPlayerPrefAmount();

            for (int i = 0; i < amountOfPrefsToDelete; i++)
                EnhancedPrefs.DeletePlayerPref(testprefkeys[i]);

            amountOfPlayerPrefsAfter = EnhancedPrefs.GetPlayerPrefAmount();

            differenceInAmountOfPrefs = amountOfPlayerPrefsBefore - amountOfPlayerPrefsAfter;

            // Assert.
            Assert.NotZero(amountOfPlayerPrefsBefore);
            Assert.NotZero(amountOfPlayerPrefsAfter);
            Assert.Less(amountOfPlayerPrefsAfter, amountOfPlayerPrefsBefore);
            Assert.True(amountOfPrefsToDelete == differenceInAmountOfPrefs);
        }

#endregion

#region Test_HasPlayerPref

        /// <summary>
        /// Expects HasPlayerPref to return true if the pref key is in the player pref file.
        /// </summary>
        [Test]
        public void Test_HasPlayerPref_PrefExists()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();
            IEncryptor<string, string> encryptor = new EncryptorAES();

            // To avoid duplicate keys while testing the key should include the metehod name.
            string testPrefKey = "Test_HasPlayerPref_PrefExists";

            bool hasPlayerPref;

            // Act.
            PlayerPrefs.SetString(testPrefKey, "You have me.");

            hasPlayerPref = EnhancedPrefs.HasPlayerPref(testPrefKey);

            // Assert.
            Assert.True(hasPlayerPref);
        }

        /// <summary>
        /// Expects HasPlayerPref to return false if the pref key is not in the player pref file.
        /// </summary>
        [Test]
        public void Test_HasPlayerPref_PrefDoesNotExists()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();

            // To avoid duplicate keys while testing the key should include the metehod name.
            string testPrefKey = "Test_HasPlayerPref_PrefDoesNotExists";

            bool hasPlayerPref;

            // Act.
            hasPlayerPref = EnhancedPrefs.HasPlayerPref(testPrefKey);

            // Assert.
            Assert.False(hasPlayerPref);
        }

#endregion

#region Test_SetPlayerPref

        /// <summary>
        /// Expects the amount of prefs to go up by 1 after a new player pref is added.
        /// </summary>
        [Test]
        public void Test_SetPlayerPref_PrefCountGoesUpByOneAfterAddingNewPref()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();

            // To avoid duplicate keys while testing the key should be the same as the metehod name.
            string testPrefKey = "Test_SetPlayerPref_PrefCountGoesUpByOneAfterAddingNewPref";

            int amountOfPrefsBefore;
            int amountOfPrefsAfter;
            int amountDifferenceBeforeAfter;
            int amountOfPrefsAdded = 1;

            // Act.
            amountOfPrefsBefore = EnhancedPrefs.GetPlayerPrefAmount();

            EnhancedPrefs.SetPlayerPref(testPrefKey, amountOfPrefsBefore);

            amountOfPrefsAfter = EnhancedPrefs.GetPlayerPrefAmount();

            amountDifferenceBeforeAfter = amountOfPrefsAfter - amountOfPrefsBefore;

            // Assert.
            Assert.Less(amountOfPrefsBefore, amountOfPrefsAfter);
            Assert.IsTrue(amountDifferenceBeforeAfter == amountOfPrefsAdded);
        }

        /// <summary>
        /// Expects the amount of prefs to stay the same after setting an existing player pref.
        /// </summary>
        [Test]
        public void Test_SetPlayerPref_PrefCountStaysTheSameAfterSettingExistingPlayerPref()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();

            // To avoid duplicate keys while testing the key should be the same as the metehod name.
            string testPrefKey = "Test_SetPlayerPref_PrefCountStaysTheSameAfterSettingExistingPlayerPref";

            int amountOfPrefsBefore;
            int amountOfPrefsAfter;

            // Act.
            EnhancedPrefs.SetPlayerPref(testPrefKey, "hi");

            amountOfPrefsBefore = EnhancedPrefs.GetPlayerPrefAmount();

            EnhancedPrefs.SetPlayerPref(testPrefKey, amountOfPrefsBefore);

            amountOfPrefsAfter = EnhancedPrefs.GetPlayerPrefAmount();

            // Assert.
            Assert.True(amountOfPrefsBefore == amountOfPrefsAfter);
        }

        /// <summary>
        /// Expects the stored value of a pref to have changed after setting it again with a different value.
        /// </summary>
        [Test]
        public void Test_SetPlayerPref_SettingAnExistingPrefShouldChangeStoredValue()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();
            IEncryptor<string, string> encryptor = new EncryptorAES();

            // To avoid duplicate keys while testing the key should be the same as the metehod name.
            string testPrefKey = "Test_SetPlayerPref_SettingAnExistingPrefShouldChangeStoredValue";

            // Inputs should not be empty to make the test more reliable.
            string firstPrefInputValue = "First Value.";
            string secondPrefInputValue = "Second Value.";

            string firstStoredPrefValue;
            string secondStoredPrefValue;

            // Act.
            EnhancedPrefs.SetPlayerPref(testPrefKey, firstPrefInputValue);

            firstStoredPrefValue = PlayerPrefs.GetString(testPrefKey);

            EnhancedPrefs.SetPlayerPref(testPrefKey, secondPrefInputValue);

            secondStoredPrefValue = PlayerPrefs.GetString(testPrefKey);

            // Assert.
            Assert.AreNotEqual(firstPrefInputValue, secondPrefInputValue, "Inputs should be different to result in different stored values.");
            Assert.AreNotEqual(firstStoredPrefValue, secondStoredPrefValue, "Stored values are the same when they should not be.");
        }

        /// <summary>
        /// Expects a new player pref to be set for a custom serializable class.
        /// </summary>
        [Test]
        public void Test_SetPlayerPref_AbleToAddNoneStandardSerializableClass()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();

            // To avoid duplicate keys while testing the key should be the same as the metehod name.
            string testPrefKey = "Test_SetPlayerPref_AbleToAddNoneStandardSerializableClass";

            // Represent an user made class that is serializable.
            SerializableTestClass initialValue = new SerializableTestClass("TestClass", 3);
            SerializableTestClass defaultValue = new SerializableTestClass("default", 0);
            SerializableTestClass storedPrefValue;

            // Act.
            EnhancedPrefs.SetPlayerPref(testPrefKey, initialValue);

            storedPrefValue = EnhancedPrefs.GetPlayerPref<SerializableTestClass>(testPrefKey, defaultValue);

            // Assert.
            Assert.AreNotSame(defaultValue, storedPrefValue, "Default value returned, pref didn't get stored.");
            Assert.IsInstanceOf<SerializableTestClass>(storedPrefValue, "Stored value has different type.");

            Assert.AreNotEqual(initialValue, storedPrefValue, "Initial value and stored value are the same reference.");
            Assert.True(initialValue.Id == storedPrefValue.Id, "Value of stored object doesn't equal initial value.");
            Assert.True(initialValue.Name.Equals(storedPrefValue.Name), "Value of stored object doesn't equal initial value.");
            Assert.True(initialValue.AmountOfPreperties == storedPrefValue.AmountOfPreperties, "Value of stored object doesn't equal initial value.");

            Assert.True(storedPrefValue.Id < defaultValue.Id, "Id is the same as default value.");
        }

        /// <summary>
        /// Expects a new player pref to not be set for a custom unserializable class.
        /// </summary>
        [Test]
        public void Test_SetPlayerPref_NotAbleToAddNoneStandardUnserializableClass()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();

            // To avoid duplicate keys while testing the key should be the same as the metehod name.
            string testPrefKey = "Test_SetPlayerPref_NotAbleToAddNoneStandardUnserializableClass";

            // Represent an user made class that is unserializable.
            UnserializableTestClass initialValue = new UnserializableTestClass(5, 5);

            // Act.
            initialValue.FillArray();

            // Assert.
            Assert.Throws<PlayerPrefsException>(() => EnhancedPrefs.SetPlayerPref(testPrefKey, initialValue),
                                                  "Exception expected but not thrown.");
        }

#endregion

#region Test_GetPlayerPref

        /// <summary>
        /// Expects the amount of player prefs not to alter after getting a player pref.
        /// </summary>
        [Test]
        public void Test_GetPlayerPref_AmountOfPrefsStaysTheSameAfterGettingPref()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();

            // To avoid duplicate keys while testing the key should be the same as the metehod name.
            string testPrefKey = "Test_GetPlayerPref_AmountOfPrefsStaysTheSameAfterGettingPref";
            string testPrefValue = "Test Value.";

            int amountOfPrefsBefore;
            int amountOfPrefsAfter;

            // Act.
            EnhancedPrefs.SetPlayerPref(testPrefKey, testPrefValue);

            amountOfPrefsBefore = EnhancedPrefs.GetPlayerPrefAmount();

            EnhancedPrefs.GetPlayerPref<string>(testPrefKey, testPrefValue);

            amountOfPrefsAfter = EnhancedPrefs.GetPlayerPrefAmount();

            // Assert.
            Assert.True(amountOfPrefsBefore == amountOfPrefsAfter);

        }

        /// <summary>
        /// Expects the value from GetPlayerPref to be the same as the value that was set.
        /// </summary>
        [Test]
        public void Test_GetPlayerPref_ShouldGetTheSameValueAsTheValueBeingSet()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();

            // To avoid duplicate keys while testing the key should be the same as the metehod name.
            string testPrefKey = "Test_GetPlayerPref_ShouldGetTheSameValueAsTheValueBeingSet";

            string setValue = "Is this the same value?";
            string getValue;

            // Act.

            EnhancedPrefs.SetPlayerPref(testPrefKey, setValue);

            getValue = EnhancedPrefs.GetPlayerPref<string>(testPrefKey, "Another value as default.");

            // Assert.
            Assert.True(setValue.Equals(getValue));
        }

        /// <summary>
        /// Expects the value from GetPlayerPref to be a default value when the key doesn't exist in the pref list.
        /// </summary>
        [Test]
        public void Test_GetPlayerPref_GetDefaultValueIfKeyDoesnNotExist()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();

            // To avoid duplicate keys while testing the key should be the same as the metehod name.
            string testPrefKey = "Test_GetPlayerPref_GetDefaultValueIfKeyDoesnNotExist";

            // default string value.
            string defaultStringValue = "Default";
            string getStringValue;

            // default int value.
            int defaultIntValue = 10;
            int getIntValue;

            // Act.
            getStringValue = EnhancedPrefs.GetPlayerPref<string>(testPrefKey, defaultStringValue);
            getIntValue = EnhancedPrefs.GetPlayerPref<int>(testPrefKey, defaultIntValue);

            // Assert.
            Assert.True(defaultStringValue == getStringValue, "Vallue differs from default value : string.");
            Assert.True(defaultIntValue == getIntValue, "Vallue differs from default value : int.");
        }

        /// <summary>
        /// Expects <see cref="EnhancedPrefs.GetAllPlayerPrefs"/> to throw an exception if a different datatype is used than that is stored.
        /// </summary>
        [Test]
        public void Test_GetPlayerPref_ExceptionWhenGetValueDataTypeDiffersFromActualVallueDataType()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();

            // To avoid duplicate keys while testing the key should be the same as the metehod name.
            string testPrefKey = "Test_GetPlayerPref_ExceptionWhenGetValueDataTypeDiffersFromActualVallueDataType";
            string setPrefValue = "String value";

            // Act.
            EnhancedPrefs.SetPlayerPref(testPrefKey, setPrefValue);

            // Assert.
            Assert.Throws<PlayerPrefsException>(() => EnhancedPrefs.GetPlayerPref<int>(testPrefKey, 0), "Exception expected but not thrown.");
        }

#endregion

#region Test_GetAllPlayerPrefs

        /// <summary>
        /// Expects <see cref="EnhancedPrefs.GetAllPlayerPrefs"/> to return an equal amount of prefs as the total amount of prefs.
        /// </summary>
        [Test]
        public void Test_GetAllPlayerPrefs_ListCountShouldBeEqualToPrefAmount()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();

            // To avoid duplicate keys while testing the key should be the same as the metehod name.
            string basePrefKey = "Test_GetAllPlayerPrefs_ListCountShouldBeEqualToPrefAmount";
            string basePrefValue = "Value";

            int amountOfPrefsToMake = 10;
            int totalAmountofPrefs;
            int amountOfPrefsInList;

            List<PlayerPrefModel> prefModelList = new List<PlayerPrefModel>();

            // Act.
            for (int i = 0; i < amountOfPrefsToMake; i++)
                EnhancedPrefs.SetPlayerPref($"{basePrefKey} {i}", $"{basePrefValue} {i}");

            totalAmountofPrefs = EnhancedPrefs.GetPlayerPrefAmount();

            prefModelList = EnhancedPrefs.GetAllPlayerPrefs();

            amountOfPrefsInList = prefModelList.Count;

            // Assert.
            Assert.True(totalAmountofPrefs == amountOfPrefsToMake, "Total amount of prefs and the amount to make are not equal.");
            Assert.True(totalAmountofPrefs == amountOfPrefsInList, "Total amount of prefs in the list and amount of prefs stored are not equal.");
        }

        /// <summary>
        /// Expects the list to contain <see cref="PlayerPrefModel"/>s with existing keys.
        /// </summary>
        [Test]
        public void Test_GetAllPlayerPrefs_KeysInListExist()
        {
            // Arrange.
            PlayerPrefs.DeleteAll();

            // To avoid duplicate keys while testing the key should be the same as the metehod name.
            string basePrefKey = "Test_GetAllPlayerPrefs_KeysInListExist";
            string basePrefValue = "Value";

            int amountOfPrefsToMake = 10;
            int amountOfPrefsInList;
            int amountOfKeysThatDoNotExisist = 0;

            bool allKeysExist = true;

            List<PlayerPrefModel> prefModelList = new List<PlayerPrefModel>();

            // Act.
            for (int i = 0; i < amountOfPrefsToMake; i++)
                EnhancedPrefs.SetPlayerPref($"{basePrefKey} {i}", $"{basePrefValue} {i}");

            prefModelList = EnhancedPrefs.GetAllPlayerPrefs();
            amountOfPrefsInList = prefModelList.Count;

            foreach (PlayerPrefModel prefModel in prefModelList)
                if (!EnhancedPrefs.HasPlayerPref(prefModel.Key))
                {
                    allKeysExist = false;
                    amountOfKeysThatDoNotExisist++;
                }

            // Assert.
            Assert.NotZero(amountOfPrefsInList, "No prefs in list.");
            Assert.True(allKeysExist, $"Not all keys exist, {amountOfKeysThatDoNotExisist} out of {amountOfPrefsInList} do not exist.");
        }

#endregion

        /// <summary>
        /// Deletes any left over test player prefs.
        /// </summary>
        [OneTimeTearDown]
        public void Cleanup() => PlayerPrefs.DeleteAll();
    }
}

#endif
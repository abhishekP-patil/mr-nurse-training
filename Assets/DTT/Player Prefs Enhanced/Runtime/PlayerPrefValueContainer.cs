using System;
using UnityEngine;

namespace DTT.PlayerPrefsEnhanced
{
    /// <summary>
    /// Stores the value of an playerpref in a class to make it serializable.
    /// </summary>
    /// <typeparam name="T">DataType of the data contained within the object.</typeparam>
    [Serializable]
    internal class PlayerPrefValueContainer<T>
    {
        /// <summary>
        /// Data value contained within the object.
        /// </summary>
        public T Value => _value;

        /// <summary>
        /// Data value contained within the object.
        /// </summary>
        [SerializeField]
        private T _value;

        /// <summary>
        /// Constructor to store the data in the object.
        /// </summary>
        /// <param name="input">The data to be stored.</param>
        public PlayerPrefValueContainer(T input) => _value = input;
    }
}
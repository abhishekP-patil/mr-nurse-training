#if TEST_FRAMEWORK

using System;
using UnityEngine;

namespace DTT.PlayerPrefsEnhanced.Tests.Runtime
{
    /// <summary>
    /// Class to represent a user made serializable class.
    /// </summary>
    [Serializable]
    public class SerializableTestClass
    {
        /// <summary>
        /// Id of this data model.
        /// </summary>
        [SerializeField]
        private static int _idCount;

        /// <summary>
        /// Id of this data model.
        /// </summary>
        public int Id => _id;

        /// <summary>
        /// Id of this data model.
        /// </summary>
        [SerializeField]
        private int _id;

        /// <summary>
        /// Name of the serializableClass.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Name of the serializableClass.
        /// </summary>
        [SerializeField]
        private string _name;

        /// <summary>
        /// The amount of properties the class has.
        /// </summary>
        public int AmountOfPreperties => _amountOfPreperties;

        /// <summary>
        /// The amount of properties the class has.
        /// </summary>
        [SerializeField]
        private int _amountOfPreperties;

        public SerializableTestClass(string name, int amountOfProperties)
        {
            _id = _idCount;
            _name = name;
            _amountOfPreperties = amountOfProperties;
            
            _idCount++;
        }
    }
}

#endif
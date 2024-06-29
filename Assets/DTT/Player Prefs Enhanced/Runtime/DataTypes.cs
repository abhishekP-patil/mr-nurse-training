using System;
using UnityEngine;

namespace DTT.PlayerPrefsEnhanced
{
    /// <summary>
    /// Enum with all datatypes supporetd for the <see cref="EnhancedPrefs"/> editor tool.
    /// </summary>
    public enum DataTypes
    {
        /// <summary>
        /// Represents unsupported datatypes.
        /// </summary>
        [InspectorName("Not Supported")]
        NOT_SUPPORTED = -1,

        /// <summary>
        /// Represents string datatype.
        /// </summary>
        [InspectorName("string")]
        STRING = 0,

        /// <summary>
        /// Represents float datatype.
        /// </summary>
        [InspectorName("float")]
        FLOAT = 1,

        /// <summary>
        /// Represents int datatype.
        /// </summary>
        [InspectorName("int")]
        INT = 2,

        /// <summary>
        /// Represents bool datatype.
        /// </summary>
        [InspectorName("bool")]
        BOOL = 3,

        /// <summary>
        /// Represents ling datatype.
        /// </summary>
        [InspectorName("long")]
        LONG = 4,

        /// <summary>
        /// Represents vector2 datatype.
        /// </summary>
        [InspectorName("Vector2")]
        VECTOR2 = 5,

        /// <summary>
        /// Represents vector3 datatype.
        /// </summary>
        [InspectorName("Vector3")]
        VECTOR3 = 6,

        /// <summary>
        /// Represents vector4 datatype.
        /// </summary>
        [InspectorName("Vector4")]
        VECTOR4 = 7,

        /// <summary>
        /// Represents quaternion datatype.
        /// </summary>
        [InspectorName("Quaternion")]
        QUATERNION = 8,

        /// <summary>
        /// Represents color datatype.
        /// </summary>
        [InspectorName("Color")]
        COLOR = 9,

        /// <summary>
        /// Represents double datatype.
        /// </summary>
        [InspectorName("Double")]
        DOUBLE = 10,
    }
}
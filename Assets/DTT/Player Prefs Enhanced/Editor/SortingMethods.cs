using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Methods of sorting.
    /// </summary>
    internal enum SortingMethods
    {
        /// <summary>
        /// List won't be sorted.
        /// </summary>
        [InspectorName("No sorting.")]
        NONE = 0,

        /// <summary>
        /// Sorted alphabetically from a to z.
        /// </summary>
        [InspectorName("Alphabetical A - Z")]
        A_TO_Z = 1,

        /// <summary>
        /// Sorted alphabetically from a to z.
        /// </summary>
        [InspectorName("Alphabetical Z - A")]
        Z_TO_A = 2,

        /// <summary>
        /// Sorted on datatypes.
        /// </summary>
        [InspectorName("Sorted on datatype.")]
        DATATYPE = 3,

        /// <summary>
        /// Sorted on favorites.
        /// </summary>
        [InspectorName("Sorted on favorites.")]
        FAVORITES = 4
    }
}
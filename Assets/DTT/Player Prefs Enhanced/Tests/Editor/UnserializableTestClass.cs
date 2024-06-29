#if TEST_FRAMEWORK

namespace DTT.PlayerPrefsEnhanced.Tests.Runtime
{
    /// <summary>
    /// Class to represent a user made unserializable class.
    /// </summary>
    public class UnserializableTestClass
    {
        /// <summary>
        /// Two dimensional array so the class cannot be serialized by Unity's rules for serialization.
        /// </summary>
        public int[,] DimensionalArray => _dimensionalArray;

        /// <summary>
        /// Two dimensional array so the class cannot be serialized by Unity's rules for serialization.
        /// </summary>
        private int[,] _dimensionalArray;

        /// <summary>
        /// Define dimentions of the two dimensional array stored within the class.
        /// </summary>
        /// <param name="Width">Width of the two dimensional array.</param>
        /// <param name="height">Height of the two dimensional array.</param>
        public UnserializableTestClass(int Width, int height) => _dimensionalArray = new int[Width, height];

        /// <summary>
        /// Used to fill the array with numbers.
        /// </summary>
        public void FillArray()
        {
            for (int i = 0; i < _dimensionalArray.GetLength(0); i++)
            {
                for (int j = 0; j < _dimensionalArray.GetLength(1); j++)
                {
                    _dimensionalArray[i, j] = i * _dimensionalArray.GetLength(1) + j;
                }
            }
        }
    }
}

#endif
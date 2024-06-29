using UnityEngine;

namespace DTT.PlayerPrefsEnhanced.Demo
{
    /// <summary>
    /// Class used to set demo prefs for users to play around with.
    /// </summary>
    public class PrefFiller : MonoBehaviour
    {
        /// <summary>
        /// Demo string pref.
        /// </summary>
        [SerializeField]
        private string _demoPrefString = "Hello World.";

        /// <summary>
        /// Demo float pref.
        /// </summary>
        [SerializeField]
        private float _demoPrefFloat = 3.33f;

        /// <summary>
        /// Demo int pref.
        /// </summary>
        [SerializeField]
        private int _demoPrefInt = 4;

        /// <summary>
        /// Demo bool pref.
        /// </summary>
        [SerializeField]
        private bool _demoPrefBool = true;

        /// <summary>
        /// Demo vector2 pref.
        /// </summary>
        [SerializeField]
        private Vector2 _demoPrefVector2 = new Vector2(1, 2.5f);

        /// <summary>
        /// Demo vector3 pref.
        /// </summary>
        [SerializeField]
        private Vector3 _demoPrefVector3 = new Vector3(1.5f, 2, 3);

        /// <summary>
        /// Demo vector4 pref.
        /// </summary>
        [SerializeField]
        private Vector4 _demoPrefVector4 = new Vector4(1.5f, 2, 3, 7);

        /// <summary>
        /// Demo long pref.
        /// </summary>
        [SerializeField]
        private long _demoPrefLong = 1972391723987123;

        /// <summary>
        /// Demo quaternion pref.
        /// </summary>
        [SerializeField]
        private Quaternion _demoPrefQuaternion = new Quaternion(4, 1, 2, 1);

        /// <summary>
        /// Demo color pref.
        /// </summary>
        [SerializeField]
        private Color _demoPrefColor = Color.red;

        /// <summary>
        /// Set prefs with data from the inspector.
        /// </summary>
        public void SetPlayerPrefs()
        {
            EnhancedPrefs.SetPlayerPref("Demo_String", _demoPrefString);
            EnhancedPrefs.SetPlayerPref("Demo_Float", _demoPrefFloat);
            EnhancedPrefs.SetPlayerPref("Demo_Int", _demoPrefInt);
            EnhancedPrefs.SetPlayerPref("Demo_Bool", _demoPrefBool);
            EnhancedPrefs.SetPlayerPref("Demo_Vector2", _demoPrefVector2);
            EnhancedPrefs.SetPlayerPref("Demo_Vector3", _demoPrefVector3);
            EnhancedPrefs.SetPlayerPref("Demo_Vector4", _demoPrefVector4);
            EnhancedPrefs.SetPlayerPref("Demo_Long", _demoPrefLong);
            EnhancedPrefs.SetPlayerPref("Demo_Quaternion", _demoPrefQuaternion);
            EnhancedPrefs.SetPlayerPref("Demo_Color", _demoPrefColor);
        }
    }
}
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Abstract class that contains shared information between the different <see cref="IPlayerPrefForm"/> instances.
    /// </summary>
    internal abstract class AbstractPrefForm : IPlayerPrefForm
    {
        /// <summary>
        /// The icon corresponding to the opperation.
        /// </summary>
        protected Texture2D p_icon;

        /// <summary>
        /// The current selected <see cref="IDataTypeForm"/>.
        /// </summary>
        protected static IDataTypeForm p_dataTypeForm;

        /// <summary>
        /// The Editor Window the content is displayed on.
        /// </summary>
        protected EditorWindow p_window;

        /// <summary>
        /// The <see cref="PlayerPrefModel"/> the CRUD opperations are being used on.
        /// </summary>
        protected PlayerPrefModel p_playerPref;

        /// <summary>
        /// Whether the pref is or should be encrypted.
        /// </summary>
        protected bool p_prefIsEncrypted;

        /// <summary>
        /// The height of the labels and inputfields.
        /// </summary>
        protected int p_labelHeight = 20;

        /// <summary>
        /// The width of the labels and inputfields prefixes.
        /// </summary>
        protected int p_labelWidth = 80;

        /// <summary>
        /// Dictionary that links the <see cref="DataTypes"/> to an <see cref="IDataTypeForm"/>.
        /// Used as a look up table.
        /// </summary>
        private static Dictionary<DataTypes, IDataTypeForm> _dataTypesForms = new Dictionary<DataTypes, IDataTypeForm>()
        {
            { DataTypes.STRING, new StringTypeForm() },
            { DataTypes.INT, new IntTypeForm() },
            { DataTypes.FLOAT, new FloatTypeForm() },
            { DataTypes.DOUBLE, new DoubleTypeForm() },
            { DataTypes.LONG, new LongTypeForm() },
            { DataTypes.BOOL, new BoolTypeForm() },
            { DataTypes.VECTOR2, new Vector2TypeForm() },
            { DataTypes.VECTOR3, new Vector3TypeForm() },
            { DataTypes.VECTOR4, new Vector4TypeForm() },
            { DataTypes.QUATERNION, new QuaternionTypeForm() },
            { DataTypes.COLOR, new ColorTypeForm() }
        };

        /// <summary>
        /// Gets the icon stored in the form.
        /// </summary>
        /// <returns>The icon corresponding to the form.</returns>
        public Texture2D GetIcon() => p_icon;

        /// <summary>
        /// Sets the <see cref="PlayerPrefModel"/> and <see cref="IDataTypeForm"/> based on the data in the provided <see cref="PlayerPrefModel"/>
        /// </summary>
        /// <param name="playerPref">The <see cref="PlayerPrefModel"/> to open a CRUD window for.</param>
        public void SetPlayerPref(PlayerPrefModel playerPref)
        {
            p_playerPref = playerPref;
            p_prefIsEncrypted = p_playerPref.IsEncrypted;

            SetDataTypeForm(p_playerPref.DataType);

            if (p_dataTypeForm == null)
                SetTypeFormValues();
        }

        /// <summary>
        /// Sets the <see cref="IDataTypeForm"/> based on the <see cref="DataTypes"/> provided.
        /// </summary>
        /// <param name="type">The data type the form should use.</param>
        public void SetDataTypeForm(DataTypes type)
        {
            if (_dataTypesForms.ContainsKey(type))
            {
                p_dataTypeForm = _dataTypesForms[type];
                SetTypeFormValues();
            }
            else
            {
                p_dataTypeForm = null;
            }
        }

        /// <summary>
        /// Sets the Editor Window used to display content on.
        /// </summary>
        /// <param name="window">The target Editor Window.</param>
        public virtual void SetWindow(EditorWindow window) => p_window = window;

        /// <summary>
        /// Draws the content to be displayed on the Editor Window.
        /// </summary>
        public abstract void DrawContent();

        /// <summary>
        /// Sets the key and variable values in the <see cref="IDataTypeForm"/> based on the player pref key.
        /// </summary>
        protected virtual void SetTypeFormValues() => p_dataTypeForm.Key = p_playerPref.Key;
    }
}
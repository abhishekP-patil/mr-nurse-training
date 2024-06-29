using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using DTT.PublishingTools;
using System.Linq;

namespace DTT.PlayerPrefsEnhanced.Editor
{
    /// <summary>
    /// Editor window used to display the player prefs in a list.
    /// </summary>
    [DTTHeader("dtt.player-prefs-enhanced")]
    internal class PlayerPrefsEnhancedEditorWindow : DTTEditorWindow
    {
        /// <summary>
        /// Used to display the playerpref list.
        /// </summary>
        private static PlayerPrefsEnhancedEditorWindow _window;

        /// <summary>
        /// Used to display the CRUD opperation forms.
        /// </summary>
        private EditorWindow _formWindow;

        /// <summary>
        /// Used to load textures from their source folders.
        /// </summary>
        private static PlayerPrefsWindowTextures _textures;

        /// <summary>
        /// The sorting type we have selected
        /// </summary>
        private int _selectedSortingMethodIndex = 0;

        /// <summary>
        /// The sorting type we have selected
        /// </summary>
        private SortingMethods _selectedSortingMethod => (SortingMethods)_selectedSortingMethodIndex;

        /// <summary>
        /// The input on which we filter the playerPrefs.
        /// </summary>
        private string _filterInput = "";

        /// <summary>
        /// List of all names of the sorting types we have
        /// </summary>
        private List<string> _sortingMethods = new List<string>();

        /// <summary>
        /// Save the scrollposition 
        /// </summary>
        private Vector2 _scrollView;

        /// <summary>
        /// List of all the PlayerPrefReferences which are used to generate the visual view elements
        /// </summary>
        private List<PlayerPrefModel> _allPlayerPrefs;

        /// <summary>
        /// List of all the PlayerPrefReferences which are used to generate the visual view elements
        /// </summary>
        private List<PlayerPrefModel> _filteredPlayerPrefs;

        /// <summary>
        /// The height of the labels and inputfields.
        /// </summary>
        private int _labelHeight = 20;

        /// <summary>
        /// Form used to create new player prefs with datatype selection option.
        /// </summary>
        private IPlayerPrefForm _createNewPrefForm = new CreateNewPlayerPrefForm();

        /// <summary>
        /// Form used to delte all player prefs.
        /// </summary>
        private IPlayerPrefForm _deleteAllPrefsForm = new DeleteAllPlayerPrefsForm();

        /// <summary>
        /// Dictionairy used to link the different <see cref="IPlayerPrefForm"/>s to their CRUD opperation button.
        /// </summary>
        private List<IPlayerPrefForm> _forms;

        /// <summary>
        /// Contains styling for GUI element.
        /// </summary>
        private PlayerPrefsEnhancedEditorWindowStyle _style;
        
        /// <summary>
        /// Menu item creation method
        /// </summary>
        [MenuItem("Tools/DTT/Player Prefs Enhanced/Window")]
        private static void Init()
        {
            _window = GetWindow<PlayerPrefsEnhancedEditorWindow>();
            _window.titleContent = new GUIContent("Player Prefs Debugger");
            _window.minSize = new Vector2(600,300);
            _window.Show();
        }

        /// <summary>
        /// Create form instances for CRUD opperations.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            _textures = new PlayerPrefsWindowTextures();
            _style = new PlayerPrefsEnhancedEditorWindowStyle();

            _forms = new List<IPlayerPrefForm>()
            {
                new UpdatePlayerPrefForm(_textures.EditLight),
                new DeletePlayerPrefForm(_textures.TrashBinIconLight)
            };
        }

        /// <summary>
        /// OnGUI method, draws the editor window.
        /// </summary>
        protected override void OnGUI()
        {
            base.OnGUI();

            DrawFilter();

            DrawSortingOptions();

            GUILayout.Space(10);

            SortPlayerPrefs();

            DrawScrollView();

            DrawCreateDeleteButtons();

            GUILayout.Space(5);
        }

        /// <summary>
        /// On awake we refresh the list of the PlayerPrefs with the provided script
        /// </summary>
        private void Awake()
        {
            _allPlayerPrefs = EnhancedPrefs.GetAllPlayerPrefs();

            _sortingMethods.Clear();
            foreach (int i in Enum.GetValues(typeof(SortingMethods)))
            {
                string sortingName = ((SortingMethods)i).ToString();
                sortingName = sortingName.Replace("_", " ");
                sortingName = sortingName.Replace("TO", "to");
                _sortingMethods.Add(sortingName);
            }
        }

        /// <summary>
        /// Draws the filter used to search for a player pref based on key name.
        /// </summary>
        private void DrawFilter()
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            EditorGUILayout.LabelField("Search Player Pref:", _style.LabelRightAligned, GUILayout.Width(115), GUILayout.Height(_labelHeight));
            _filterInput = EditorGUILayout.TextField(_filterInput, GUILayout.Width(376), GUILayout.Height(_labelHeight));
            if (GUILayout.Button("X", _style.Button, GUILayout.Width(_labelHeight), GUILayout.Height(_labelHeight)))
            {
                GUIUtility.keyboardControl = 0;
                GUIUtility.hotControl = 0;
                _filterInput = "";
            }

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws the buttons to select the sorting options used to order the player prefs.
        /// </summary>
        private void DrawSortingOptions()
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            EditorGUILayout.LabelField("Sort By:", _style.LabelRightAligned, GUILayout.Width(115), GUILayout.Height(_labelHeight));
            _selectedSortingMethodIndex = GUILayout.Toolbar(_selectedSortingMethodIndex, _sortingMethods.ToArray(), "Button", GUILayout.Width(400), GUILayout.Height(_labelHeight));

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Sorts the player pref list based on the selected tab, filter input and selected sorting option.
        /// </summary>
        private void SortPlayerPrefs()
        {
            _filteredPlayerPrefs = _allPlayerPrefs;

            if (!string.IsNullOrEmpty(_filterInput))
                _filteredPlayerPrefs = _filteredPlayerPrefs.Where(x => x.Key.ToLower().Contains(_filterInput.ToLower())).ToList();

            switch (_selectedSortingMethod)
            {
                case SortingMethods.NONE:
                    break;
                case SortingMethods.A_TO_Z:
                    _filteredPlayerPrefs = _filteredPlayerPrefs.OrderBy(x => x.Key).ToList();
                    break;
                case SortingMethods.Z_TO_A:
                    _filteredPlayerPrefs = _filteredPlayerPrefs.OrderByDescending(x => x.Key).ToList();
                    break;
                case SortingMethods.DATATYPE:
                    _filteredPlayerPrefs = _filteredPlayerPrefs.OrderBy(x => x.ShortDataTypeName).ToList();
                    break;
                case SortingMethods.FAVORITES:
                    _filteredPlayerPrefs = _filteredPlayerPrefs.OrderByDescending(x => x.Favourite).ToList();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Draws the scrollview containing the elements to display the player prefs.
        /// </summary>
        private void DrawScrollView()
        {
            ApiCompatibilityLevel compatibilityLevel = PlayerSettings.GetApiCompatibilityLevel(BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget));

            if (compatibilityLevel == ApiCompatibilityLevel.NET_4_6)
            {
                DrawColNames();

                _scrollView = EditorGUILayout.BeginScrollView(_scrollView, EditorStyles.helpBox);

                foreach (PlayerPrefModel playerPref in _filteredPlayerPrefs)
                    DrawPlayerPrefElement(playerPref);

                GUILayout.FlexibleSpace();

                EditorGUILayout.EndScrollView();
            }
            else
            {
                string warning = "To make use of the player pref overview, " +
                                 "the \'API Compatibility Level\' needs to be set to \".Net 4.x\". " +
                                 "This setting can be found in Edit/Project Settings/Player.";
                EditorGUILayout.HelpBox(warning, MessageType.Warning);
            }
        }

        /// <summary>
        /// Draws the column names for the player prefs list.
        /// </summary>
        private void DrawColNames()
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Space(4);

            if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("d_AssemblyLock")), _style.Button, GUILayout.Width(_labelHeight), GUILayout.Height(_labelHeight)))
                RefreshPrefsList();

            EditorGUILayout.LabelField("Key", _style.LabelMiddleAligned, GUILayout.Width(100), GUILayout.Height(_labelHeight));
            EditorGUILayout.LabelField("Value", _style.LabelMiddleAligned, GUILayout.Height(_labelHeight));
            EditorGUILayout.LabelField("Data Type", _style.LabelMiddleAligned, GUILayout.Width(75), GUILayout.Height(_labelHeight));

            GUILayout.Space(40);

            if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("d_Refresh")), _style.Button, GUILayout.Width(_labelHeight), GUILayout.Height(_labelHeight)))
                RefreshPrefsList();

            GUILayout.Space(4);

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws a player pref element.
        /// </summary>
        /// <param name="pref">Player pref to be displayed by the element.</param>
        private void DrawPlayerPrefElement(PlayerPrefModel pref)
        {
            EditorGUILayout.BeginHorizontal();


            string starIcon = pref.Favourite? "Favorite Icon": "Favorite On Icon";
            if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent(starIcon)), _style.Button, GUILayout.Width(_labelHeight), GUILayout.Height(_labelHeight)))
            {
                pref.Favourite = !pref.Favourite;
                EnhancedPrefs.SetJsonPlayerPref(pref.Key, pref.JsonValue, pref.IsEncrypted, pref.Favourite, pref.FullDataTypeName);
            }

            GUI.enabled = false;
            EditorGUILayout.Toggle(pref.IsEncrypted, GUILayout.Width(16), GUILayout.Height(_labelHeight));
            GUI.enabled = true;

            EditorGUILayout.LabelField(pref.Key, _style.LabelCenterAligned, GUILayout.Width(100), GUILayout.Height(_labelHeight));

            string plainTextValue = pref.JsonValue.Replace("\"_value\":","");
            plainTextValue = plainTextValue.Substring(1, plainTextValue.Length - 2);
            EditorGUILayout.LabelField(plainTextValue, _style.LabelCenterAligned, GUILayout.Height(_labelHeight));

            EditorGUILayout.LabelField(pref.ShortDataTypeName, _style.LabelCenterAligned, GUILayout.Width(70), GUILayout.Height(_labelHeight));

            foreach (IPlayerPrefForm form in _forms)
            {
                GUILayout.Space(5);

                if (GUILayout.Button(form.GetIcon(), _style.ButtonLargePadding, GUILayout.Width(_labelHeight), GUILayout.Height(_labelHeight)))
                {
                    form.SetPlayerPref(pref);

                    _formWindow = PlayerPrefFormEditorWindow.OpenForm(form);
                    SetFormPosition();

                    form.SetWindow(_formWindow);
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Sets the center of the <see cref="_formWindow"/> in the center of <see cref="_window"/>.
        /// </summary>
        public void SetFormPosition()
        {
            Rect formPosition = this.position;
            formPosition.x += this.position.width / 2;
            formPosition.x -= _formWindow.position.width / 2;

            formPosition.y += this.position.height / 2;
            formPosition.y -= _formWindow.position.height / 2;

            _formWindow.position = formPosition;
        }

        /// <summary>
        /// Draws the Delete all and create new player pref buttons.
        /// </summary>
        private void DrawCreateDeleteButtons()
        {
            int buttonWidth = 250;

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Delete All", GUILayout.Width(buttonWidth)))
            {
                _formWindow = PlayerPrefFormEditorWindow.OpenForm(_deleteAllPrefsForm);
                SetFormPosition();

                _deleteAllPrefsForm.SetWindow(_formWindow);
            }

            EditorGUILayout.Separator();

            if (GUILayout.Button("Create Player Pref", GUILayout.Width(buttonWidth)))
            {
                _formWindow = PlayerPrefFormEditorWindow.OpenForm(_createNewPrefForm);
                SetFormPosition();

                _createNewPrefForm.SetWindow(_formWindow);
            }
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Sets the player pref list to the data retrieved from the player pref file.
        /// </summary>
        public void RefreshPrefsList() => _allPlayerPrefs = EnhancedPrefs.GetAllPlayerPrefs();
    }
}
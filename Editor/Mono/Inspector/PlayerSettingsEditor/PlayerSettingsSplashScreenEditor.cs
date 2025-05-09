// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using UnityEditor.AnimatedValues;
using UnityEditor.Modules;
using UnityEditorInternal;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEditor.Build;
using UnityEngine.Events;

namespace UnityEditor
{
    internal partial class PlayerSettingsSplashScreenEditor
    {
        PlayerSettingsEditor m_Owner;

        SerializedProperty m_ShowUnitySplashLogo;
        SerializedProperty m_ShowUnitySplashScreen;
        SerializedProperty m_SplashScreenAnimation;
        SerializedProperty m_SplashScreenBackgroundAnimationZoom;
        SerializedProperty m_SplashScreenBackgroundColor;
        SerializedProperty m_SplashScreenBackgroundLandscape;
        SerializedProperty m_SplashScreenBackgroundPortrait;
        SerializedProperty m_SplashScreenBlurBackground;
        SerializedProperty m_SplashScreenDrawMode;
        SerializedProperty m_SplashScreenLogoAnimationZoom;
        SerializedProperty m_SplashScreenLogos;
        SerializedProperty m_SplashScreenLogoStyle;
        SerializedProperty m_SplashScreenOverlayOpacity;
        SerializedProperty m_VirtualRealitySplashScreen;

        ReorderableList m_LogoList;

        float m_TotalLogosDuration;

        static readonly float k_MinLogoTime = 2;
        static readonly float k_MaxLogoTime = 10.0f;
        static readonly float k_DefaultLogoTime = 2.0f;

        static readonly float k_LogoListElementHeight = 72;
        static readonly float k_LogoListLogoFieldHeight = 64;
        static readonly float k_LogoListFooterHeight = 20;
        static readonly float k_LogoListUnityLogoMinWidth = 64;
        static readonly float k_LogoListUnityLogoMaxWidth = 220;
        static readonly float k_LogoListPropertyMinWidth = 230;
        static readonly float k_LogoListPropertyLabelWidth = 100;
        static readonly float k_MinOverlayOpacity = 0.0f;
        static readonly Color32 k_DarkOnLightBgColor = new Color32(204, 204, 204, 255);// #CCCCCC
        static readonly Color32 k_LightOnDarkBgColor = new Color32(35, 31, 32, 255);

        static Sprite s_UnityLogoLight; // We use this version as a placeholder when the logo is in the list.
        static Sprite s_UnityLogoDark;

        readonly AnimBool m_ShowAnimationControlsAnimator = new AnimBool();
        readonly AnimBool m_ShowBackgroundColorAnimator = new AnimBool();
        readonly AnimBool m_ShowLogoControlsAnimator = new AnimBool();

        Sprite UnityLogo => m_SplashScreenLogoStyle.intValue == (int)PlayerSettings.SplashScreen.UnityLogoStyle.DarkOnLight ? s_UnityLogoDark : s_UnityLogoLight;

        // If the user changes an asset(delete, re-import etc) then we should cancel the splash screen to avoid using invalid data. (case 857060)
        class CancelSplashScreenOnAssetChange : AssetPostprocessor
        {
            void OnPreprocessAsset()
            {
                SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate);
            }
        }

        class CancelSplashScreenOnAssetDelete : AssetModificationProcessor
        {
            static AssetDeleteResult OnWillDeleteAsset(string asset, RemoveAssetOptions options)
            {
                SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate);
                return AssetDeleteResult.DidNotDelete;
            }
        }

        internal class Texts
        {
            public static readonly GUIContent animate = EditorGUIUtility.TrTextContent("Animation");
            public static readonly GUIContent backgroundColor = EditorGUIUtility.TrTextContent("Background Color", "Background color when no background image is used. On Android, use this property to set static splash image background color.");
            public static readonly GUIContent backgroundImage = EditorGUIUtility.TrTextContent("Background Image", "Image to be used in landscape and portrait (when portrait image is not set).");
            public static readonly GUIContent backgroundPortraitImage = EditorGUIUtility.TrTextContent("Alternate Portrait Image*", "Optional image to be used in portrait mode.");
            public static readonly GUIContent backgroundTitle = EditorGUIUtility.TrTextContent("Background*");
            public static readonly GUIContent backgroundZoom = EditorGUIUtility.TrTextContent("Background Zoom");
            public static readonly GUIContent blurBackground = EditorGUIUtility.TrTextContent("Blur Background Image");
            public static readonly GUIContent cancelPreviewSplash = EditorGUIUtility.TrTextContent("Cancel Preview");
            public static readonly GUIContent configDialogBanner = EditorGUIUtility.TrTextContent("Application Config Dialog Banner");
            public static readonly GUIContent configDialogBannerDeprecationWarning = EditorGUIUtility.TrTextContent("Application Config Dialog Banner is deprecated and will be removed in future versions.");
            public static readonly GUIContent drawMode = EditorGUIUtility.TrTextContent("Draw Mode");
            public static readonly GUIContent logoDuration = EditorGUIUtility.TrTextContent("Logo Duration", "The time the logo will be shown for.");
            public static readonly GUIContent logosTitle = EditorGUIUtility.TrTextContent("Logos*");
            public static readonly GUIContent logoZoom = EditorGUIUtility.TrTextContent("Logo Zoom");
            public static readonly GUIContent overlayOpacity = EditorGUIUtility.TrTextContent("Overlay Opacity", "Overlay strength applied to improve logo visibility.");
            public static readonly GUIContent previewSplash = EditorGUIUtility.TrTextContent("Preview", "Preview the splash screen in the game view.");
            public static readonly GUIContent showLogo = EditorGUIUtility.TrTextContent("Show Unity Logo");
            public static readonly GUIContent showSplash = EditorGUIUtility.TrTextContent("Show Splash Screen");
            public static readonly GUIContent splashStyle = EditorGUIUtility.TrTextContent("Splash Style");
            public static readonly GUIContent splashTitle = EditorGUIUtility.TrTextContent("Splash Screen");
            public static readonly GUIContent title = EditorGUIUtility.TrTextContent("Splash Image");
            public static readonly GUIContent vrSplashScreen = EditorGUIUtility.TrTextContent("Virtual Reality Splash Image");
        }

        public PlayerSettingsSplashScreenEditor(PlayerSettingsEditor owner)
        {
            m_Owner = owner;
        }

        public void OnEnable()
        {
            m_ShowUnitySplashLogo = m_Owner.FindPropertyAssert("m_ShowUnitySplashLogo");
            m_ShowUnitySplashScreen = m_Owner.FindPropertyAssert("m_ShowUnitySplashScreen");
            m_SplashScreenAnimation = m_Owner.FindPropertyAssert("m_SplashScreenAnimation");
            m_SplashScreenBackgroundAnimationZoom = m_Owner.FindPropertyAssert("m_SplashScreenBackgroundAnimationZoom");
            m_SplashScreenBackgroundColor = m_Owner.FindPropertyAssert("m_SplashScreenBackgroundColor");
            m_SplashScreenBackgroundLandscape = m_Owner.FindPropertyAssert("splashScreenBackgroundSourceLandscape");
            m_SplashScreenBackgroundPortrait = m_Owner.FindPropertyAssert("splashScreenBackgroundSourcePortrait");
            m_SplashScreenBlurBackground = m_Owner.FindPropertyAssert("blurSplashScreenBackground");
            m_SplashScreenDrawMode = m_Owner.FindPropertyAssert("m_SplashScreenDrawMode");
            m_SplashScreenLogoAnimationZoom = m_Owner.FindPropertyAssert("m_SplashScreenLogoAnimationZoom");
            m_SplashScreenLogos = m_Owner.FindPropertyAssert("m_SplashScreenLogos");
            m_SplashScreenLogoStyle = m_Owner.FindPropertyAssert("m_SplashScreenLogoStyle");
            m_SplashScreenOverlayOpacity = m_Owner.FindPropertyAssert("m_SplashScreenOverlayOpacity");
            m_VirtualRealitySplashScreen = m_Owner.FindPropertyAssert("m_VirtualRealitySplashScreen");

            m_LogoList = new ReorderableList(m_Owner.serializedObject, m_SplashScreenLogos, true, true, true, true);
            m_LogoList.elementHeight = k_LogoListElementHeight;
            m_LogoList.footerHeight = k_LogoListFooterHeight;
            m_LogoList.onAddCallback = OnLogoListAddCallback;
            m_LogoList.drawHeaderCallback = DrawLogoListHeaderCallback;
            m_LogoList.onCanRemoveCallback = OnLogoListCanRemoveCallback;
            m_LogoList.drawElementCallback = DrawLogoListElementCallback;
            m_LogoList.drawFooterCallback = DrawLogoListFooterCallback;

            // Set up animations
            m_ShowAnimationControlsAnimator.value = m_SplashScreenAnimation.intValue == (int)PlayerSettings.SplashScreen.AnimationMode.Custom;
            m_ShowBackgroundColorAnimator.value = m_SplashScreenBackgroundLandscape.objectReferenceValue == null;
            m_ShowLogoControlsAnimator.value = m_ShowUnitySplashLogo.boolValue;
            SetValueChangeListeners(m_Owner.Repaint);

            if (s_UnityLogoLight == null)
                s_UnityLogoLight = AssetDatabase.GetBuiltinExtraResource<Sprite>("SplashScreen/UnitySplash-Light.png");
            if (s_UnityLogoDark == null)
                s_UnityLogoDark = AssetDatabase.GetBuiltinExtraResource<Sprite>("SplashScreen/UnitySplash-Dark.png");
        }

        internal void SetValueChangeListeners(UnityAction action)
        {
            m_ShowAnimationControlsAnimator.valueChanged.RemoveAllListeners();
            m_ShowAnimationControlsAnimator.valueChanged.AddListener(action);

            m_ShowBackgroundColorAnimator.valueChanged.RemoveAllListeners();
            m_ShowBackgroundColorAnimator.valueChanged.AddListener(action);

            m_ShowLogoControlsAnimator.valueChanged.RemoveAllListeners();
            m_ShowLogoControlsAnimator.valueChanged.AddListener(action);
        }

        private void DrawLogoListHeaderCallback(Rect rect)
        {
            m_TotalLogosDuration = 0; // Calculated during logo list draw
            EditorGUI.LabelField(rect, "Logos");
        }

        private void DrawElementUnityLogo(Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = m_SplashScreenLogos.GetArrayElementAtIndex(index);
            var duration = element.FindPropertyRelative("duration");

            // Unity logo
            float logoWidth = Mathf.Clamp(rect.width - k_LogoListPropertyMinWidth, k_LogoListUnityLogoMinWidth, k_LogoListUnityLogoMaxWidth);
            var logoRect = new Rect(rect.x, rect.y, logoWidth, rect.height);
            GUI.DrawTexture(logoRect, UnityLogo.texture, ScaleMode.ScaleToFit);

            // Properties
            var oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = k_LogoListPropertyLabelWidth;
            var propertyRect = new Rect(rect.x + logoWidth, rect.y + EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight, rect.width - logoWidth, EditorGUIUtility.singleLineHeight);
            EditorGUI.BeginChangeCheck();
            var durationLabel = EditorGUI.BeginProperty(propertyRect, Texts.logoDuration, duration);
            var newDurationVal = EditorGUI.Slider(propertyRect, durationLabel, duration.floatValue, k_MinLogoTime, k_MaxLogoTime);
            if (EditorGUI.EndChangeCheck())
                duration.floatValue = newDurationVal;
            EditorGUI.EndProperty();
            EditorGUIUtility.labelWidth = oldLabelWidth;

            m_TotalLogosDuration += duration.floatValue;
        }

        private void DrawLogoListElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.height -= EditorGUIUtility.standardVerticalSpacing;

            var element = m_SplashScreenLogos.GetArrayElementAtIndex(index);
            var logo = element.FindPropertyRelative("logo");

            if ((Sprite)logo.objectReferenceValue == s_UnityLogoLight)
            {
                DrawElementUnityLogo(rect, index, isActive, isFocused);
                return;
            }

            // Logo field
            float unityLogoWidth = Mathf.Clamp(rect.width - k_LogoListPropertyMinWidth, k_LogoListUnityLogoMinWidth, k_LogoListUnityLogoMaxWidth);
            var logoRect = new Rect(rect.x, rect.y + (rect.height - k_LogoListLogoFieldHeight) / 2.0f, k_LogoListUnityLogoMinWidth, k_LogoListLogoFieldHeight);
            EditorGUI.BeginChangeCheck();
            var value = EditorGUI.ObjectField(logoRect, GUIContent.none, (Sprite)logo.objectReferenceValue, typeof(Sprite), false);
            if (EditorGUI.EndChangeCheck())
                logo.objectReferenceValue = value;

            // Properties
            var propertyRect = new Rect(rect.x + unityLogoWidth, rect.y + EditorGUIUtility.standardVerticalSpacing, rect.width - unityLogoWidth, EditorGUIUtility.singleLineHeight);
            var duration = element.FindPropertyRelative("duration");

            EditorGUI.BeginChangeCheck();
            var oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = k_LogoListPropertyLabelWidth;
            var newDurationVal = EditorGUI.Slider(propertyRect, Texts.logoDuration, duration.floatValue, k_MinLogoTime, k_MaxLogoTime);
            EditorGUIUtility.labelWidth = oldLabelWidth;
            if (EditorGUI.EndChangeCheck())
                duration.floatValue = newDurationVal;

            m_TotalLogosDuration += duration.floatValue;
        }

        private void DrawLogoListFooterCallback(Rect rect)
        {
            float totalDuration = Mathf.Max(k_MinLogoTime, m_TotalLogosDuration);
            EditorGUI.LabelField(rect, "Splash Screen Duration: " + totalDuration, EditorStyles.miniBoldLabel);
            ReorderableList.defaultBehaviours.DrawFooter(rect, m_LogoList);
        }

        private void OnLogoListAddCallback(ReorderableList list)
        {
            int index = m_SplashScreenLogos.arraySize;
            m_SplashScreenLogos.InsertArrayElementAtIndex(m_SplashScreenLogos.arraySize);
            var element = m_SplashScreenLogos.GetArrayElementAtIndex(index);

            // Set up default values.
            var logo = element.FindPropertyRelative("logo");
            var duration = element.FindPropertyRelative("duration");
            logo.objectReferenceValue = null;
            duration.floatValue = k_DefaultLogoTime;
        }

        // Prevent users removing the unity logo.
        private bool OnLogoListCanRemoveCallback(ReorderableList list)
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(list.index);
            var logo = (Sprite)element.FindPropertyRelative("logo").objectReferenceValue;
            return logo != s_UnityLogoLight;
        }

        private void AddUnityLogoToLogosList()
        {
            // Only add a logo if one does not already exist.
            for (int i = 0; i < m_SplashScreenLogos.arraySize; ++i)
            {
                var listElement = m_SplashScreenLogos.GetArrayElementAtIndex(i);
                var listLogo = listElement.FindPropertyRelative("logo");
                if ((Sprite)listLogo.objectReferenceValue == s_UnityLogoLight)
                    return;
            }

            m_SplashScreenLogos.InsertArrayElementAtIndex(0);
            var element = m_SplashScreenLogos.GetArrayElementAtIndex(0);
            var logo = element.FindPropertyRelative("logo");
            var duration = element.FindPropertyRelative("duration");
            logo.objectReferenceValue = s_UnityLogoLight;
            duration.floatValue = k_DefaultLogoTime;
        }

        private void RemoveUnityLogoFromLogosList()
        {
            for (int i = 0; i < m_SplashScreenLogos.arraySize; ++i)
            {
                var element = m_SplashScreenLogos.GetArrayElementAtIndex(i);
                var logo = element.FindPropertyRelative("logo");
                if ((Sprite)logo.objectReferenceValue == s_UnityLogoLight)
                {
                    m_SplashScreenLogos.DeleteArrayElementAtIndex(i);
                    --i; // Continue checking in case we have duplicates.
                }
            }
        }

        private static bool TargetSupportsOptionalBuiltinSplashScreen(BuildTargetGroup targetGroup, ISettingEditorExtension settingsExtension)
        {
            if (settingsExtension != null)
                return settingsExtension.CanShowUnitySplashScreen();
            return targetGroup == BuildTargetGroup.Standalone;
        }

        private static void ObjectReferencePropertyField<T>(SerializedProperty property, GUIContent label) where T : UnityEngine.Object
        {
            EditorGUI.BeginChangeCheck();
            Rect r = EditorGUILayout.GetControlRect(true, 64, EditorStyles.objectFieldThumb);
            label = EditorGUI.BeginProperty(r, label, property);
            var value = EditorGUI.ObjectField(r, label, (T)property.objectReferenceValue, typeof(T), false);
            if (EditorGUI.EndChangeCheck())
            {
                property.objectReferenceValue = value;
            }
            EditorGUI.EndProperty();
        }

        public void SplashSectionGUI(BuildPlatform platform, ISettingEditorExtension settingsExtension, int sectionIndex = 2)
        {
            if (m_Owner.BeginSettingsBox(sectionIndex, Texts.title))
            {
                if (platform.namedBuildTarget == NamedBuildTarget.Server)
                {
                    PlayerSettingsEditor.ShowNoSettings();
                    EditorGUILayout.Space();
                }
                else
                {
                    bool VREnabled = BuildPipeline.IsFeatureSupported("ENABLE_VR", platform.defaultTarget);

                    if (VREnabled)
                        ObjectReferencePropertyField<Texture2D>(m_VirtualRealitySplashScreen, Texts.vrSplashScreen);

                    if (TargetSupportsOptionalBuiltinSplashScreen(platform.namedBuildTarget.ToBuildTargetGroup(), settingsExtension))
                        BuiltinCustomSplashScreenGUI(platform.namedBuildTarget.ToBuildTargetGroup(), settingsExtension);

                    if (settingsExtension != null)
                    {
                        settingsExtension.SplashSectionGUI();
                        if (!m_ShowUnitySplashScreen.boolValue && settingsExtension.SupportsStaticSplashScreenBackgroundColor())
                            EditorGUILayout.PropertyField(m_SplashScreenBackgroundColor, Texts.backgroundColor);
                    }

                    if (m_ShowUnitySplashScreen.boolValue)
                        m_Owner.ShowSharedNote();
                }
            }
            m_Owner.EndSettingsBox();
        }

        private void BuiltinCustomSplashScreenGUI(BuildTargetGroup targetGroup, ISettingEditorExtension settingsExtension)
        {
            EditorGUILayout.LabelField(Texts.splashTitle, EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(m_ShowUnitySplashScreen, Texts.showSplash);
            if (!m_ShowUnitySplashScreen.boolValue)
                return;

            GUIContent buttonLabel = SplashScreen.isFinished ? Texts.previewSplash : Texts.cancelPreviewSplash;
            Rect previewButtonRect = GUILayoutUtility.GetRect(buttonLabel, "button");
            previewButtonRect = EditorGUI.PrefixLabel(previewButtonRect, new GUIContent(" "));
            if (GUI.Button(previewButtonRect, buttonLabel))
            {
                if (SplashScreen.isFinished)
                {
                    SplashScreen.Begin();
                    PlayModeView.RepaintAll();
                    var playModeView = PlayModeView.GetMainPlayModeView();
                    if (playModeView)
                    {
                        playModeView.Focus();
                    }
                    EditorApplication.update += PollSplashState;
                }
                else
                {
                    SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate);
                    EditorApplication.update -= PollSplashState;
                }

                GameView.RepaintAll();
            }

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(m_SplashScreenLogoStyle, Texts.splashStyle);
            if (EditorGUI.EndChangeCheck())
            {
                if (m_SplashScreenLogoStyle.intValue == (int)PlayerSettings.SplashScreen.UnityLogoStyle.DarkOnLight)
                    m_SplashScreenBackgroundColor.colorValue = k_DarkOnLightBgColor;
                else
                    m_SplashScreenBackgroundColor.colorValue = k_LightOnDarkBgColor;
            }

            // Animation
            EditorGUILayout.PropertyField(m_SplashScreenAnimation, Texts.animate);
            m_ShowAnimationControlsAnimator.target = m_SplashScreenAnimation.intValue == (int)PlayerSettings.SplashScreen.AnimationMode.Custom;

            if (EditorGUILayout.BeginFadeGroup(m_ShowAnimationControlsAnimator.faded))
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.Slider(m_SplashScreenLogoAnimationZoom, 0.0f, 1.0f, Texts.logoZoom);
                EditorGUILayout.Slider(m_SplashScreenBackgroundAnimationZoom, 0.0f, 1.0f, Texts.backgroundZoom);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFadeGroup();

            EditorGUILayout.Space();

            // Logos
            EditorGUILayout.LabelField(Texts.logosTitle, EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(m_ShowUnitySplashLogo, Texts.showLogo);
            if (EditorGUI.EndChangeCheck())
            {
                if (!m_ShowUnitySplashLogo.boolValue)
                    RemoveUnityLogoFromLogosList();
                else if (m_SplashScreenDrawMode.intValue == (int)PlayerSettings.SplashScreen.DrawMode.AllSequential)
                    AddUnityLogoToLogosList();
            }

            m_ShowLogoControlsAnimator.target = m_ShowUnitySplashLogo.boolValue;

            if (EditorGUILayout.BeginFadeGroup(m_ShowLogoControlsAnimator.faded))
            {
                EditorGUI.BeginChangeCheck();
                var oldDrawmode = m_SplashScreenDrawMode.intValue;
                EditorGUILayout.PropertyField(m_SplashScreenDrawMode, Texts.drawMode);
                if (oldDrawmode != m_SplashScreenDrawMode.intValue)
                {
                    if (m_SplashScreenDrawMode.intValue == (int)PlayerSettings.SplashScreen.DrawMode.UnityLogoBelow)
                        RemoveUnityLogoFromLogosList();
                    else
                        AddUnityLogoToLogosList();
                }
            }
            EditorGUILayout.EndFadeGroup();

            using (var vertical = new EditorGUILayout.VerticalScope())
            using (new EditorGUI.PropertyScope(vertical.rect, GUIContent.none, m_SplashScreenLogos))
            {
                m_LogoList.DoLayoutList();
            }


            EditorGUILayout.Space();

            // Background
            EditorGUILayout.LabelField(Texts.backgroundTitle, EditorStyles.boldLabel);
            EditorGUILayout.Slider(m_SplashScreenOverlayOpacity, k_MinOverlayOpacity, 1.0f, Texts.overlayOpacity);
            m_ShowBackgroundColorAnimator.target = m_SplashScreenBackgroundLandscape.objectReferenceValue == null ||
                (settingsExtension?.SupportsStaticSplashScreenBackgroundColor() ?? false);
            if (EditorGUILayout.BeginFadeGroup(m_ShowBackgroundColorAnimator.faded))
                EditorGUILayout.PropertyField(m_SplashScreenBackgroundColor, Texts.backgroundColor);
            EditorGUILayout.EndFadeGroup();

            EditorGUILayout.PropertyField(m_SplashScreenBlurBackground, Texts.blurBackground);
            EditorGUI.BeginChangeCheck();
            ObjectReferencePropertyField<Sprite>(m_SplashScreenBackgroundLandscape, Texts.backgroundImage);
            if (EditorGUI.EndChangeCheck() && m_SplashScreenBackgroundLandscape.objectReferenceValue == null)
                m_SplashScreenBackgroundPortrait.objectReferenceValue = null;

            using (new EditorGUI.DisabledScope(m_SplashScreenBackgroundLandscape.objectReferenceValue == null))
            {
                ObjectReferencePropertyField<Sprite>(m_SplashScreenBackgroundPortrait, Texts.backgroundPortraitImage);
            }
        }

        void PollSplashState()
        {
            // Force the GameViews to repaint whilst showing the splash(1166664)
            PlayModeView.RepaintAll();

            // When the splash screen is playing we need to keep track so that we can update the preview button when it has finished.
            if (SplashScreen.isFinished)
            {
                var window = SettingsWindow.FindWindowByScope(SettingsScope.Project);
                window?.Repaint();
                EditorApplication.update -= PollSplashState;
            }
        }
    }
}

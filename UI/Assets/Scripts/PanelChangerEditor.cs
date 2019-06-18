using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PanelChanger))]
public class PanelChangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PanelChanger _panel = target as PanelChanger;

        _panel.fadeInMode = (PanelChanger.FadeMode)EditorGUILayout.EnumPopup("fadeInMode", _panel.fadeInMode);
        if (_panel.fadeInMode == PanelChanger.FadeMode.Fade)
        {
            _panel.fadeIn = (PanelChanger.FadeDirection)EditorGUILayout.EnumPopup("fadeIn", _panel.fadeIn);
            _panel.fadeInTime = EditorGUILayout.FloatField("fadeInTime", _panel.fadeInTime);
            _panel.fadeInDelay = EditorGUILayout.FloatField("fadeinDelay", _panel.fadeInDelay);
            _panel.fadeInDistance = EditorGUILayout.FloatField("fadeinDistance", _panel.fadeInDistance);
            _panel.fadeInAlpha = EditorGUILayout.Toggle("fadeinAlpha", _panel.fadeInAlpha);
            _panel.fadeInAnimationCurve = EditorGUILayout.CurveField("fadeInAnimationCurve", _panel.fadeInAnimationCurve);
        }
        SerializedProperty onStart = serializedObject.FindProperty("onStart");
        EditorGUILayout.PropertyField(onStart);
        if (_panel.fadeInMode == PanelChanger.FadeMode.Fade)
        {
            SerializedProperty onFinishFadeIn = serializedObject.FindProperty("onFinishFadeIn");
            EditorGUILayout.PropertyField(onFinishFadeIn);
        }

        _panel.fadeOutMode = (PanelChanger.FadeMode)EditorGUILayout.EnumPopup("fadeOutMode", _panel.fadeOutMode);
        if (_panel.fadeOutMode == PanelChanger.FadeMode.Fade)
        {
            _panel.fadeOut = (PanelChanger.FadeDirection)EditorGUILayout.EnumPopup("fadeOut", _panel.fadeOut);
            _panel.fadeOutTime = EditorGUILayout.FloatField("fadeOutTime", _panel.fadeOutTime);
            _panel.fadeOutDelay = EditorGUILayout.FloatField("fadeOutDelay", _panel.fadeOutDelay);
            _panel.fadeOutDistance = EditorGUILayout.FloatField("fadeOutDistance", _panel.fadeOutDistance);
            _panel.fadeOutAlpha = EditorGUILayout.Toggle("fadeOutAlpha", _panel.fadeOutAlpha);
            _panel.fadeOutAnimationCurve = EditorGUILayout.CurveField("fadeOutAnimationCurve", _panel.fadeOutAnimationCurve);
            SerializedProperty onStartFadeOut = serializedObject.FindProperty("onStartFadeOut");
            EditorGUILayout.PropertyField(onStartFadeOut);
        }
        SerializedProperty onFinish = serializedObject.FindProperty("onFinish");
        EditorGUILayout.PropertyField(onFinish);

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }

        EditorUtility.SetDirty(target);
    }

}
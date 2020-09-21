using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class IconManager
{
    public enum Icon
    {
        None = -1,
        CircleGray,
        CircleBlue,
        CircleTeal,
        CircleGreen,
        CircleYellow,
        CircleOrange,
        CircleRed,
        CirclePurple,
        DiamondGray,
        DiamondBlue,
        DiamondTeal,
        DiamondGreen,
        DiamondYellow,
        DiamondOrange,
        DiamondRed,
        DiamondPurple
    }

    public enum LabelIcon
    {
        None = -1,
        Gray,
        Blue,
        Teal,
        Green,
        Yellow,
        Orange,
        Red,
        Purple
    }

    public static GUIContent[] _LabelIcons;
    public static GUIContent[] _LargeIcons;

    public static void SetIcon(GameObject _object, Icon _icon)
    {
        if (_LargeIcons == null) _LargeIcons = GetTextrues("sv_icon_dot", "_pix16_gizmo", 0, 16);
        if (_icon == Icon.None) RemoveIcon(_object);
        else InternalSetIcon(_object, _LargeIcons[(int)_icon].image as Texture2D);
    }

    public static void SetLabelIcon(GameObject _object, LabelIcon _label_icon)
    {
        if (_LabelIcons == null) _LabelIcons = GetTextrues("sv_label_", string.Empty, 0, 8);
        if (_label_icon == LabelIcon.None) RemoveIcon(_object);
        else InternalSetIcon(_object, _LabelIcons[(int)_label_icon].image as Texture2D);
    }

    public static void AddedIcon(GameObject _object, Texture2D _texture_2D)
    {
        InternalSetIcon(_object, _texture_2D);
    }

    public static void RemoveIcon(GameObject _object)
    {
        InternalSetIcon(_object, null);
    }

    public static void InternalSetIcon(GameObject _object, Texture2D _texture_2D)
    {
#if UNITY_EDITOR
        var _ty = typeof(EditorGUIUtility);
        var _mi = _ty.GetMethod("SetIconForObject", BindingFlags.NonPublic | BindingFlags.Static);
        _mi.Invoke(null, new object[] { _object, _texture_2D });
#endif
    }

    public static GUIContent[] GetTextrues(string _base_name, string _pos_fix, int _start_index, int _count)
    {
        GUIContent[] _gui_content_array = new GUIContent[_count];
#if UNITY_EDITOR
        for (int _index = 0; _index < _count; _index++)
        {
            _gui_content_array[_index] = EditorGUIUtility.IconContent(_base_name + (_start_index + _index) + _pos_fix);
        }
#endif
        /*
        var _t = typeof(EditorGUIUtility);
        var _mi = _t.GetMethod("IconContent", BindingFlags.NonPublic | BindingFlags.Static, null, new Type[] { typeof(string) }, null);
        for (int _index = 0; _index < _count; ++_index)
        {
            _gui_content_array[_index] = _mi.Invoke(null, new object[] { _base_name + (object)(_start_index + _index) + _pos_fix }) as GUIContent;
        }
        */
        return _gui_content_array;
    }
}

public static class IconManagerExtension
{
    public static void SetIcon(GameObject _object, IconManager.Icon _icon)
    {
        if (_icon == IconManager.Icon.None) IconManager.RemoveIcon(_object);
        else IconManager.SetIcon(_object, _icon);
    }

    public static void SetLabelIcon(GameObject _object, IconManager.LabelIcon _label_icon)
    {
        if (_label_icon == IconManager.LabelIcon.None) IconManager.RemoveIcon(_object);
        else IconManager.SetLabelIcon(_object, _label_icon);
    }

    public static void AddedIcon(GameObject _object, Texture2D _texture_2D)
    {
        IconManager.AddedIcon(_object, _texture_2D);
    }

    public static void RemoveIcon(GameObject _object)
    {
        IconManager.RemoveIcon(_object);
    }
}

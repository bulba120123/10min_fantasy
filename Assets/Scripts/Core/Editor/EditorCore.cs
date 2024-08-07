using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


#nullable enable
namespace Carmone.Core.Editor
{
    public class EditorStyle
    {
        public static GUIStyle headerStyle = new GUIStyle(EditorStyles.largeLabel)
        {
            fontSize = 18, // 글자 크기
            fontStyle = FontStyle.Bold, // 글자 스타일
            alignment = TextAnchor.MiddleCenter // 글자 정렬
        };

    }

    public class EditorComponent
    {
        public static void Title(
            string title,
            GUIStyle? headerStyle = null,
            int spaceBefore = 10,
            int spaceAfter = 20
        )
        {
            headerStyle ??= EditorStyle.headerStyle;

            GUILayout.Space(spaceBefore);
            EditorGUILayout.LabelField(title, headerStyle);
            GUILayout.Space(spaceAfter);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Carmone.Core.Editor;
using EnumTypes;
using UnityEditor;
using UnityEngine;


namespace Carmone.Item
{
    [CustomEditor(typeof(ItemData))]
    public class ItemDataEditor : Editor
    {

        SerializedProperty gearStatsProperty;

        SerializedProperty initStatProp;
        SerializedProperty levelStatProp;

        void OnEnable()
        {
            gearStatsProperty = serializedObject.FindProperty("levelGearStat");
            initStatProp = serializedObject.FindProperty("initStat");
            levelStatProp = serializedObject.FindProperty("levelStat");
        }

        public override void OnInspectorGUI()
        {
            ItemData itemData = (ItemData)target;

            // 사용자 수정 영역의 라벨
            EditorComponent.Title("커스텀");
            CustomLayout(itemData);

            // 구분선
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            EditorComponent.Title("기본");
            base.OnInspectorGUI();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(itemData);
            }
        }

        private void CustomLayout(ItemData itemData)
        {
            itemData.itemType = (ItemType)EditorGUILayout.EnumPopup("타입", itemData.itemType);
            if (itemData.itemType == ItemType.Gear)
            {
                if (gearStatsProperty != null)
                {
                    EditorGUILayout.PropertyField(gearStatsProperty, new GUIContent("Gear Stats"), true);
                }
            }
            itemData.itemName = EditorGUILayout.TextField("아이템 이름", itemData.itemName);
            itemData.itemIcon = (Sprite)EditorGUILayout.ObjectField("아이템 아이콘", itemData.itemIcon, typeof(Sprite), allowSceneObjects: false);
            GUILayout.Label("아이템 설명");
            itemData.itemDesc = EditorGUILayout.TextArea(itemData.itemDesc, GUILayout.Height(50));

            // Gear
            if (itemData.itemType == ItemType.Gear)
            {
                itemData.gearType = (GearType)EditorGUILayout.EnumPopup("장비 타입", itemData.gearType);
            }

            // WEAPON
            if (itemData.itemType != ItemType.Gear && itemData.gearType == EnumTypes.GearType.NULL)
            {
                EditorGUILayout.LabelField("Weapon", EditorStyles.boldLabel);
                itemData.baseDamage = EditorGUILayout.FloatField("무기 기본 데미지", itemData.baseDamage);
                itemData.weaponType = (WeaponType)EditorGUILayout.EnumPopup("Weapon Type", itemData.weaponType);
                EditorGUILayout.LabelField("초기값", EditorStyles.boldLabel);
                DrawWeaponStat(initStatProp);
                EditorGUILayout.LabelField("레벨값", EditorStyles.boldLabel);
                DrawWeaponStatsArray(levelStatProp);
            }
        }

        private void DrawWeaponStat(SerializedProperty statProp)
        {
            GUI.backgroundColor = Color.gray;

            EditorGUILayout.BeginVertical("box");
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(statProp.FindPropertyRelative("damage"), new GUIContent("데미지"));
            EditorGUILayout.PropertyField(statProp.FindPropertyRelative("scale"), new GUIContent("크기"));
            EditorGUILayout.PropertyField(statProp.FindPropertyRelative("attackSpeed"), new GUIContent("공격속도"));
            EditorGUILayout.PropertyField(statProp.FindPropertyRelative("projectileSpeed"), new GUIContent("투사체 속도"));
            EditorGUILayout.PropertyField(statProp.FindPropertyRelative("projectileCount"), new GUIContent("투사체 개수"));
            EditorGUILayout.PropertyField(statProp.FindPropertyRelative("penetration"), new GUIContent("관통력"));

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }

        private void DrawWeaponStatsArray(SerializedProperty arrayProp)
        {
            // 배열 크기 조정
            EditorGUILayout.PropertyField(arrayProp, new GUIContent("Level Stats"), true);

            // 배열의 각 요소에 대한 편집 인터페이스를 제공합니다.
            if (arrayProp.isExpanded)
            {
                EditorGUI.indentLevel++;

                for (int i = 0; i < arrayProp.arraySize; i++)
                {
                    SerializedProperty refProp = arrayProp.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(refProp, new GUIContent("Level " + (i + 1)));
                    if (refProp.isExpanded)
                    {
                        DrawWeaponStat(refProp);
                    }
                }

                EditorGUI.indentLevel--;
            }
        }
    }
}
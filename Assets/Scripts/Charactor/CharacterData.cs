using System.Collections;
using System.Collections.Generic;
using Carmone.Item;
using EnumTypes;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptble Object/CharacterData")]
public class CharacterData : ScriptableObject
{
    public int id;
    public CharacterType characterType;
    public WeaponType startWeapon;
    public StatEffect passive;
    public int levelInterval = 1;
    public string name;
    [TextArea(3, 10)]
    public string description;
    public Sprite sprite;
    public RuntimeAnimatorController animator;
}
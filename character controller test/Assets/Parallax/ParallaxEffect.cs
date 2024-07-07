using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Threading.Tasks.Sources;
using System;

[CreateAssetMenu]
public class ParallaxEffect : ScriptableObject
{

    [SerializeField] private List<(Sprite, float)> spriteList = new List<(Sprite, float)>();


    public List<(Sprite, float)> SpriteList { get => spriteList; set => spriteList = value; }
}

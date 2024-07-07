using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct SpriteValuePair
{
    public Sprite sprite;
    public float value;
    public bool isVerticalParallax;
}

public class ParallaxMain : MonoBehaviour
{
    [SerializeField] List<SpriteValuePair> _spriteList;
    [HideInInspector, SerializeField] GameObject parallaxImage;
    [HideInInspector, SerializeField] GameObject horizontalParallaxImage;

    void Start()
    {
        
        foreach (var currentPair in _spriteList)
        {

            Sprite currentSprite = currentPair.sprite;
            float currentValue = currentPair.value;
            bool currentIsVerticalParallax = currentPair.isVerticalParallax;

            if (currentPair.isVerticalParallax)
            {

                GameObject center = parallaxImage.InstantiateWithParams(transform.position, currentSprite, currentValue, currentIsVerticalParallax, gameObject);
            } else
            {
                GameObject center = horizontalParallaxImage.InstantiateWithParams(transform.position, currentSprite, currentValue, currentIsVerticalParallax, gameObject);
            }


        }
    }

    public List<SpriteValuePair> SpriteList { get => _spriteList; set => _spriteList = value; }
}


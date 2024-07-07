using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod
{
    public static GameObject InstantiateWithParams(this GameObject prefab, Vector3 pos, Sprite sprite, float parallaxStrength, bool isVerticalParallax, GameObject parent)
    {


        GameObject newImage = UnityEngine.Object.Instantiate(original: prefab, parent: parent.transform, position: pos, rotation: Quaternion.Euler(Vector3.zero));
        

        MainParallaxImage script = newImage.GetComponent<MainParallaxImage>();


        SpriteValuePair pair = new SpriteValuePair{ sprite = sprite, value = parallaxStrength, isVerticalParallax = isVerticalParallax};


        script.Values = pair;

        return newImage;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtilities : MonoBehaviour
{
    public static bool MoreApproximately(float a, float b, float threshold)
    {


        float difference = Mathf.Abs(a - b);
        return difference < threshold;

    }
}

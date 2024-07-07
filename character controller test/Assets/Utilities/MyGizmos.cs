using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyGizmos
{

    public static void DrawLine(Vector3 start, Vector3 direction, float length)
    {

        
        Vector3 directionVector = direction.normalized * length;

        Gizmos.DrawLine(start, start + directionVector);
    }
}

public static class MyDebug
{
    public static void DrawLine(Vector3 start, Vector3 direction, float length)
    {


        Vector3 directionVector = direction.normalized * length;

        Debug.DrawLine(start, start + directionVector);
    }

}

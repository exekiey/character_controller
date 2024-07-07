using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 from = transform.position;


        RaycastHit2D raycastHit2D = Physics2D.Raycast(from, Vector2.right);


        Debug.Log(raycastHit2D.point);
        
    }
}

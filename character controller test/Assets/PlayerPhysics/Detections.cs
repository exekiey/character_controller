using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System;

public class Detections : MonoBehaviour
{
    static Detections _instance;

    public void Awake()
    {
        _instance = this;
    }

    public static (Vector2, bool) IsGrounded
    {
        get
        {

            (Vector3, bool) isNextFrameOnGround = GetNextFramePosition("ground");

            RaycastHit2D isGroundedRaycast = Physics2D.Raycast(_instance.transform.position, Vector2.down, PlayerStats.instance.IsGroundedDetection );
            Vector3 nextFramePosition = isNextFrameOnGround.Item1;
            Vector3 dummy = Vector3.zero;
            
            Collider2D groundCollider = isGroundedRaycast.collider;
            

            //is already on ground
            if (groundCollider != null )
            {


                if (groundCollider.gameObject.CompareTag("ground"))
                {

                    //Debug.Log("ya estoy en el suelo");
                    
                    return (nextFramePosition, true);
                }

            }
            
            //will fall in ground
            if (isNextFrameOnGround.Item2 )
            {

                //Debug.Log("voy al suelo");


                return (nextFramePosition, true);

            }

            //Debug.Log("no");

            //is not and will not be in ground
            return (dummy, false);


        }
    }

    public static (Vector2, bool) IsLeftWall
    {

        get
        {

            (Vector3, bool) isNextFrameOnWall = GetNextFramePosition("wall");

            RaycastHit2D isOnWallRaycast = Physics2D.Raycast(PlayerPhysics.Position, Vector2.left, PlayerStats.instance.IsWallDetection);
            Vector3 nextFramePosition = isNextFrameOnWall.Item1;
            Vector3 dummy = Vector3.zero;

            Collider2D wallCollider = isOnWallRaycast.collider;

            //is already on wall
            if (wallCollider != null)
            {

                if (wallCollider.gameObject.CompareTag("wall"))
                {
                    return (nextFramePosition, true);
                }

            }

            //will touch ground
            if (isNextFrameOnWall.Item2)
            {

                //Debug.Log("voy al suelo");


                return (nextFramePosition, true);

            }

            //Debug.Log("no");

            //is not and will not be on wall
            return (dummy, false);

        }

    }

    public static (Vector2, bool) IsRightWall
    {

        get
        {

            (Vector3, bool) isNextFrameOnWall = GetNextFramePosition("wall");

            RaycastHit2D isOnWallRaycast = Physics2D.Raycast(PlayerPhysics.Position, Vector2.right, PlayerStats.instance.IsWallDetection);
            Vector3 nextFramePosition = isNextFrameOnWall.Item1;
            Vector3 dummy = Vector3.zero;

            Collider2D wallCollider = isOnWallRaycast.collider;

            //is already on wall
            if (wallCollider != null)
            {

                if (wallCollider.gameObject.CompareTag("wall"))
                {
                    return (nextFramePosition, true);
                }

            }

            //will touch ground
            if (isNextFrameOnWall.Item2)
            {

                //Debug.Log("voy al suelo");


                return (nextFramePosition, true);

            }

            //Debug.Log("no");

            //is not and will not be on wall
            return (dummy, false);

        }

    }

    static Vector2 GetFixedNextFrameCollision(Vector2 moveDirection)
    {

        RaycastHit2D speculationRay = Physics2D.Raycast(PlayerPhysics.Position, PlayerPhysics.Velocity, PlayerPhysics.Velocity.magnitude * Time.deltaTime);

        Vector2 unfixedNextFramePosition = speculationRay.point;

        Debug.DrawLine(unfixedNextFramePosition, unfixedNextFramePosition + Vector2.left, Color.blue);

        Vector2 fixDirection = (moveDirection * -1).normalized;

        float Xfix = fixDirection.x * PlayerPhysics.SpriteRenderer.bounds.extents.x;
        float Yfix = fixDirection.y * PlayerPhysics.SpriteRenderer.bounds.extents.y;

        Vector2 fixingFactor = new Vector2(Xfix, Yfix);

        //Debug.Log(fixingFactor);

        Vector2 fixedNextFramePosition = unfixedNextFramePosition += fixingFactor;

        return fixedNextFramePosition;

    }

    public static (Vector2, bool) GetNextFramePosition(params string[] testCollisions)
    {

        Vector2 currentPosition = PlayerPhysics.Position;

        RaycastHit2D speculationRay = Physics2D.Raycast(currentPosition, currentPosition + (Vector2)PlayerPhysics.Velocity, PlayerPhysics.Velocity.magnitude * Time.deltaTime);
        //Debug.Log("next" + (currentPosition + (Vector2)PlayerPhysics.Velocity *Time.deltaTime));

        bool isNextCollidingInNextFrame = speculationRay.collider != null;

        //Debug.Log("next = " + ((Vector2)PlayerPhysics.Velocity * Time.deltaTime));

        if (isNextCollidingInNextFrame)
        {

            GameObject nextFrameCollision = speculationRay.collider.gameObject;

            bool isNextFrameCollisionInTestCollisions = testCollisions.Contains(nextFrameCollision.tag);

            if (isNextFrameCollisionInTestCollisions)
            {

                return (GetFixedNextFrameCollision(PlayerPhysics.Velocity), true);
            }
        }

        Debug.DrawLine(currentPosition, currentPosition + (Vector2)PlayerPhysics.Velocity * Time.deltaTime, Color.red);
        return (_instance.transform.position, false);
    }
}

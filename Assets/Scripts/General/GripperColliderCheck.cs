using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripperColliderCheck : MonoBehaviour
{
    [SerializeField] private HandleGripper handleGripper;
    [SerializeField] private bool isRight;

    private Ray ray;
    private RaycastHit hit;

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, (isRight ? transform.forward : -transform.forward) * 0.02f);
    }*/

    private void Update()
    {
        if (!handleGripper.open)
        {
            ray = new Ray(transform.position, isRight ? transform.forward : -transform.forward);
            if (Physics.Raycast(ray, out hit, 0.02f, LayerMask.GetMask("Dice")))
            {
                if (hit.distance <= 0.012f)
                {
                    if (isRight)
                    {
                        handleGripper.offsetMoveRight = hit.distance;
                        handleGripper.enableMoveRight = false;
                        handleGripper.gripperObject = hit.collider.gameObject;
                    }
                    else
                    {
                        handleGripper.offsetMoveLeft = hit.distance;
                        handleGripper.enableMoveLeft = false;
                    }

                }
            }
        }
    }
}

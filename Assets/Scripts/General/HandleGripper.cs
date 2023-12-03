using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class HandleGripper : MonoBehaviour
{
    [SerializeField] private Transform cadRoot;
    [SerializeField] private Transform robot;

    [SerializeField] private Transform leftInnerBeam;
    [SerializeField] private Transform leftOuterBeam;
    [SerializeField] private Transform leftFrontJoint;

    [SerializeField] private Transform rightInnerBeam;
    [SerializeField] private Transform rightOuterBeam;
    [SerializeField] private Transform rightFrontJoint;

    [SerializeField] private Transform tcp;

    public float moveValueLeft { private get; set; }
    public float moveValueRight { private get; set; }
    public bool enableMoveLeft { get; set; } = true;
    public bool enableMoveRight { get; set; } = true;
    public float offsetMoveLeft { get; set; } = 0f;
    public float offsetMoveRight { get; set; } = 0f;
    public float closeMoveValue { get; private set; } = 43.5f;
    public float openMoveValue { get; private set; } = 0f;
    public float speedValue { get; set; }

    public GameObject gripperObject { private get; set; }

    public bool open { get; set; }

    private float curMoveValueLeft;
    private float curMoveValueRight;

    public float targetXPos { set; private get; }
    public float targetYPos { set; private get; }


    private void Update()
    {
        robot.localPosition = Vector3.MoveTowards(robot.localPosition, new Vector3(targetXPos, targetYPos, robot.position.z), 0.5f * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (curMoveValueLeft > moveValueLeft)
        {
            HandleGripperObject(true);
            enableMoveRight = true;
            enableMoveLeft = true;
            open = true;
        }
        else
        {
            open = false;
        }

        if (enableMoveLeft || enableMoveRight)
        {
            MoveLeft();
            MoveRight();            
        }
        else if ((offsetMoveLeft > 0.006 || offsetMoveRight > 0.006) && leftInnerBeam.localRotation.eulerAngles.x + offsetMoveLeft * 100 < moveValueLeft 
            && rightInnerBeam.localRotation.eulerAngles.x + offsetMoveLeft * 100 < moveValueRight)
        {
            float newLeft = moveValueLeft;
            if (offsetMoveLeft > 0.006)
            {
                newLeft = leftInnerBeam.localRotation.eulerAngles.x + (offsetMoveLeft - 0.003f) * 100;
                leftInnerBeam.localRotation = MoveLeftInnerBeam(newLeft);
                leftOuterBeam.localRotation = MoveLeftOuterBeam(newLeft);
                leftFrontJoint.localRotation = MoveLeftFrontJoint(newLeft);
            }
            curMoveValueLeft = newLeft;

            float newRight = moveValueRight;
            if (offsetMoveRight > 0.006)
            {
                newRight = rightInnerBeam.localRotation.eulerAngles.x + (offsetMoveRight - 0.003f) * 100;
                rightInnerBeam.localRotation = MoveRightInnerBeam(newRight);
                rightOuterBeam.localRotation = MoveRightOuterBeam(newRight);
                rightFrontJoint.localRotation = MoveRightFrontJoint(newRight);
            }
            curMoveValueRight = newRight;

            offsetMoveLeft = 0;
            offsetMoveRight = 0;

        }
        else
        {
            moveValueLeft = curMoveValueLeft;
            moveValueRight = curMoveValueRight;

            HandleGripperObject(false);
        }
    }

    private void MoveLeft()
    {
        leftInnerBeam.localRotation = Quaternion.RotateTowards(leftInnerBeam.localRotation, MoveLeftInnerBeam(moveValueLeft), speedValue * Time.fixedDeltaTime);
        leftOuterBeam.localRotation = Quaternion.RotateTowards(leftOuterBeam.localRotation, MoveLeftOuterBeam(moveValueLeft), speedValue * Time.fixedDeltaTime);
        leftFrontJoint.localRotation = Quaternion.RotateTowards(leftFrontJoint.localRotation, MoveLeftFrontJoint(moveValueLeft), speedValue * Time.fixedDeltaTime);
    }

    private Quaternion MoveLeftInnerBeam(float moveValueLeft) => Quaternion.Euler(moveValueLeft, leftInnerBeam.localRotation.eulerAngles.y, leftInnerBeam.localRotation.eulerAngles.z);
    private Quaternion MoveLeftOuterBeam(float moveValueLeft) => Quaternion.Euler(-moveValueLeft, leftOuterBeam.localRotation.eulerAngles.y, leftOuterBeam.localRotation.eulerAngles.z);
    private Quaternion MoveLeftFrontJoint(float moveValueLeft) => Quaternion.Euler(-moveValueLeft, leftFrontJoint.localRotation.eulerAngles.y, leftFrontJoint.localRotation.eulerAngles.z);

    private void MoveRight()
    {
        rightInnerBeam.localRotation = Quaternion.RotateTowards(rightInnerBeam.localRotation, MoveRightInnerBeam(moveValueRight), speedValue * Time.fixedDeltaTime);
        rightOuterBeam.localRotation = Quaternion.RotateTowards(rightOuterBeam.localRotation, MoveRightOuterBeam(moveValueRight), speedValue * Time.fixedDeltaTime);
        rightFrontJoint.localRotation = Quaternion.RotateTowards(rightFrontJoint.localRotation, MoveRightFrontJoint(moveValueRight), speedValue * Time.fixedDeltaTime);
    }

    private Quaternion MoveRightInnerBeam(float moveValueRight) => Quaternion.Euler(moveValueRight, rightInnerBeam.localRotation.eulerAngles.y, rightInnerBeam.localRotation.eulerAngles.z);
    private Quaternion MoveRightOuterBeam(float moveValueRight) => Quaternion.Euler(-moveValueRight, rightOuterBeam.localRotation.eulerAngles.y, rightOuterBeam.localRotation.eulerAngles.z);
    private Quaternion MoveRightFrontJoint(float moveValueRight) => Quaternion.Euler(-moveValueRight, rightFrontJoint.localRotation.eulerAngles.y, rightFrontJoint.localRotation.eulerAngles.z);

    private void HandleGripperObject(bool open)
    {
        if(gripperObject != null)
        {
            if (!open) gripperObject.transform.parent = tcp;
            else gripperObject.transform.parent = cadRoot;

            gripperObject.GetComponent<Rigidbody>().useGravity = open;
            gripperObject.GetComponent<Rigidbody>().isKinematic = !open;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // take a look a that!
using System;

public class FastIKFabric : MonoBehaviour
{
    //chain length of bones
    public int chainLength = 2;

    //Target the chain should bent to
    public Transform target;
    public Transform pole; // more advanced stuff

    [Header("Solver parameters")]
    public int iterations = 10;

    //Distance when solver stops
    public float delta = 0.001f;
    
    //strength of goind to the start position
    public float snapBackStrength = 1f;

    protected float[] bonesLength; // target to origin
    protected float completeLength;
    protected Transform[] bones;
    protected Vector3[] positions;

    protected Vector3[] startDirectionSucc;
    protected Quaternion[] startRotationBone;
    protected Quaternion startRotationTarget;
    protected Quaternion startRotationRoot;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        //initial arrays;
        bones = new Transform[chainLength + 1]; //adding one because bones point *---*---*
        positions = new Vector3[chainLength + 1];
        bonesLength = new float[chainLength]; // -- 2 ---3 ----4 (length of bones)

        startDirectionSucc = new Vector3[chainLength + 1];
        startRotationBone = new Quaternion[chainLength + 1];

        startRotationTarget = target.rotation;

        completeLength = 0; // add up all bonesLength

        Transform current = transform;
        for (int i = bones.Length - 1; i >= 0 ; i--)
        {
            bones[i] = current;

            startRotationBone[i] = current.rotation;

            if (i ==  bones.Length -1)
            {
                //leaf bone and it has no length
                startDirectionSucc[i] = target.position - current.position;
            } else
            {
                //mid bone
                startDirectionSucc[i] = bones[i + 1].position - current.position;
                bonesLength[i] = (bones[i + 1].position - current.position).magnitude; // теорема пифагора
                completeLength += bonesLength[i];
            }

            current = current.parent;
        }
    }

    private void LateUpdate()
    {
        ResolveIK();
    }

    private void ResolveIK()
    {
        if (target == null)
            return;

        if (bonesLength.Length != chainLength)
            Init();

        //Fabric IK algorithm
        
        //we do any computation on bones directly
        //get positions  
        for (int i = 0; i < bones.Length; i++)
        {
            positions[i] = bones[i].position;
        }

        var rootRot = (bones[0].parent != null) ? bones[0].parent.rotation : Quaternion.identity; // rotation root
        var rootRotDiff = rootRot * Quaternion.Inverse(startRotationRoot); // diffrence of rotation from start

        //Calculation
        // t     *--*--*
        if ((target.position - bones[0].position).sqrMagnitude >= completeLength * completeLength) //sqr for optimization way faster
        {
            //just stretch it 

            ///
            var direction = (target.position - bones[0].position).normalized;
            //eqally spead the length
            for (int i = 1; i < positions.Length; i++)
            {
                positions[i] = positions[i - 1] + direction * bonesLength[i - 1];
            }

//----------------Doesnt Work
            //---// For snake
            //bones[bones.Length - 1].position = target.position;
            ///---///
            // YOU JUST NEED TO DELETE THIS IF
 //-----
        }
        else
        {
            for (int iteration = 0; iteration < iterations; iteration++)
            {
                //back
                // but just more sign, so first bone wont be included
                for (int i = positions.Length - 1; i >= 0; i--) // i>0 skipping root bone  so we dont move it position
                {
                    if (i == positions.Length - 1)
                        positions[i] = target.position; // setting last one to target
                    else
                        positions[i] = positions[i + 1] + (positions[i] - positions[i + 1]).normalized * bonesLength[i];
                }

                //forward
                for (int i = 1; i < positions.Length; i++) // almost the same as back just reversed
                {
                    positions[i] = positions[i - 1] + (positions[i] - positions[i - 1]).normalized * bonesLength[i-1];
                }

                //close enough?
                if ((positions[positions.Length - 1] - target.position).sqrMagnitude < delta * delta)
                    break;
            }
        }

        //Pole
        if (pole != null)
        {
            for (int i = 1; i < positions.Length - 1; i++) // skip root and last one we can only rotate mids
            {
                var plane = new Plane(positions[i + 1] - positions[i - 1], positions[i-1] ); // normal and point // previos bone point is base for plane
                var projectedPole = plane.ClosestPointOnPlane(pole.position);
                var projectedBone = plane.ClosestPointOnPlane(positions[i]);
                var angle = Vector3.SignedAngle(projectedBone - positions[i-1], projectedPole - positions[i-1], plane.normal);// 45 degrees for example
                positions[i] = Quaternion.AngleAxis(angle, plane.normal) * (positions[i] - positions[i - 1]) + positions[i - 1];
            }
        }

        //set position and rotation
        for (int i = 0; i < positions.Length; i++)
        {
            if (i == positions.Length - 1)
            {
                bones[i].rotation = target.rotation * Quaternion.Inverse(startRotationTarget) * startRotationBone[i];
            } else
            {
                bones[i].rotation = Quaternion.FromToRotation(startDirectionSucc[i], positions[i+1] - positions[i]) * startRotationBone[i];
            }
            bones[i].position = positions[i];
        }
    }
    
    private void OnDrawGizmos()
    {
        var current = transform;
        for (int i = 0; i < chainLength && current != null && current.parent != null; i++)
        {
            float scale = Vector3.Distance(current.position, current.parent.position) * 0.1f;
            Handles.matrix = Matrix4x4.TRS(current.position, Quaternion.FromToRotation(Vector3.up, current.parent.position - current.position),
                new Vector3(scale, scale / 0.1f, scale));
            Handles.color = Color.green;
            Handles.DrawWireCube(Vector3.up * 0.5f, Vector3.one);
            current = current.parent;
        }
    }
}

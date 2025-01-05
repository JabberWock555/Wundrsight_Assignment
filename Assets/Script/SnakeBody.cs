using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    //[SerializeField] private int length = 30;
    // [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Vector3[] segmentPoses;
    [SerializeField] private Vector3[] segmentV;

    [SerializeField] private RectTransform targetDirection;
    [SerializeField] private float targetDistance = 0.25f;
    [SerializeField] private float smoothSpeed = 10f;

    [SerializeField] private float wiggleSpeed = 1f;
    [SerializeField] private float wiggleMagnitude = 0.25f;
    [SerializeField] private RectTransform WiggleDirection;

    [SerializeField] private Transform[] bodyParts;


    void Start()
    {
        //lineRenderer.positionCount = length;
        segmentPoses = new Vector3[bodyParts.Length];
        segmentV = new Vector3[bodyParts.Length];
        ResetPosition();
    }

    void Update()
    {
        WiggleDirection.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        segmentPoses[0] = targetDirection.position;
        for (int i = 1; i < bodyParts.Length; i++)
        {
            Vector3 targetPos = segmentPoses[i - 1] + (segmentPoses[i] - segmentPoses[i - 1]).normalized * targetDistance;
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPos, ref segmentV[i], smoothSpeed);
            bodyParts[i - 1].position = segmentPoses[i];
        }
        //lineRenderer.SetPositions(segmentPoses);
    }

    private void ResetPosition()
    {
        segmentPoses[0] = targetDirection.position;
        for (int i = 1; i < bodyParts.Length; i++)
        {
            segmentPoses[i] = segmentPoses[i - 1] + targetDirection.up * targetDistance;
            bodyParts[i].position = segmentPoses[i];
        }
    }
}

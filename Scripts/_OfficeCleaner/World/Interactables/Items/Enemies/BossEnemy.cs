using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private float rotationTimeS = 0.25f;
    [SerializeField]
    private float speed;

    [Space]
    [SerializeField]
    private GameObject targetWaypoints;

    #endregion

    #region Propeties

    private List<Vector3> WaypointsPositions { get; set; } = new List<Vector3>();
    private Vector3 NextWaypoint { get; set; } = Vector3.zero;
    private int NextWaypointIndex { get; set; } = 0;

    #endregion

    #region Methods

    public void OnTriggerEnterRange()
    {
        Debug.Log("todo; attack!");
    }

    private void Start()
    {
        int waypointsCount = targetWaypoints.transform.childCount;
        for (int i = 0; i < waypointsCount; i++)
        {
            Transform childTransform = targetWaypoints.transform.GetChild(i);
            WaypointsPositions.Add(new Vector3(childTransform.position.x, transform.position.y, childTransform.position.z));
        }

        ChangeWaypoint();
    }

    private void Update()
    {
        Vector3 move = (NextWaypoint - transform.position).normalized * speed * Time.deltaTime;
        transform.position = transform.position + new Vector3(move.x, Constants.DEFAULT_VALUE, move.z);

        if (Vector3.Distance(transform.position, NextWaypoint) < 0.05f)
        {
            ChangeWaypoint();
        }
    }

    private void ChangeWaypoint()
    {
        if(WaypointsPositions.Count <= NextWaypointIndex)
        {
            NextWaypointIndex = 0;
        }

        NextWaypoint = WaypointsPositions[NextWaypointIndex];

        Quaternion lastRotation = transform.rotation;
        transform.LookAt(NextWaypoint);
        Quaternion targetRotation = transform.rotation;
        transform.rotation = lastRotation;
        transform.DORotateQuaternion(targetRotation, rotationTimeS);

        NextWaypointIndex++;
    }

    #endregion

    #region Enums



    #endregion
}
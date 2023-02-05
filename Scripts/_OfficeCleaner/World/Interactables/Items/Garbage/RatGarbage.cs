using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class RatGarbage : GarbageBase
{
    #region Fields

    [SerializeField]
    private GameObject targetWaypoints;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationTimeS = 0.25f;

    #endregion

    #region Propeties

    private List<Vector3> Waypoints { get; set; } = new List<Vector3>();
    private Vector3 NextWaypoint { get; set; } = Vector3.zero;

    #endregion

    #region Methods

    protected override void OnPlayerEnter(PlayerCharacterController player)
    {
        base.OnPlayerEnter(player);

        if (CanInteract() == true)
        {
            DestroyGarbage();
        }
    }

    private void Start()
    {
        int waypointsCount = targetWaypoints.transform.childCount;
        for (int i = 0; i < waypointsCount; i++)
        {
            Transform childTransform = targetWaypoints.transform.GetChild(i);
            Waypoints.Add(new Vector3(childTransform.position.x, transform.position.y, childTransform.position.z));
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
        NextWaypoint = Waypoints.GetRandomElement();

        Quaternion lastRotation = transform.rotation;
        transform.LookAt(NextWaypoint);
        Quaternion targetRotation = transform.rotation;
        transform.rotation = lastRotation;
        transform.DORotateQuaternion(targetRotation, rotationTimeS);
    }

    #endregion

    #region Enums



    #endregion
}

using UnityEngine;
using System.Collections;

public class UICameraFollower : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Vector3 rotationModifier = Vector3.one;

    #endregion

    #region Propeties

    public Vector3 RotationModifier { 
        get => rotationModifier; 
    }

    private Camera CachedCamera { get; set; }

    #endregion

    #region Methods

    private void Awake()
    {
        CachedCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Quaternion cameraRotation = CachedCamera.transform.rotation;
        transform.LookAt(transform.position + cameraRotation * Vector3.forward, cameraRotation * Vector3.up);

        transform.rotation = new Quaternion(transform.rotation.x * RotationModifier.x, transform.rotation.y * RotationModifier.y, transform.rotation.z * RotationModifier.z, transform.rotation.z);
    }

    #endregion

    #region Enums



    #endregion
}

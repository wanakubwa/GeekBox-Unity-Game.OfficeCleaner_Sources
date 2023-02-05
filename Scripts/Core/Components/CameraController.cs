using GeekBox.Scripts.Generic;
using UnityEngine;

public class CameraController : SingletonBase<CameraController>
{
    #region Fields

    [SerializeField]
    private Camera currentCamera;

    #endregion

    #region Propeties

    public Camera CurrentCamera {
        get => currentCamera;
    }

    private PlayerCharacterController Target { get; set; }

    #endregion

    #region Methods

    private void Start()
    {
        Target = FindObjectOfType<PlayerCharacterController>();
    }

    private void OnEnable()
    {
        if(PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnCharacterSpawned += OnCharacterSpawned;
        }
    }
    private void OnDisable()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnCharacterSpawned -= OnCharacterSpawned;
        }
    }

    private void Update()
    {
        if(Target == null) { return; }

        transform.position = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);
    }

    private void OnCharacterSpawned(PlayerCharacterController player)
    {
        Target = player;
    }

    #endregion

    #region Enums



    #endregion
}

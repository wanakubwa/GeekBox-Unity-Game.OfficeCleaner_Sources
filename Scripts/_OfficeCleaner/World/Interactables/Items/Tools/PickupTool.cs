using UnityEngine;
using System.Collections;

public class PickupTool : Interactable
{
    #region Fields

    [SerializeField]
    private ToolType typeOfTool;
    [SerializeField]
    private GameObject modelRoot;

    #endregion

    #region Propeties

    public ToolType TypeOfTool {
        get => typeOfTool;
    }
    public GameObject ModelRoot {
        get => modelRoot; 
    }

    #endregion

    #region Methods

    protected override void OnPlayerEnter(PlayerCharacterController player)
    {
        base.OnPlayerEnter(player);

        if(player.Tool.CurrentTool != TypeOfTool)
        {
            player.PickUpTool(TypeOfTool);
            GameplayEvents.Instance.NotifyPickupTool(TypeOfTool);
        }
    }

    private void OnEnable()
    {
        GameplayEvents.Instance.OnToolPickup += OnToolPickupHandler;
    }

    private void OnDisable()
    {
        if (GameplayEvents.Instance != null)
        {
            GameplayEvents.Instance.OnToolPickup -= OnToolPickupHandler;
        }
    }

    private void OnToolPickupHandler(ToolType obj)
    {
        ModelRoot.SetActive(obj != TypeOfTool);
    }

    #endregion

    #region Enums



    #endregion
}

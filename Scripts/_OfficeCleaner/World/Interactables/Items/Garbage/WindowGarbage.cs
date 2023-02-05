using UnityEngine;

public class WindowGarbage : GarbageBase
{
    #region Fields

    [SerializeField]
    private MeshFilter windowModelFilter;
    [SerializeField]
    private Mesh cleanWindoModel;
    [SerializeField]
    private int stainsAmmount = 6;

    #endregion

    #region Propeties

    public int StainsAmmount { get => stainsAmmount; }

    #endregion

    #region Methods

    protected override void OnPlayerEnter(PlayerCharacterController player)
    {
        base.OnPlayerEnter(player);

        if(CanInteract() == true)
        {
            PlayerManager.Instance.CurrentPlayer.IsInputEnabled = false;
            PopUpManager.Instance.ShowWindowGarbagePopUp(StainsAmmount, OnCleaningCompleted);
        }
    }

    protected override void DestroyGarbage()
    {
        windowModelFilter.sharedMesh = cleanWindoModel;

        base.DestroyGarbage();
    }

    private void OnCleaningCompleted()
    {
        PlayerManager.Instance.CurrentPlayer.IsInputEnabled = true;
        DestroyGarbage();
    }

    #endregion

    #region Enums



    #endregion
}

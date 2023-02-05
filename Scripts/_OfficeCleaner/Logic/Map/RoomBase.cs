using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomBase : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Transform garbageParent;

    [Space]
    [SerializeField]
    private Gradient flootChangeGradient;
    [SerializeField]
    private string materialColorFieldName = "_Color";
    [SerializeField]
    private MeshRenderer[] floorRenderers;

    #endregion

    #region Propeties

    public MeshRenderer[] FloorRenderers { get => floorRenderers; }

    public List<GarbageBase> RoomGarbages { get; private set; } = new List<GarbageBase>();
    private MaterialPropertyBlock MeshShaderBlock { get; set; }
    private int MaxGarbagesAmmount { get; set; } = Constants.DEFAULT_VALUE;

    #endregion

    #region Methods

    public virtual void Init()
    {
        MeshShaderBlock = new MaterialPropertyBlock();

        if (garbageParent != null)
        {
            RoomGarbages = garbageParent.GetComponentsInChildren<GarbageBase>().ToList();
        }

        MaxGarbagesAmmount = RoomGarbages.Count;
        RefreshFloorColor();
    }

    public void OnRemoveGarbage(int garbageId)
    {
        RoomGarbages.RemoveElementByID(garbageId);
        RefreshFloorColor();
        Debug.LogFormat("Zostalo {0} smeci w pokoju.", RoomGarbages.Count);
    }

    private void RefreshFloorColor()
    {
        float gradientKeyNormalized = (1 - (float)RoomGarbages.Count / MaxGarbagesAmmount);
        Color newFloorColor = flootChangeGradient.Evaluate(gradientKeyNormalized);

        MeshShaderBlock.SetColor(materialColorFieldName, newFloorColor);
        //FloorRenderers.ForEach(x => x.SetPropertyBlock(MeshShaderBlock));
    }

    #endregion

    #region Enums



    #endregion
}

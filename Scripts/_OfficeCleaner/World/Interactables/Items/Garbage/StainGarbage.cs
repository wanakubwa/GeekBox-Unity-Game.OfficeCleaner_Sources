using UnityEngine;
using System.Collections;

public class StainGarbage : GarbageBase
{
    #region Fields

    [SerializeField]
    private float maxDurability = 10f;
    [SerializeField]
    private string shaderAlphaPropertyName = "_ModelAlpha";
    [SerializeField]
    private GameObject particlesObj;
    [SerializeField]
    private GameObject particlesSpark;

    #endregion

    #region Propeties

    private float CurrentDurability { get; set; }
    private Vector3 LastPlayerPosition { get; set; } = Vector3.zero;
    private MaterialPropertyBlock MeshShaderBlock { get; set; }

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();

        MeshShaderBlock = new MaterialPropertyBlock();
        MainRenderer.SetPropertyBlock(MeshShaderBlock);

        CurrentDurability = maxDurability;
    }

    protected override void OnPlayerEnter(PlayerCharacterController player)
    {
        base.OnPlayerEnter(player);

        LastPlayerPosition = player.transform.position;
    }

    protected override void OnPlayerStay()
    {
        base.OnPlayerStay();

        // == false
        if (!CanInteract()) { return; }

        float positionDelta = Vector3.Distance(LastPlayerPosition, Player.transform.position);
        LastPlayerPosition = Player.transform.position;

        if (positionDelta >= Mathf.Epsilon)
        {
            CurrentDurability = Mathf.Clamp(CurrentDurability - positionDelta, Constants.DEFAULT_VALUE, float.MaxValue);
            RefreshVisualization();
            particlesObj.SetActive(true);
        }
        else
        {
            particlesObj.SetActive(false);
        }
    }

    protected override void OnPlayerExit(PlayerCharacterController player)
    {
        base.OnPlayerExit(player);

        particlesObj.SetActive(false);
    }

    private void RefreshVisualization()
    {
        //Opacity ze wzgledu na durability.
        float opacityNormalized = CurrentDurability / maxDurability;
        MeshShaderBlock.SetFloat(shaderAlphaPropertyName, opacityNormalized);
        MainRenderer.SetPropertyBlock(MeshShaderBlock);

        if (opacityNormalized == Constants.DEFAULT_VALUE)
        {
            Instantiate(particlesSpark,transform.position, particlesSpark.transform.rotation);
            DestroyGarbage();
            
        }
    }

    #endregion

    #region Enums



    #endregion
}

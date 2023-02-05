using QuickOutline;
using UnityEngine;

public abstract class GarbageBase : Interactable
{
    #region Fields

    [SerializeField]
    private ToolType requireTool;
    [SerializeField]
    private GarbageType typeOfGarbage;
    [SerializeField]
    private MeshRenderer mainRenderer;
    [SerializeField]
    private GameObject rootObject;

    [Header("Outline Settings")]
    [SerializeField]
    private Outline meshOutline;
    [SerializeField]
    private Color negativeColor = Color.red;
    [SerializeField]
    private Color positiveColor = Color.green;

    #endregion

    #region Propeties

    public ToolType RequireTool {
        get => requireTool;
    }
    protected MeshRenderer MainRenderer {
        get => mainRenderer;
    }
    public GameObject RootObject { 
        get => rootObject;
    }
    public GarbageType TypeOfGarbage { 
        get => typeOfGarbage; 
    }
    public Outline MeshOutline {
        get => meshOutline;
    }
    public Color NegativeColor {
        get => negativeColor;
    }
    public Color PositiveColor {
        get => positiveColor;
    }

    public RoomBase RoomOwner { get; set; }

    #endregion

    #region Methods

    protected bool CanInteract()
    {
        return IsInteractable == true && Player.Tool.CurrentTool == RequireTool;
    }

    protected virtual void OnPlayerStay()
    {

    }

    protected override void OnPlayerEnter(PlayerCharacterController player)
    {
        base.OnPlayerEnter(player);

        if(CanInteract() == false)
        {
            OnCanNoInteract();
        }
    }

    protected override void OnPlayerExit(PlayerCharacterController player)
    {
        base.OnPlayerExit(player);

        if(MeshOutline != null)
        {
            MeshOutline.OutlineColor = PositiveColor;
        }
    }

    protected virtual void DestroyGarbage()
    {
        IsInteractable = false;
        Destroy(RootObject);

        // To mozna spiac w jedno.
        RoomOwner.OnRemoveGarbage(ID);
        MapsManager.Instance.CurrentMap.OnRemoveGarbage(TypeOfGarbage);
        HapticsManager.Instance.TryVibrate(HapticsManager.Instance.GateAmplitude);
    }

    protected override void Awake()
    {
        base.Awake();

        RoomOwner = GetComponentInParent<RoomBase>();
    }

    private void OnCanNoInteract()
    {
        if (MeshOutline != null)
        {
            MeshOutline.OutlineColor = NegativeColor;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerCharacterController player = other.gameObject.GetComponent<PlayerCharacterController>();
        if (player != null)
        {
            OnPlayerStay();
        }
    }

    #endregion

    #region Enums



    #endregion
}

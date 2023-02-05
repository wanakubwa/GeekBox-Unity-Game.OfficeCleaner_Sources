using UnityEngine;

public class TutorialWindowGarbage : WindowGarbage
{
    #region Fields



    #endregion

    #region Propeties

    private Sprite GarbageSprite { get; set; }

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();

        GarbageSprite = InteractablesSettings.Instance.GetToolDefinition(RequireTool).IconSprite;
    }

    protected override void OnPlayerEnter(PlayerCharacterController player)
    {
        base.OnPlayerEnter(player);

        if (CanInteract() == false)
        {
            Player.BubbleUI.ShowTool(GarbageSprite);
        }
    }

    #endregion

    #region Enums



    #endregion
}

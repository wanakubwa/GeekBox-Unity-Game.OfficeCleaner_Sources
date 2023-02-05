using System;
using UnityEngine;

[Serializable]
public class ToolController
{
    #region Fields

    #endregion

    #region Propeties

    public ToolType CurrentTool { get; private set; } = ToolType.NONE;

    #endregion

    #region Methods

    public void SetToolType(ToolType toolType)
    {
        CurrentTool = toolType;
    }

    #endregion

    #region Enums



    #endregion
}

using Sirenix.OdinInspector;
using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationController
{
    #region Fields

    [SerializeField]
    private Animator currentAnimator;
    [SerializeField]
    private Transform toolParent;
    [SerializeField]
    private string idleBoolName;
    [SerializeField]
    private GameObject runParticles;
    [SerializeField]
    private SerializableDictionary<ToolType, ToolVisualizationData> animatorStatesMap = new SerializableDictionary<ToolType, ToolVisualizationData>();

    #endregion

    #region Propeties

    public SerializableDictionary<ToolType, ToolVisualizationData> AnimatorStatesMap
    {
        get => animatorStatesMap;
    }
    public string IdleBoolName {
        get => idleBoolName;
    }
    public Transform ToolParent { 
        get => toolParent;
    }
    public GameObject RunParticles { 
        get => runParticles;
    }

    private string LastBoolParameterName { get; set; } = string.Empty;
    public GameObject CurrentSpawnedTool { get; set; } = null;

    #endregion

    #region Methods

    public void SetIdle(bool isIdle)
    {
        currentAnimator.SetBool(IdleBoolName, isIdle);
    }

    public void RefreshAnimationState(ToolType typeOfTool)
    {
        if(string.IsNullOrEmpty(LastBoolParameterName) == false)
        {
            currentAnimator.SetBool(LastBoolParameterName, false);
        }

        if(AnimatorStatesMap.TryGetValue(typeOfTool, out ToolVisualizationData toolData) == true)
        {
            currentAnimator.SetBool(toolData.RunBoolName, true);
            LastBoolParameterName = toolData.RunBoolName;
        }
    }

    public void SpawnTool(ToolType typeOfTool)
    {
        if(CurrentSpawnedTool != null)
        {
            GameObject.Destroy(CurrentSpawnedTool.gameObject);
        }

        if (AnimatorStatesMap.TryGetValue(typeOfTool, out ToolVisualizationData toolData) == true)
        {
            CurrentSpawnedTool = GameObject.Instantiate(toolData.VisualizationPrefab);
            CurrentSpawnedTool.transform.ResetParent(ToolParent);
        }
    }

    #endregion

    #region Enums

    [Serializable]
    public class ToolVisualizationData
    {
        [field: SerializeField]
        public string RunBoolName { get; set; }
        [field: SerializeField]
        public GameObject VisualizationPrefab { get; set; }
    }

    #endregion
}

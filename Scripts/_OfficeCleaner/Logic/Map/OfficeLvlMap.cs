using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OfficeLvlMap : LvlMap
{
    #region Fields
    
    [SerializeField]
    private float lvlTimeLimitS = 60f;
    [SerializeField]
    private Transform roomsParent;

    #endregion

    #region Propeties

    public event Action<GarbageType> OnUpdateLvlGarbages = delegate { };
    public event Action<float> OnTimeAdded = delegate { };

    public float LvlTimeLimitS { get => lvlTimeLimitS; }
    public float LvlLeftTimeS { get; private set; }

    // Variables.
    public StorageRoom Storage { get; set; }
    public Dictionary<GarbageType, int> Garbages { get; private set; } = new Dictionary<GarbageType, int>();
    public List<RoomBase> Rooms { get; set; } = new List<RoomBase>();

    #endregion

    #region Methods

    public int GetGarbageAmmount(GarbageType garbage)
    {
        Garbages.TryGetValue(garbage, out int count);
        return count;
    }

    public override void Init(MapsManager mapsManager)
    {
        base.Init(mapsManager);

        // Inicjalizacja pomieszczen.
        Rooms = roomsParent.GetComponentsInChildren<RoomBase>().ToList();
        for (int i = 0; i < Rooms.Count; i++)
        {
            Rooms[i].Init();
        }

        Storage = GetComponentInChildren<StorageRoom>();
        InitializeGarbages();

        LvlLeftTimeS = LvlTimeLimitS;
    }

    public void AddTime(float timeToAddS)
    {
        LvlLeftTimeS += timeToAddS;
        OnTimeAdded(timeToAddS);
    }

    public void OnGameStop()
    {
        StopAllCoroutines();
    }

    public void OnGameStart()
    {
        StartCoroutine(_TimeWatchdog());
    }

    public void OnRemoveGarbage(GarbageType garbage)
    {
        if(Garbages.TryGetValue(garbage, out int count) == true)
        {
            Garbages[garbage] = --count;
            OnUpdateLvlGarbages(garbage);
        }
        else
        {
            Debug.LogError("Proba usuniecia elementu, ktorego nie ma w kolekcji smieci poziomu! " + garbage);
        }

        CheckWinConditions();
    }

    private void InitializeGarbages()
    {
        GarbageBase[] allGarbages = GetComponentsInChildren<GarbageBase>();
        foreach (GarbageBase garbage in allGarbages)
        {
            if(Garbages.TryGetValue(garbage.TypeOfGarbage, out int count) == true)
            {
                Garbages[garbage.TypeOfGarbage] = ++count;
            }
            else
            {
                Garbages.Add(garbage.TypeOfGarbage, 1);
            }            
        }
    }

    private void CheckWinConditions()
    {
        int mapGarbageCount = Constants.DEFAULT_VALUE;
        foreach (var garbage in Garbages)
        {
            mapGarbageCount += garbage.Value;
        }

        if(mapGarbageCount == Constants.DEFAULT_VALUE)
        {
            GamePlayManager.Instance.LvlSuccess();
        }
    }

    private void OnLvlFailed()
    {
        GamePlayManager.Instance.LvlFailed();
    }

    private IEnumerator _TimeWatchdog()
    {
        while (true)
        {
            LvlLeftTimeS -= Time.deltaTime;
            if (LvlLeftTimeS <= Constants.DEFAULT_VALUE)
            {
                OnLvlFailed();
                yield break;
            }
            else
            {
                GameplayEvents.Instance.NotifiMapTimeUpdate(LvlLeftTimeS);
            }

            yield return null;
        }
    }

    #endregion

    #region Enums



    #endregion
}

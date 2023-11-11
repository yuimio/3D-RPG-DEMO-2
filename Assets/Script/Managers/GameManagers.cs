using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManagers : Singleton<GameManagers>
{
    public CharacterStats playerStats;

    public CinemachineFreeLook followCamera;

    List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void RegisterPlayer(CharacterStats player)
    {
        playerStats = player;

        followCamera = FindObjectOfType<CinemachineFreeLook>();

        if (followCamera != null)
        {
            followCamera.Follow = playerStats.transform.GetChild(2);
            followCamera.LookAt = playerStats.transform.GetChild(2);
        }
    }

    public void AddOberserver(IEndGameObserver observer)
    {
        endGameObservers.Add(observer);
    }

    public void RemoveOberserver(IEndGameObserver observer)
    {
        endGameObservers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in endGameObservers)
        {
            observer.EndNotify();
        }
    }

    public Transform GetEntrance()
    {
        foreach (var item in FindObjectsOfType<TransitionDestination>())
        {
            if (item.destinationTag == TransitionDestination.DestinationTag.ENTER)
                return item.transform;
        }
        return null;
    }
}

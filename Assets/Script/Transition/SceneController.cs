using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System;

public class SceneController : Singleton<SceneController>,IEndGameObserver
{
    public GameObject playerPrefab;
    public SceneFader sceneFaderPrefab;
    bool fadeFinished;

    GameObject player;
    NavMeshAgent playerAgent;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

     void Start()
    {
        GameManagers.Instance.AddOberserver(this);
        fadeFinished = true;
    }

    public void TransitionToDestination(TransitionPoint transitionPoint)
    {
        switch (transitionPoint.transitionType)
        {
            case TransitionPoint.TransitionType.SameScene:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitionPoint.destinationTag));
                break;
            case TransitionPoint.TransitionType.DifferentScene:
                StartCoroutine(Transition(transitionPoint.sceneName, transitionPoint.destinationTag));
                break;
        }

    }
    IEnumerator Transition(string sceneName, TransitionDestination.DestinationTag destinationTag)
    {
        //todo��������
        SaveManager.Instance.SavePlayerData();
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return Instantiate(playerPrefab, GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
            SaveManager.Instance.LoadPlayerData();
            yield break;
        }
        else
        {
            player = GameManagers.Instance.playerStats.gameObject;
            playerAgent = player.GetComponent<NavMeshAgent>();
            playerAgent.enabled = false;
            player.transform.SetPositionAndRotation(GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
            playerAgent.enabled = true;
            yield return null;
        }
    }

    private TransitionDestination GetDestination(TransitionDestination.DestinationTag destinationTag)
    {
        var entrances = FindObjectsOfType<TransitionDestination>();
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].destinationTag == destinationTag)
                return entrances[i];
        }

        return null;
    }

    public void TransitionToMain()
    {
        StartCoroutine(LoadMain());
    }

    public void TransitionToLoadGame()
    {
        StartCoroutine(LoadLevel(SaveManager.Instance.SceneName));
    }

    public  void TransitionToFirstLevel()
        {
            StartCoroutine(LoadLevel("Game"));
        }

    IEnumerator LoadLevel(string scene)
    {
        SceneFader fade = Instantiate(sceneFaderPrefab);
        if (scene != "")
        {
            yield return StartCoroutine(fade.FadeOut(0.5f));
            yield return SceneManager.LoadSceneAsync(scene);
            yield return player = Instantiate(playerPrefab, GameManagers.Instance.GetEntrance().position,
                GameManagers.Instance.GetEntrance().rotation);

            SaveManager.Instance.SavePlayerData();
            yield return StartCoroutine(fade.FadeIn(0.5f));
            yield break;
        }
    }
    
    IEnumerator LoadMain()
    {
        SceneFader fade = Instantiate(sceneFaderPrefab);
        yield return StartCoroutine(fade.FadeOut(2f));
        yield return SceneManager.LoadSceneAsync("Main");
        yield return StartCoroutine(fade.FadeIn(2f));
        yield break;
    }

    public void EndNotify()
    {
        if (fadeFinished)
        {
            fadeFinished = false;
            StartCoroutine(LoadMain());
        }
    }
}

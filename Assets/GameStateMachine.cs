using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameStateMachine : MonoBehaviour
{
    public event Action HandleBeginGame;
    //public event Action HandleGoToLoading;
    public event Action<IControlPlayers> HandleAddingPlayersToGame;

    public static GameStateMachine instance;

    private StateMachine gameStateMachine;
    private Menu menu;
    private Loading loading;
    private Play play;
    private bool goToLoading;

    public void ResetGameStateBools()
    {
        goToLoading = false;
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        gameStateMachine = new StateMachine();
        menu = new Menu();
        loading = new Loading();
        play = new Play();

        gameStateMachine.AddTransition(menu, loading, () => goToLoading);
        gameStateMachine.AddTransition(loading, play, () => loading.isFinished);
    }
    private void Start()
    {


        gameStateMachine.SetState(menu);

    }

    private void Update()
    {
        gameStateMachine.Tick();
    }
    public void AddAPlayer(IControlPlayers controller)
    {
        HandleAddingPlayersToGame?.Invoke(controller);
    }
    public void BeginGame()
    {
        HandleBeginGame?.Invoke();
    }
    public void LoadingSceneLoad()
    {
        goToLoading = true;
        //HandleGoToLoading?.Invoke();

    }
}

public class Loading : Istate
{
    private AsyncOperation operation;
    public bool isFinished => operation.isDone;
    public void OnEnter()
    {

        operation = SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Additive);
    }

    public void ONExit()
    {
    }

    public void OnTick()
    {
    }
}

public class Menu : Istate
{
    public void OnEnter()
    {


    }


    public void ONExit()
    {
        GameStateMachine.instance.ResetGameStateBools();
    }

    public void OnTick()
    {

    }
}
public class Play : Istate
{
    bool GameBegun = false;
    public void OnEnter()
    {
        if (!GameBegun)
        {

            GameStateMachine.instance.BeginGame();
            GameBegun = true;
        }
    }

    public void ONExit()
    {

    }

    public void OnTick()
    {

    }
}
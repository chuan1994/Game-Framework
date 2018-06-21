using UnityEngine;

public abstract class AbstractGameStateManager : MonoBehaviour {

    #region Serialized Fields

    /// <summary>
    /// The number of objectives required for the level to complete
    /// </summary>
    [SerializeField]
    private int ObjectiveCount;

    #region Serialized Audio Clip Fields

    /// <summary>
    /// The sound played on completing an objective
    /// </summary>
    [SerializeField]
    protected AudioClip ObjectiveMet;

    /// <summary>
    /// The sound played on losing an objective
    /// </summary>
    [SerializeField]
    protected AudioClip ObjectiveLost;

    /// <summary>
    /// The sound played on winning the game
    /// </summary>
    [SerializeField]
    protected AudioClip GameWon;

    /// <summary>
    /// The sound played on losing the game
    /// </summary>
    [SerializeField]
    protected AudioClip GameLost;

    #endregion Serialized Audio Clip Fields

    #endregion Serialized Fields

    #region Fields and Properties

    private GameStatus currentGameStatus = GameStatus.Pending;
    protected GameStatus CurrentGameStatus {
        get { return currentGameStatus; }
        set {
            if (currentGameStatus != GameStatus.Finalized) {
                currentGameStatus = value;
            }
        }
    }

    #endregion Fields and Properties

    #region Delegates
    public delegate void OnLevelComplete();
    public static event OnLevelComplete LevelCompleted;

    public delegate void OnLevelFailed();
    public static event OnLevelFailed LevelFailed;


    #endregion Delegates

    #region Unity Hook Points
    /// <summary>
    /// Method used for initialization by the unity framework
    /// </summary>
    void Start() {
        OnBeforeStart();
        OnAfterStart();
    }

    protected virtual void OnBeforeStart() { }
    protected virtual void OnAfterStart() { }

    /// <summary>
    /// Method used for updating. Called by they unity framework
    /// This implementation will call the appropriate event based on the current game status
    /// </summary>
    void Update() {
        OnBeforeUpdate();

        //Core update function
        switch (CurrentGameStatus) {
            case GameStatus.Win:
                if (LevelCompleted != null) {
                    LevelCompleted();
                }
                break;
            case GameStatus.Loss:
                if (LevelFailed != null) {
                    LevelFailed();
                }
                break;
        }

        OnAfterUpdate();
    }

    protected virtual void OnBeforeUpdate() { }
    protected virtual void OnAfterUpdate() { }

    #endregion Unity Hook Points

    protected enum GameStatus {
        Win,
        Loss,
        Pending,
        Finalized
    }
}


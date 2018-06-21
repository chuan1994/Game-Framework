﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateManager : MonoBehaviour {

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

    private bool gameWon;


    #endregion Fields and Properties

    #region Delegates
    public delegate void OnLevelComplete();
    public static event OnLevelComplete LevelComplete;

    public delegate void OnLevelFailed();
    public static event OnLevelFailed LevelFailed;


    #endregion Delegates

    #region Unity Hook Points
    /// <summary>
    /// Method used for initialization by the unity framework
    /// </summary>
    void Start() {
        OnBeforeStart();
        gameWon = false;
        OnAfterStart();
    }

    protected virtual void OnBeforeStart() { }
    protected virtual void OnAfterStart() { }

    /// <summary>
    /// Method used for updating. Called by they unity framework
    /// This implementation will call the <see cref="LevelComplete"/> event if <see cref="gameWon"/> is set to true
    /// </summary>
    void Update() {
        OnBeforeUpdate();

        //Core update function
        if (gameWon) {
            gameWon = false;
            if (LevelComplete != null) {
                LevelComplete();
            }
        }

        OnAfterUpdate();
    }

    protected virtual void OnBeforeUpdate() { }
    protected virtual void OnAfterUpdate() { }

    #endregion Unity Hook Points
}


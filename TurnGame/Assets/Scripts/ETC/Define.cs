using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Unit,
        Monster,
    }

    public enum ScreenRatio
    {
        FullScreen = 0,
        Hd = 1,
        Fhd = 2,
        HdWidth = 1280,
        HdHeight = 720,
        FHDWidth = 1920,
        FHDHeight = 1080
    }

    public enum ScenePhase
    {
        StartPhase,
        BattlePhase,
        EndPhase,
        RewardPhase
    }

    public enum Language
    {
        Korean,
        English
    }

    public enum GameState
    {
        Play,
        Pause
    }

    public enum UnitNum
    {
        Player = 0,
        Unit = 1,
        Enemy = 10
    }

    public enum PlayerState
    {
        Idle,

    }

    public enum Layer
    {
        Ground = 6,
        Block = 7,
        Monster = 8
    }

    public enum InputEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click
    }

    public enum Sound
    {
        Bgm,
        SE,
        MaxCount
    }

    public enum CameraMode
    {
        QuarterView
    }

    public enum Scene
    {
        Unknown,
        Title,
        Stage1
    }

    public enum UIEvent
    {
        Click,
        Drag
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //변수선언=================================================================================
    static Managers     _Instance;
    static Managers Instance { get { Init(); return _Instance; } }

    #region Contents
    GameManager _game = new GameManager();

    public static GameManager Game { get { return Instance._game;  } }
    #endregion

    #region Core
    DataManager         _data = new DataManager();
    InputManager        _input = new InputManager();
    PoolManager         _pool = new PoolManager();
    ResourceManager     _resource = new ResourceManager();
    SceneManagerEx      _scene = new SceneManagerEx();
    UIManager           _ui = new UIManager();
    SoundManager        _sound = new SoundManager();

    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound;  } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource;  } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion


    //Start & Update =================================================================================
    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }


    //Init 함수 =================================================================================
    static void Init()
    {
        if(_Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
                //go.name = "@Managers";
            }

            DontDestroyOnLoad(go);
            _Instance = go.GetComponent<Managers>();

            _Instance._data.Init();
            _Instance._pool.Init();
            _Instance._sound.Init();
        }
        
    }


    //초기화 함수 =================================================================================
    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}

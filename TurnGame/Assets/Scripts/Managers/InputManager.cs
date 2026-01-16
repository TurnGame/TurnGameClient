using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action<KeyCode> KeyDownAction = null;
    public Action<KeyCode> KeyUpAction = null;
    public Action<Define.InputEvent> MouseAction = null;

    bool _pressed = false;
    float _pressedTime = 0;
    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Managers.Scene.CurrentScene.SceneType != Define.Scene.Title)
        {
            KeyDownAction?.Invoke(KeyCode.Escape);
            Managers.Game.ChangeGameState(Define.GameState.Pause);
        }

        if (KeyDownAction != null || KeyUpAction != null)
        {
            CheckKey(KeyCode.W);
            CheckKey(KeyCode.A);
            CheckKey(KeyCode.S);
            CheckKey(KeyCode.D);
        }

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                if(! _pressed)
                {
                    MouseAction.Invoke(Define.InputEvent.PointerDown);
                    _pressedTime = Time.time;
                }

                MouseAction.Invoke(Define.InputEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                {
                    if(Time.time < _pressedTime + 0.2f)
                        MouseAction.Invoke(Define.InputEvent.Click);
                    MouseAction.Invoke(Define.InputEvent.PointerUp);
                }
                    
                _pressed = false;
                _pressedTime = 0;
            }
        }
    }

    //키 체크 로직
    private void CheckKey(KeyCode key)
    {
        if (Input.GetKeyDown(key))
            KeyDownAction?.Invoke(key);

        if (Input.GetKeyUp(key))
            KeyUpAction?.Invoke(key);
    }

    public void Clear()
    {
        KeyDownAction = null;
        KeyUpAction = null;
        MouseAction = null;
    }
}

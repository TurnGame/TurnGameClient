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
        if (KeyDownAction != null || KeyUpAction != null)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                KeyDownAction.Invoke(KeyCode.Escape);
                //게임종료 작업
            }

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

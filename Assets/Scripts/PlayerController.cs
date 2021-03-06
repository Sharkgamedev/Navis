using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharkUtils;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Camera MainCamera;
    public Rigidbody2D PlayerRB;

    [Header("Cache")]
    private float _changeY;
    private float _changeX;
    private float _tCx, _tCy;
    private bool _facingRight;

    [Header("Movement")]
    public float YAccel;
    public float XAccel;
    public float XCap;
    public float YCap;
    public float Friction;
    public float ZeroThreshold;

    // Update is called once per frame
    void Update()
    {
        HandleMovment();
        _tCx = _changeX * 150; 
        _tCx *= Time.deltaTime;
        _tCy = _changeY * 150; 
        _tCy *= Time.deltaTime;

        Move();
    }

    #region Movement

    public void Teleport (string tP)
    {
        string[] pos = tP.Split('|');
        Vector3 realPos = new Vector3();
        for (int i = 0; i < 3; i++)
        {
            realPos[i] = float.Parse(pos[i]);
        }
        transform.position = realPos;
        realPos.z = -10.0f;
        Camera.main.transform.position = realPos;
    }

    public void HandleMovment()
    {
        HandleXAxisInput();
        HandleYAxisInput();
        ZeroIn();
    }

    public void Move()
    {
        PlayerRB.AddForce(new Vector2(_tCx, _tCy), ForceMode2D.Force);
    }

    public void HandleYAxisInput()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && _changeY < YCap)
        {
            _changeY += YAccel;
        }
        else if (_changeY > 0)
        {
            _changeY -= Friction;
        }

        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && _changeY > YCap * -1)
        {
            _changeY -= YAccel;
        }
        else if (_changeY < 0)
        {
            _changeY += Friction;
        }
    }

    public void HandleXAxisInput()
    {
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && _changeX < XCap)
        {
            _changeX += XAccel;
        }
        else if (_changeX > 0)
        {
            _changeX -= Friction;
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && _changeX > XCap * -1)
        {
            _changeX -= XAccel;
        }
        else if (_changeX < 0)
        {
            _changeX += Friction;
        }
    }

    public void ZeroIn()
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            if (_changeX > 0 && _changeX < ZeroThreshold)
                _changeX = 0;

            if (_changeX < 0 && _changeX > ZeroThreshold * -1)
                _changeX = 0;

            if (_changeY > 0 && _changeY < ZeroThreshold)
                _changeY = 0;

            if (_changeY < 0 && _changeY > ZeroThreshold * -1)
                _changeY = 0;
        }
    }

    #endregion
}

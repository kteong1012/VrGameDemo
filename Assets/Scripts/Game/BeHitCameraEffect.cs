using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeHitCameraEffect : MonoBehaviour
{
    private float _timer = 0f;
    public Material mat;

    private void Start()
    {
        MessageCenter.Instance.RegisterListner(GameMessageType.AttackPlayer, BeHit);
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
    }

    private void BeHit(MessageBase messageBase)
    {
        _timer = 0.1f;
    }

    private void OnDestroy()
    {
        MessageCenter.Instance.UnRegisterListner(GameMessageType.AttackPlayer, BeHit);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_timer>0)
        {
            Graphics.Blit(source, destination, mat);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 100f;
    public Material mat;

    private GameObject leftCamera;
    private GameObject rightCamera;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        MessageCenter.Instance.RegisterListner(GameMessageType.AttackPlayer, BeHit);
    }
    private void Update()
    {
        if (leftCamera == null)
        {
            leftCamera = GameObject.Find("Main Camera:Instant Preview Left");
            if (leftCamera)
            {
                BeHitCameraEffect effect = leftCamera.AddComponent<BeHitCameraEffect>();
                effect.mat = mat;
            }
        }
        if (rightCamera == null)
        {
            rightCamera = GameObject.Find("Main Camera:Instant Preview Right");
            if (rightCamera)
            {
                BeHitCameraEffect effect = rightCamera.AddComponent<BeHitCameraEffect>();
                effect.mat = mat;
            }
        }
    }
    private void BeHit(MessageBase message)
    {
        AttackPlayerMessage msg = (AttackPlayerMessage)message;
        health -= msg.damage;
        if (health <= 0)
        {
            MessageCenter.Instance.PostMessage(new GameOverMessage());
        }
    }

    private void OnDestroy()
    {
        MessageCenter.Instance.UnRegisterListner(GameMessageType.AttackPlayer, BeHit);
    }

  
}

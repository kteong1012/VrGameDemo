using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(EventTrigger))]
public class SimpleVrButton : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onConfirm;
    public UnityEvent onExit;
    public Text btnText;

    private const  float CONFIRM_TIME = 1f;
    private float _timer = 0;
    private string _textString;
    private void Start()
    {
        _textString = btnText.text;
    }
    public virtual void OnPointerEnter()
    {
        StopAllCoroutines();
        StartCoroutine(StartCountDown());
        onEnter?.Invoke();
    }
    public virtual void OnPointerExit()
    {
        StopAllCoroutines();
        btnText.text = _textString;
        onExit?.Invoke();
    }

    private IEnumerator StartCountDown()
    {
        _timer = CONFIRM_TIME;
        while (_timer > 0)
        {
            _timer -= Time.deltaTime;
            btnText.text = _timer.ToString("0.00");
            yield return null;
        }
        btnText.text = _textString;
        onConfirm?.Invoke();
    }
}

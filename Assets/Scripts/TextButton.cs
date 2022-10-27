using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TextButton : MonoBehaviour
{
    [SerializeField] private float threshold = .1f;
    [SerializeField] private float deadZone = .025f;

    public string targetTag;
    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;
    public GameObject uiObject;
    public UnityEvent onPressed, onReleased;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
        uiObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPressed && GetValue() + threshold >= .7)
            Pressed();
        if (isPressed && GetValue() - threshold <= 0)
            Released();
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;

        if (Math.Abs(value) < deadZone)
            value = 0;

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
        uiObject.SetActive(true);
     
    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
        uiObject.SetActive(false);
        Debug.Log("Released");
    }

}

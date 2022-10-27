using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Printer : MonoBehaviour
{

    [SerializeField] private float threshold = .1f;
    [SerializeField] private float deadZone = .025f;
    [SerializeField] private GameObject print;
    [SerializeField] private GameObject Spawner;

    public string targetTag;
    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;
    
    public UnityEvent onPressed, onReleased;

    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();

    }

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
        Instantiate(print, Spawner.transform.position, Spawner.transform.rotation);
    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
 
        Debug.Log("Released");
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    [SerializeField] private LayerMask acceptedLayers;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!IsOnLayer(col)) return;
        onTriggerEnter.Invoke();
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (!IsOnLayer(col)) return;
        onTriggerExit.Invoke();
    }

    private bool IsOnLayer(Collider2D collider)
    {
        int layerNumber = collider.gameObject.layer;
        return (1 << layerNumber & acceptedLayers) != 0;
    }
}

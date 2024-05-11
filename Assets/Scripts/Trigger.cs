using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    [SerializeField] private List<LayerMask> _acceptedLayers;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!_acceptedLayers.Contains(col.gameObject.layer)) return;
        
        onTriggerEnter.Invoke();
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (!_acceptedLayers.Contains(col.gameObject.layer)) return;
        
        onTriggerExit.Invoke();
    }
}

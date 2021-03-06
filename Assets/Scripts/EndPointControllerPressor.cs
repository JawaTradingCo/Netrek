﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EndPointControllerPressor : MonoBehaviour
{
    public bool isOn;
    [SerializeField]
    private PressorController pressorController;
    public CircleCollider2D endPointCollider;
    [SerializeField]
    public Transform shipTransform;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private PhotonView photonView;
    [SerializeField]
    public Vector2 point;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float maxRange = 2.25f;
    public Vector2 end;
    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Y) && photonView.IsMine)
        {
            if (!isOn)
            {
                point = cam.ScreenToWorldPoint(Input.mousePosition);
                direction = point - (Vector2)shipTransform.position;
                distance = Mathf.Clamp(Vector2.Distance((Vector2)shipTransform.position, point), 0, maxRange);
                end = (Vector2)shipTransform.position + (direction.normalized * distance);
                pressorController.toggle();
            }
            else
            {
                pressorController.toggle();                
            }
        }
        if (isOn && photonView.IsMine)
        {            
            direction = point - (Vector2)shipTransform.position;
            distance = Mathf.Clamp(Vector2.Distance((Vector2)shipTransform.position, point), 0, maxRange);
            end = (Vector2)shipTransform.position + (direction.normalized * distance);            
            transform.position = end;         
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.root.name != this.gameObject.transform.root.name)
        {
            point = collision.transform.position;
        }
    }
    
}

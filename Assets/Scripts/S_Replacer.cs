using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Replacer : MonoBehaviour
{
    private MeshRenderer renderer;
    [HideInInspector]
    public GameObject emptyTargetObject;
    [HideInInspector]
    public GameObject focusObject;
    bool move = true;
    [HideInInspector]
    public GameObject startPosition;
    Vector3 needPosition;
    float speed = 0.015f;
    float offset = 0;
    Quaternion startRotation;
    Quaternion needRotation;
    private bool shouldPlaceBackCamera = false;

    void Start()
    {
        try
        {
            renderer = focusObject.GetComponent<MeshRenderer>();
            emptyTargetObject = renderer.probeAnchor.gameObject;
            startRotation = transform.rotation; 
            needPosition = emptyTargetObject.transform.position;
            needRotation = emptyTargetObject.transform.rotation;
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }
    }
    void FixedUpdate()
    {
        try
        {
            if (move)
            {
                offset += speed;
                transform.position = Vector3.Lerp(startPosition.transform.localPosition, needPosition, offset);
                transform.rotation = Quaternion.Slerp(startRotation, needRotation, offset);
                StartCoroutine("PlaceBackCamera");
                if (offset >= 1)
                {
                    move = false;
                    offset = 0;
                }
            }
            else if (shouldPlaceBackCamera)
            {
                offset += speed;
                transform.position = Vector3.Lerp(needPosition, startPosition.transform.localPosition, offset);
                transform.rotation = Quaternion.Slerp(needRotation, startRotation, offset);
                if (offset >= 1)
                {
                    shouldPlaceBackCamera = false;
                    offset = 0;
                }
            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }
    }
    IEnumerator PlaceBackCamera()
    {
        yield return new WaitForSeconds(3f);
        shouldPlaceBackCamera = true;
    }
}

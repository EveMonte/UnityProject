using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ObjectModel
{
    public enum TypeOfObject
    {
        ROTATABLE_PART, MOVABLE_PART, SOURCE_SWITCH
    }
    private TypeOfObject objectType;

    public TypeOfObject ObjectType
    {
        get { return objectType; }
        set { objectType = value; }
    }


    private GameObject activePart;

    public GameObject ActivePart
    {
        get { return activePart; }
        set { activePart = value; }
    }

    private float minLimit;

    public float MinLimit
    {
        get { return minLimit; }
        set { minLimit = value; }
    }

    private float maxLimit;

    public  float MaxLimit
    {
        get { return maxLimit; }
        set { maxLimit = value; }
    }


    private Vector3 expectedPosition;

    public Vector3 ExpectedPosition
    {
        get { return expectedPosition; }
        set { expectedPosition = value; }
    }


    private Vector3 expectedRotation;

    public Vector3 ExpectedRotation
    {
        get { return expectedRotation; }
        set { expectedRotation = value; }
    }

    private GameObject cameraTargetObject;

    public GameObject CameraTargetObject
    {
        get { return cameraTargetObject; }
        set { cameraTargetObject = value; }
    }


    private string tipText;

    public string TipText
    {
        get { return tipText; }
        set { tipText = value; }
    }


    public S_ObjectModel(GameObject cameraTargetObject, TypeOfObject typeOfObject, float minLimit, string tipText)
    {
        try
        {
            ActivePart = GameObject.Find(cameraTargetObject.tag);
            CameraTargetObject = cameraTargetObject;
            ObjectType = typeOfObject;
            DefineExpectedPositions();


            switch (ObjectType)
            {
                case TypeOfObject.MOVABLE_PART:
                    MaxLimit = ActivePart.transform.localPosition.x > minLimit ? ActivePart.transform.localPosition.x : minLimit;
                    MinLimit = ActivePart.transform.localPosition.x < minLimit ? ActivePart.transform.localPosition.x : minLimit;
                    //Debug.Log(ActivePart.name + " Max: " + MaxLimit + " Min: " + MinLimit);
                    break;
                case TypeOfObject.ROTATABLE_PART:
                    MaxLimit = ActivePart.transform.eulerAngles.y;
                    MinLimit = minLimit;
                    ////Debug.Log(MinLimit + " " + MaxLimit);

                    break;
                case TypeOfObject.SOURCE_SWITCH:
                    MaxLimit = ActivePart.transform.eulerAngles.x;
                    MinLimit = minLimit;
                    break;
            }
            TipText = tipText;
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    public S_ObjectModel(GameObject cameraTargetObject, TypeOfObject typeOfObject, string tipText)
    {
        try
        {
            ActivePart = GameObject.Find(cameraTargetObject.tag);
            CameraTargetObject = cameraTargetObject;
            ObjectType = typeOfObject;
            //MaxLimit = ActivePart.transform.eulerAngles.x;
            TipText = tipText;
            DefineExpectedPositions();
            SetMinAndMaxLimits();
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    public void SetMinAndMaxLimits()
    {
        switch (ObjectType)
        {
            case TypeOfObject.MOVABLE_PART:
                MinLimit = ExpectedPosition.x;
                MaxLimit = ActivePart.transform.localPosition.x;
                break;
            case TypeOfObject.ROTATABLE_PART:
                MinLimit = ExpectedRotation.y;
                MaxLimit = ActivePart.transform.eulerAngles.y;
                break;
            case TypeOfObject.SOURCE_SWITCH:
                MinLimit = ExpectedRotation.x;
                MaxLimit = ActivePart.transform.eulerAngles.x;
                break;
        }
    }

    public void DefineExpectedPositions()
    {
        GameObject temp = GameObject.Find(cameraTargetObject.gameObject.tag);
        temp = temp.GetComponent<MeshRenderer>() == null ? temp : temp.GetComponent<MeshRenderer>().probeAnchor.gameObject;
        ExpectedPosition = temp.transform.localPosition;
        ExpectedRotation = temp.transform.eulerAngles;
    }

    public bool DefineCanWeMoveNext()
    {
        try
        {
            switch (ObjectType)
            {
                case TypeOfObject.ROTATABLE_PART: return ExpectedRotation.y - 5 < ActivePart.transform.eulerAngles.y && (ExpectedRotation.y + 5 > ActivePart.transform.eulerAngles.y);
                case TypeOfObject.MOVABLE_PART: return ExpectedPosition.x - 0.01f < ActivePart.transform.localPosition.x && (ExpectedPosition.x + 0.01f > ActivePart.transform.localPosition.x);
                case TypeOfObject.SOURCE_SWITCH: return ExpectedRotation.x - 5 < ActivePart.transform.eulerAngles.x && (ExpectedRotation.x + 5 > ActivePart.transform.eulerAngles.x);
            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }
        return false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_ArrowRotation : MonoBehaviour
{
    public enum TargetObject
    {
        AMPERMETER = 0, VOLTMETERVN, MILLIAMPERMETER, VOLTMETERVA
    };
    private float speed;
    public GameObject milliampermeterArrow;
    public GameObject ampermeterArrow;
    public GameObject voltmeterVa;
    public GameObject voltmeterVn;
    public Button restartButton;

    private Vector3 startRotationOfMilliampermeter;
    private Vector3 startRotationOfAmpermeter;
    private Vector3 startRotationOfVoltmeterVa;
    private Vector3 startRotationOfVoltmeterVn;
    private Vector3 endRotationOfMilliampermeter;
    private Vector3 endRotationOfAmpermeter;
    private Vector3 endRotationOfVoltmeterVa;
    private Vector3 endRotationOfVoltmeterVn;
    private float offset;
    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();
        ////Debug.Log(startRotationOfVoltmeterVa);
    }

    public void InitializeVariables()
    {
        try
        {
            offset = 0;
            speed = 1.5f;
            startRotationOfMilliampermeter = endRotationOfMilliampermeter = milliampermeterArrow.transform.eulerAngles;
            startRotationOfAmpermeter = endRotationOfAmpermeter = ampermeterArrow.transform.eulerAngles;
            startRotationOfVoltmeterVa = endRotationOfVoltmeterVa = voltmeterVa.transform.eulerAngles;
            startRotationOfVoltmeterVn = endRotationOfVoltmeterVn = voltmeterVn.transform.eulerAngles;
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            milliampermeterArrow.transform.eulerAngles = Vector3.Slerp(startRotationOfMilliampermeter, endRotationOfMilliampermeter, offset);
            ampermeterArrow.transform.eulerAngles = Vector3.Slerp(startRotationOfAmpermeter, endRotationOfAmpermeter, offset);
            voltmeterVn.transform.eulerAngles = Vector3.Slerp(startRotationOfVoltmeterVn, endRotationOfVoltmeterVn, offset);
            voltmeterVa.transform.eulerAngles = Vector3.Slerp(startRotationOfVoltmeterVa, endRotationOfVoltmeterVa, offset);
            offset += Time.deltaTime * speed;
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
    public void SetNewRotation(Vector3 newEndRotation, TargetObject targetObject)
    {
        try
        {
            switch (targetObject)
            {
                case TargetObject.MILLIAMPERMETER:
                    endRotationOfMilliampermeter = newEndRotation;
                    startRotationOfMilliampermeter = milliampermeterArrow.transform.eulerAngles;
                    break;
                case TargetObject.VOLTMETERVN:
                    endRotationOfVoltmeterVn = newEndRotation;
                    startRotationOfVoltmeterVn = voltmeterVn.transform.eulerAngles;
                    break;
                case TargetObject.AMPERMETER:
                    endRotationOfAmpermeter = newEndRotation;
                    startRotationOfAmpermeter = ampermeterArrow.transform.eulerAngles;
                    break;
                case TargetObject.VOLTMETERVA:
                    endRotationOfVoltmeterVa = newEndRotation;
                    startRotationOfVoltmeterVa = voltmeterVa.transform.eulerAngles;
                    break;
            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
}

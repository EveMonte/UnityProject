using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class S_Practice : MonoBehaviour
{
    public Button thisButton;
    public GameObject baseModel;

    private S_GameObjectModel gameObjectModel;
    private S_ArrowRotation arrowRotator;
    private List<S_ObjectModel> listOfSteps = new List<S_ObjectModel>();
    private bool move = false;
    private float speed = 0.015f;
    private int currentIndex = 0;
    private float offset = 0;
    private GameObject tempAnchor;
    private Vector3 cameraPosition;
    private Quaternion cameraRotation;
    private S_ObjectModel objectModel;
    private S_ObjectMover objectMover;
    private Text tipText;
    private GameObject tableCamera;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            tipText = baseModel.GetComponent<S_Model>().tableModel.GetComponent<S_TableModel>().tip;
            tableCamera = baseModel.GetComponent<S_Model>().camerasModel.GetComponent<S_CamerasModel>().tableCamera;
            gameObjectModel = baseModel.GetComponent<S_Model>().gameObjectModel.GetComponent<S_GameObjectModel>();
            arrowRotator = baseModel.GetComponent<S_Model>().rotationManager.GetComponent<S_ArrowRotation>();
            thisButton.onClick.AddListener(ExecuteOneStepOfPreparation);
            FillPracticeDataList();
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    public void FillPracticeDataList()
    {
        objectModel = new S_ObjectModel(gameObjectModel.source, S_ObjectModel.TypeOfObject.SOURCE_SWITCH, "Переведите переключатель на источнике напряжения в положение On");

        objectMover = baseModel.GetComponent<S_Model>().practiceManager.GetComponent<S_ObjectMover>();
        objectMover.ObjectModel = objectModel;
        listOfSteps.Add(objectModel);

        objectModel = new S_ObjectModel(gameObjectModel.ampermeter, S_ObjectModel.TypeOfObject.ROTATABLE_PART, "Установите предел измерения амперметра Iн равным 1А");
        listOfSteps.Add(objectModel);

        objectModel = new S_ObjectModel(gameObjectModel.voltmeterVn, S_ObjectModel.TypeOfObject.ROTATABLE_PART, 240, "Проверьте, установлен ли предел измерения вольтметра Vн в значение 7,5В");
        listOfSteps.Add(objectModel);

        objectModel = new S_ObjectModel(gameObjectModel.milliampermeter, S_ObjectModel.TypeOfObject.ROTATABLE_PART, 330, "Установите предел измерения миллиамперметра Ia равным 15mA");
        listOfSteps.Add(objectModel);

        objectModel = new S_ObjectModel(gameObjectModel.voltmeterVa, S_ObjectModel.TypeOfObject.ROTATABLE_PART, "Установите предел измерения вольтметра Vа равным 300В");
        listOfSteps.Add(objectModel);

        objectModel = new S_ObjectModel(gameObjectModel.reostat1, S_ObjectModel.TypeOfObject.MOVABLE_PART, "Сопротивление реостата Rнак сделайте максимальным");
        listOfSteps.Add(objectModel);

        objectModel = new S_ObjectModel(gameObjectModel.reostat2, S_ObjectModel.TypeOfObject.MOVABLE_PART, -0.2222f, "Сопротивление реостата Rа должно быть минимальным");
        listOfSteps.Add(objectModel);

        objectModel = objectMover.ObjectModel;

        tipText.text = objectModel.TipText;
    }


    public void EnablePractice()
    {
        try
        {
            tipText.text = objectModel.TipText;
            move = true;
            tempAnchor = objectModel.CameraTargetObject.GetComponent<MeshRenderer>().probeAnchor.gameObject;
            cameraPosition = tableCamera.transform.position;
            cameraRotation = tableCamera.transform.rotation;
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
            if (currentIndex >= 5)
            {
                if(objectMover.isMoving)
                ComputeArrowRotation();
            }

            if (move)
            {
                offset += speed;
                tableCamera.transform.position = Vector3.Lerp(cameraPosition, tempAnchor.transform.position, offset);
                tableCamera.transform.rotation = Quaternion.Slerp(cameraRotation, tempAnchor.transform.rotation, offset);
                if (offset >= 1)
                {
                    move = false;
                    offset = 0;
                }
            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    private void ComputeArrowRotation()
    {
        try
        {
            if (objectModel.ActivePart.name.Contains('2') && objectModel.ActivePart.transform.localPosition.x <= objectModel.MaxLimit)
            {
                if (objectModel.ActivePart.transform.localPosition.x < 0.0435f)
                {
                    float coeff = Math.Abs((objectModel.MaxLimit - 0.01849826f - Mathf.Clamp(objectModel.ActivePart.transform.localPosition.x, objectModel.MinLimit, objectModel.MaxLimit)) / (objectModel.MaxLimit - objectModel.MinLimit - 0.01849826f));
                    Vector3 newRotation = new Vector3(270f, Mathf.Clamp(90 + coeff * 180 * (2 - coeff) / 1.5f, 90, 270), 0);
                    baseModel.GetComponent<S_Model>().gameObjectModel.GetComponent<S_GameObjectModel>().lamp.GetComponent<Light>().intensity = coeff;

                    arrowRotator.SetNewRotation(newRotation, S_ArrowRotation.TargetObject.MILLIAMPERMETER);
                    arrowRotator.SetNewRotation(new Vector3(270f, 90f + (objectModel.MaxLimit - 0.01849826f - objectModel.ActivePart.transform.localPosition.x) / (objectModel.MaxLimit - 0.01849826f - objectModel.MinLimit) * 144f, 0f), S_ArrowRotation.TargetObject.VOLTMETERVA);

                }

            }
            else if (objectModel.ActivePart.name.Contains('1'))
            {
                arrowRotator.SetNewRotation(new Vector3(270f, 90f + Math.Abs(objectModel.ActivePart.transform.localPosition.x - objectModel.MaxLimit) / (objectModel.MaxLimit - objectModel.MinLimit) * 174, 0f), S_ArrowRotation.TargetObject.AMPERMETER);
                float resistance;
                resistance = 7.5f * (objectModel.MaxLimit - objectModel.ActivePart.transform.localPosition.x) / (objectModel.MaxLimit - objectModel.MinLimit);
                float current = Math.Abs(objectModel.ActivePart.transform.localPosition.x - objectModel.MaxLimit) / (objectModel.MaxLimit - objectModel.MinLimit);
                float voltage = current * resistance;
                arrowRotator.SetNewRotation(new Vector3(270f, 90f + voltage * 177 / 7.5f, 0f), S_ArrowRotation.TargetObject.VOLTMETERVN);

            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
    public void ExecuteOneStepOfPreparation()
    {
        try
        {
            if (currentIndex == 6)
            {
                objectModel = null;
                thisButton.onClick.RemoveAllListeners();
                gameObject.GetComponent<S_ChangeResistance>().enabled = true;
                gameObject.GetComponent<S_Practice>().enabled = false;
            }

            else if (objectModel.DefineCanWeMoveNext())
            {
                currentIndex++;
                move = true;
                objectModel = listOfSteps.ElementAt(currentIndex);
                cameraPosition = tableCamera.transform.position;
                cameraRotation = tableCamera.transform.rotation;
                tipText.text = objectModel.TipText;
                tempAnchor = objectModel.CameraTargetObject.GetComponent<MeshRenderer>().probeAnchor.gameObject;
                objectMover.ObjectModel = objectModel;
            }
        }

        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
}

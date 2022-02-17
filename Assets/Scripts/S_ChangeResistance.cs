using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class S_ChangeResistance : MonoBehaviour
{
    public GameObject tableCameraPosition;
    public GameObject baseModel;

    private S_ArrowRotation arrowRotator;
    private S_Model baseModelScript;
    private S_UIModel uiModel;
    private GameObject movablePart;
    private GameObject firstReostatMovablePart;
    private S_ObjectModel objectModelFirstReostat;
    private S_ObjectModel objectModel;
    private S_ObjectModel objectModelSecondReostat;
    private S_ObjectMover objectMover;
    private float experiment;
    private float divider;
    private float? dividerVoltage;
    private bool isOnRestart = false;
    private S_TableModel tableModel;
    private S_TableModel.Experiment experimentEnum = 0;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            baseModelScript = baseModel.GetComponent<S_Model>();
            uiModel = baseModelScript.uiModel.GetComponent<S_UIModel>();
            tableModel = baseModelScript.tableModel.GetComponent<S_TableModel>();
            movablePart = GameObject.Find(baseModelScript.gameObjectModel.GetComponent<S_GameObjectModel>().reostat2.gameObject.tag);
            firstReostatMovablePart = GameObject.Find(baseModelScript.gameObjectModel.GetComponent<S_GameObjectModel>().reostat1.gameObject.tag);
            arrowRotator = baseModelScript.rotationManager.GetComponent<S_ArrowRotation>();
            uiModel.GetComponent<S_UIModel>().nextStepButton.GetComponent<Button>().onClick.AddListener(Experiment);
            Restart();
            isOnRestart = true;
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    public void Restart()
    {
        try
        {
            experiment = 0.6f;
            divider = 3.3f;
            if (isOnRestart)
            {
                new WaitForSeconds(2f);
            }

            objectModelFirstReostat = new S_ObjectModel(firstReostatMovablePart, S_ObjectModel.TypeOfObject.MOVABLE_PART, 0.07414327f, $"Установите сопротивление реостата Rн таким, чтобы Амперметр показывал значение {experiment}A");

            objectModelSecondReostat = new S_ObjectModel(movablePart, S_ObjectModel.TypeOfObject.MOVABLE_PART, -0.2222f, "Изменяйте сопротивление реостата Ra таким образом, чтобы напряжение по вольтметру Va изменялось с шагом 20В. Не забудьте записать данные каждого шага в таблицу");

            objectMover = baseModel.GetComponent<S_Model>().practiceManager.GetComponent<S_ObjectMover>();
            objectMover.ObjectModel = objectModelFirstReostat;

            uiModel.descriptionBackground.transform.GetChild(0).GetComponent<Text>().text = objectModelFirstReostat.TipText;
            objectModel = objectModelFirstReostat;
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    private void Experiment()
    {
        try
        {
            float tempValue = objectModelSecondReostat.MaxLimit - Math.Abs(objectModelSecondReostat.MaxLimit - objectModelSecondReostat.MinLimit) * experiment - 0.01f;

            if (tempValue - 0.005f < objectModel.ActivePart.transform.localPosition.x && tempValue + 0.02f > objectModel.ActivePart.transform.localPosition.x)
            {
                objectMover.ObjectModel = objectModelSecondReostat;

                uiModel.descriptionBackground.transform.GetChild(0).GetComponent<Text>().text = objectModelSecondReostat.TipText;
                experiment += 0.05f;

                uiModel.nextStepButton.GetComponent<Button>().onClick.RemoveAllListeners();
                objectModel = objectModelSecondReostat;
                if (experiment > 0.75f)
                {
                    uiModel.nextStepButton.GetComponent<Button>().onClick.AddListener(FinishPractice);
                }
                else
                    uiModel.nextStepButton.GetComponent<Button>().onClick.AddListener(NextExperiment);
            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    private void NextExperiment()
    {
        try
        {
            if (tableModel.IsExperimentDone(experimentEnum))
            {
                objectMover.ObjectModel = objectModelFirstReostat;

                uiModel.descriptionBackground.transform.GetChild(0).GetComponent<Text>().text = $"Установите сопротивление реостата Rн таким, чтобы Амперметр показывал значение {experiment}A";

                objectModel = objectModelFirstReostat;
                uiModel.nextStepButton.GetComponent<Button>().onClick.RemoveAllListeners();
                uiModel.nextStepButton.GetComponent<Button>().onClick.AddListener(Experiment);
                experimentEnum++;
                divider /= 1.75f;

            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    private void FinishPractice()
    {
        try
        {
            uiModel.descriptionBackground.transform.GetChild(0).GetComponent<Text>().text = "Лабораторная работа выполнена. Чтобы начать заново нажмите на кнопку перезапуска";
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }


    private void Update()
    {
        try
        {
            if (objectMover.isMoving)
            {
                ComputeNewRotations();

            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
    private void ComputeNewRotations()
    {

        if (objectModel.ActivePart.name.Contains('2'))
        {
            if (objectModel.ActivePart.transform.localPosition.x < 0.0435f)
            {
                float coeff = Math.Abs((objectModel.MaxLimit - Mathf.Clamp(objectModel.ActivePart.transform.localPosition.x, objectModel.MinLimit, objectModel.MaxLimit)) / (objectModel.MaxLimit - objectModel.MinLimit));
                baseModelScript.gameObjectModel.GetComponent<S_GameObjectModel>().lamp.GetComponent<Light>().intensity = coeff;
                Vector3 newRotation = new Vector3(270f, Mathf.Clamp(90 + coeff * 180 * (2 - coeff) / 1.5f / divider, 90, 270), 0);

                arrowRotator.SetNewRotation(newRotation, S_ArrowRotation.TargetObject.MILLIAMPERMETER);
                arrowRotator.SetNewRotation(new Vector3(270f, 90f + (objectModel.MaxLimit - objectModel.ActivePart.transform.localPosition.x) / (objectModel.MaxLimit - objectModel.MinLimit) * 144f, 0f), S_ArrowRotation.TargetObject.VOLTMETERVA);
            }
        }
        else
        {
            float resistance;
            resistance = 7.5f * (objectModel.MaxLimit - objectModel.ActivePart.transform.localPosition.x) / (objectModel.MaxLimit - objectModel.MinLimit);
            float current = Math.Abs(objectModel.ActivePart.transform.localPosition.x - objectModel.MaxLimit) / (objectModel.MaxLimit - objectModel.MinLimit);
            float voltage = current * resistance;
            dividerVoltage = dividerVoltage ?? current;
            arrowRotator.SetNewRotation(new Vector3(270f, 90f + current * 174, 0f), S_ArrowRotation.TargetObject.AMPERMETER);
            arrowRotator.SetNewRotation(new Vector3(270f, (float)(90f + voltage * 177 * Math.Pow((double)dividerVoltage, 0.25f) / 7.5f), 0f), S_ArrowRotation.TargetObject.VOLTMETERVN);
            if(experiment < 0.63f)
            {
                dividerVoltage = null;
            }
        }

    }
}

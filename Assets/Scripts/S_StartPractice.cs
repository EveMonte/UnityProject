using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_StartPractice : MonoBehaviour
{
    public GameObject controlsTipPanel;
    public Button startWorkButton;
    private GameObject trigger;
    public GameObject baseModel;

    private Button thisButton;
    private S_UIModel uiModel;
    private float deltaX;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            trigger = baseModel.GetComponent<S_Model>().gameObjectModel.GetComponent<S_GameObjectModel>().chairTrigger;
            uiModel = baseModel.GetComponent<S_Model>().uiModel.GetComponent<S_UIModel>();
            thisButton = gameObject.GetComponent<Button>();
            thisButton.onClick.AddListener(OpenControlsTip);
            startWorkButton.onClick.AddListener(StartWork);
            deltaX = uiModel.menuUI.transform.GetChild(0).GetComponent<RectTransform>().rect.width;
            deltaX = deltaX / 1920 * baseModel.GetComponent<S_Model>().camerasModel.GetComponent<S_CamerasModel>().tableCamera.GetComponent<Camera>().pixelWidth;
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    private void StartWork()
    {
        try
        {
            uiModel.descriptionBackground.SetActive(true);
            uiModel.practiceControls.SetActive(true);
            baseModel.GetComponent<S_Model>().practiceManager.GetComponent<S_Practice>().EnablePractice();
            uiModel.openTipButton.SetActive(true);
            uiModel.openMenuButton.SetActive(false);
            uiModel.moveBackToMenuButton.SetActive(false);
            uiModel.hintUI.gameObject.SetActive(false);
            startWorkButton.gameObject.SetActive(false);
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    private void OpenControlsTip()
    {
        try
        {
            if (!uiModel.hintUI.activeSelf && !baseModel.GetComponent<S_Model>().isItemChosen)
            {
                startWorkButton.gameObject.SetActive(true);
                uiModel.hintUI.gameObject.SetActive(true);
                uiModel.menuUI.SetActive(false);
                uiModel.tableUI.SetActive(false);
                uiModel.descriptionBackground.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                uiModel.openMenuButton.transform.GetChild(0).GetComponent<S_OpenMenu>().OpenMenu();
                uiModel.moveBackToMenuButton.SetActive(true);
                trigger.GetComponent<S_SitDown>().enabled = false;
                uiModel.openMenuButton.SetActive(false);
            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
}

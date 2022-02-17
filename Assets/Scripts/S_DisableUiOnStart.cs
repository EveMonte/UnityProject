using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_DisableUiOnStart : MonoBehaviour
{
    public GameObject basemodel;

    private S_UIModel uiModel;
    private S_CamerasModel camerasModel;
    private Button restartButton;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            uiModel = basemodel.GetComponent<S_Model>().uiModel.GetComponent<S_UIModel>();
            camerasModel = basemodel.GetComponent<S_Model>().camerasModel.GetComponent<S_CamerasModel>();
            Restart();
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    public void Restart()
    {
        uiModel.menuUI.SetActive(false);
        uiModel.tableUI.SetActive(false);
        uiModel.hintUI.SetActive(false);
        uiModel.practiceControls.SetActive(false);
        uiModel.openTipButton.gameObject.SetActive(false);
        uiModel.moveBackToMenuButton.gameObject.SetActive(false);
        uiModel.openMenuButton.gameObject.SetActive(false);
        camerasModel.tableCamera.SetActive(false);
        camerasModel.scaleCamera.SetActive(false);
        uiModel.descriptionBackground.SetActive(false);
        uiModel.scheduleImage.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_MoveBackToMenu : MonoBehaviour
{
    private Button thisButton;
    public GameObject baseModel;
    private S_UIModel uiModel;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            uiModel = baseModel.GetComponent<S_Model>().uiModel.GetComponent<S_UIModel>();
            thisButton = gameObject.GetComponent<Button>();
            thisButton.onClick.AddListener(closeActiveTabAndMoveBackToMenu);
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    private void closeActiveTabAndMoveBackToMenu()
    {
        try
        {
            uiModel.tableUI.SetActive(false);
            uiModel.menuUI.SetActive(true);
            uiModel.hintUI.SetActive(false);
            uiModel.openMenuButton.SetActive(true);
            uiModel.openMenuButton.transform.GetChild(0).GetComponent<S_OpenMenu>().OpenMenu();
            gameObject.SetActive(false);
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
}

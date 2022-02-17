using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_HideTable : MonoBehaviour
{
    public GameObject baseModel;

    private Button thisButton;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            thisButton = gameObject.GetComponent<Button>();
            thisButton.onClick.AddListener(HideTable);
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    public void HideTable()
    {
        try
        {
            baseModel.GetComponent<S_Model>().uiModel.GetComponent<S_UIModel>().tableUI.gameObject.SetActive(false);
            baseModel.GetComponent<S_Model>().uiModel.GetComponent<S_UIModel>().menuUI.gameObject.SetActive(true);
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

}

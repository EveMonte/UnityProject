using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_OpenTable : MonoBehaviour
{
    public GameObject baseModel;
    [HideInInspector]

    private Button thisButton;
    private S_Model baseModelScript;
    private S_UIModel uiModel;
    public Sprite returnButoonSprite;
    public Sprite openMenuButtonSprite;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            baseModelScript = baseModel.GetComponent<S_Model>();
            uiModel = baseModelScript.uiModel.GetComponent<S_UIModel>();
            thisButton = gameObject.GetComponent<Button>();
            thisButton.onClick.AddListener(OpenTable);
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    public void OpenTable()
    {
        try 
        {
            if (gameObject.name == "OpenTable")
                uiModel.openMenuButton.transform.GetChild(0).GetComponent<S_OpenMenu>().OpenMenu();

            uiModel.openMenuButton.transform.GetChild(0).GetComponent<Image>().sprite = !uiModel.tableUI.activeSelf ? returnButoonSprite : openMenuButtonSprite;
            if (baseModelScript.gameObjectModel.GetComponent<S_GameObjectModel>().chairTrigger.GetComponent<S_SitDown>().enabled)
            {
                uiModel.descriptionBackground.SetActive(!uiModel.tableUI.activeSelf);
                uiModel.descriptionBackground.transform.GetChild(0).GetComponent<Text>().text = "";

            }
            uiModel.tableUI.SetActive(!uiModel.tableUI.activeSelf);

        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
}

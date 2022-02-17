using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_OpenSchedule : MonoBehaviour
{
    private Button thisButton;
    public GameObject baseModel;
    public Sprite returnButtonSprite;
    private S_UIModel uiModel;
    private Sprite baseSprite;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            uiModel = baseModel.GetComponent<S_Model>().uiModel.GetComponent<S_UIModel>();
            thisButton = GetComponent<Button>();
            thisButton.onClick.AddListener(OpenSchedule);
            baseSprite = gameObject.GetComponent<Image>().sprite;
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    private void OpenSchedule()
    {
        try
        {
            uiModel.scheduleImage.SetActive(!uiModel.scheduleImage.activeSelf);
            uiModel.tableUI.SetActive(!uiModel.tableUI.activeSelf);
            if (uiModel.tableUI.activeSelf)
            {
                gameObject.GetComponent<Image>().sprite = returnButtonSprite;
            }
            else
            {
                gameObject.GetComponent<Image>().sprite = baseSprite;

            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

}

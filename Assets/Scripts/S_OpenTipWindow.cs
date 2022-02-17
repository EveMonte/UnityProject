using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_OpenTipWindow : MonoBehaviour
{
    public GameObject baseModel;
    public Sprite returnButtonSprite;

    private Sprite hintSprite;
    private Button thisButton;
    private S_UIModel uiModel;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            hintSprite = gameObject.GetComponent<Image>().sprite;
            uiModel = baseModel.GetComponent<S_Model>().uiModel.GetComponent<S_UIModel>();
            thisButton = gameObject.GetComponent<Button>();
            thisButton.onClick.AddListener(OpenTipWindow);
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    private void OpenTipWindow()
    {
        try
        {
            gameObject.GetComponent<Image>().sprite = uiModel.hintUI.activeSelf ? hintSprite : returnButtonSprite;
            uiModel.hintUI.SetActive(!uiModel.hintUI.activeSelf);
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
}

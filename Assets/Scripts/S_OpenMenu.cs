using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_OpenMenu : MonoBehaviour
{
    public Sprite menuIcon;
    public GameObject baseModel;
    private Button thisButton;
    private bool isMenuOpen = false;
    private float deltaX;
    private S_UIModel uiModel;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            uiModel = baseModel.GetComponent<S_Model>().uiModel.GetComponent<S_UIModel>();
            uiModel.menuUI.SetActive(true);
            deltaX = uiModel.menuUI.GetComponent<RectTransform>().rect.width;
            deltaX = deltaX / 1920 * baseModel.GetComponent<S_Model>().camerasModel.GetComponent<S_CamerasModel>().tableCamera.GetComponent<Camera>().pixelWidth;
            thisButton = gameObject.GetComponent<Button>();
            thisButton.onClick.AddListener(OpenMenu);
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    public void OpenMenu()
    {
        try
        {
            uiModel.tableUI.SetActive(false);
            uiModel.scheduleImage.SetActive(false);
            thisButton.GetComponent<Image>().sprite = menuIcon;
            deltaX *= isMenuOpen ? -1 : 1;
            uiModel.menuUI.transform.parent.Translate(deltaX, 0, 0, Space.Self);
            deltaX = Mathf.Abs(deltaX);
            isMenuOpen = !isMenuOpen;
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_CameraManipulation : MonoBehaviour
{
    public GameObject baseModel;

    private S_UIModel uiModel;
    private Button openScaleCameraButton;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            uiModel = baseModel.GetComponent<S_Model>().uiModel.GetComponent<S_UIModel>();
            openScaleCameraButton = uiModel.openScaleCameraButton.GetComponent<Button>();
            openScaleCameraButton.onClick.AddListener(OpenCamera);
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    private void OpenCamera()
    {
        baseModel.GetComponent<S_Model>().camerasModel.GetComponent<S_CamerasModel>().tableCamera.SetActive(gameObject.activeSelf);
        baseModel.GetComponent<S_Model>().camerasModel.GetComponent<S_CamerasModel>().tableCamera.GetComponent<Camera>().enabled = gameObject.activeSelf;
        gameObject.SetActive(!gameObject.activeSelf);
        gameObject.GetComponent<Camera>().enabled = gameObject.activeSelf;

    }
}

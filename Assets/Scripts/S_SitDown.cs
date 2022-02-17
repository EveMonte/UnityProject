using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_SitDown : MonoBehaviour
{
    // Start is called before the first frame update

    public Image cursor;
    public GameObject baseModel;

    [HideInInspector]

    private string textForTip;

    [HideInInspector]
    private bool flag = false;
    private Vector3 capsuleCoords = new Vector3(0, 0, 0);
    private bool isControllerStayingOnTrigger = false;
    private Button restartButton;
    private S_UIModel uiModel;
    private S_CamerasModel camerasModel;
    Collider other;
    private void Start()
    {
        try
        {
            uiModel = baseModel.GetComponent<S_Model>().uiModel.GetComponent<S_UIModel>();
            camerasModel = baseModel.GetComponent<S_Model>().camerasModel.GetComponent<S_CamerasModel>();
            restartButton = baseModel.GetComponent<S_Model>().uiModel.GetComponent<S_UIModel>().restartPracticeButton.GetComponent<Button>();
            restartButton.onClick.AddListener(Restart);
            uiModel.menuUI.gameObject.SetActive(false);
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (isControllerStayingOnTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                flag = true;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.gameObject.tag == "ChairTrigger")
            {
                textForTip = "Нажмите клавишу E на клавиатуре, чтобы сесть на стул";
                uiModel.descriptionBackground.transform.GetChild(0).GetComponent<Text>().text = textForTip;
                uiModel.descriptionBackground.gameObject.SetActive(true);
            }
    }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        try
        {
            isControllerStayingOnTrigger = false;
            if (other.gameObject.tag == "ChairTrigger")
            {
                uiModel.descriptionBackground.gameObject.SetActive(false);
            }
    }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        try
        {
            this.other = other;
            isControllerStayingOnTrigger = true;
            if (other.gameObject.tag == "ChairTrigger" && flag)
            {
                if (camerasModel.tableCamera.activeSelf)
                {

                    SwitchBooleanVariables(other);

                    textForTip = "Нажмите клавишу E на клавиатуре, чтобы сесть на стул";
                }

                else
                {
                    Restart();
                }
                uiModel.descriptionBackground.transform.GetChild(0).GetComponent<Text>().text = textForTip;

                flag = false;
            }
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
            SwitchBooleanVariables(other);
            textForTip = "Нажмите клавишу E на клавиатуре, чтобы встать со стула";
            StartCoroutine("CanvasOff");
            capsuleCoords = other.gameObject.transform.position;
    }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    IEnumerator CanvasOff()
    {
        yield return new WaitForSeconds(2f);
        uiModel.descriptionBackground.gameObject.SetActive(false);

    }
    public void SwitchBooleanVariables(Collider other)
    {
        if (uiModel.tableUI.activeSelf)
        {
            uiModel.openTableButton.transform.GetComponent<S_OpenTable>().OpenTable();

        }
        uiModel.hintUI.SetActive(false);
        uiModel.practiceControls.SetActive(false);
        uiModel.openTipButton.gameObject.SetActive(false);
        uiModel.moveBackToMenuButton.gameObject.SetActive(false);
        uiModel.scheduleImage.SetActive(false);

        uiModel.descriptionBackground.gameObject.SetActive(true);
        uiModel.tableUI.gameObject.SetActive(false);
        other.gameObject.GetComponent<S_Movement>().enabled = camerasModel.tableCamera.activeSelf;
        other.gameObject.GetComponent<S_Rotation>().enabled = camerasModel.tableCamera.activeSelf;
        camerasModel.mainCamera.gameObject.SetActive(camerasModel.tableCamera.activeSelf);
        camerasModel.mainCamera.GetComponent<Camera>().enabled = camerasModel.tableCamera.activeSelf;
        other.gameObject.GetComponent<MeshCollider>().enabled = camerasModel.tableCamera.activeSelf;
        cursor.gameObject.SetActive(camerasModel.tableCamera.activeSelf);

        camerasModel.tableCamera.gameObject.SetActive(!camerasModel.tableCamera.activeSelf);
        camerasModel.tableCamera.GetComponent<Camera>().enabled = camerasModel.tableCamera.activeSelf;

        Cursor.visible = camerasModel.tableCamera.gameObject.activeSelf;
        uiModel.menuUI.gameObject.SetActive(camerasModel.tableCamera.activeSelf);
        uiModel.openMenuButton.gameObject.SetActive(camerasModel.tableCamera.activeSelf);

    }
}

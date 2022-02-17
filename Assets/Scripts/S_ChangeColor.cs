using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_ChangeColor : MonoBehaviour
{
    public GameObject goal;
    public int index;
    public GameObject descriptionPanel;
    public GameObject trigger;
    public GameObject startPosition;
    public GameObject basemodel;

    private string[] descriptionArray = new string[] 
    { "�����������. ������ ���������� ����������� ���������� ��� ������� ���� � ���� ������ ������.",
        "��������� A. �������� �������� ���� ���� ������.", "�������������� mA. �������� ���� �������� ���� Ia.",
        "��������� Va. ��������� ��� ��������� �������� �������� ���������� Ua.",
        "��������� Vn. ������������ ��� ��������� ���������� ������ U���.",
        "���������� �������� ��� ������� R���. ���������� ���� ���� ������.",
        "�������� Ra ��� ������� Ra. ���������� �������� �������� ����������.",
        "��������������� ����� � ����. ���� ������������ ����� ���������� ��� ������������� ������," +
        " ���������� �� ��������� �������, � ����� ����������� � ������ � � ������� �." 
    };
    private Button thisButton;
    private Camera tableCamera;
    private S_Replacer script;
    private Color oldColor;
    private Texture mainTexture;
    private Text descriptionText;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            tableCamera = basemodel.GetComponent<S_Model>().camerasModel.GetComponent<S_CamerasModel>().tableCamera.GetComponent<Camera>();
            thisButton = gameObject.GetComponent<Button>();
            thisButton.onClick.AddListener(ChangeColor);
            descriptionText = descriptionPanel.transform.GetChild(0).GetComponent<Text>();
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }


    public void ChangeColor()
    {
        try
        {
            if (!basemodel.GetComponent<S_Model>().isItemChosen)
            {
                descriptionText.text = descriptionArray[index];
                descriptionPanel.SetActive(true);
                oldColor = goal.GetComponent<Renderer>().material.color;
                goal.GetComponent<Renderer>().material.color = new Color(0, 0, 1, 0.95f);
                mainTexture = goal.GetComponent<Renderer>().material.mainTexture;
                goal.GetComponent<Renderer>().material.mainTexture = null;
                basemodel.GetComponent<S_Model>().isItemChosen = true;
                Destroy(script);
                Destroy(tableCamera.gameObject.GetComponent<S_Replacer>());
                if (script == null)
                {
                    script = tableCamera.gameObject.AddComponent<S_Replacer>();
                    script.focusObject = goal;
                    script.startPosition = startPosition;
                    script.enabled = true;
                }
                StartCoroutine("DefaultColor");
            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    IEnumerator DefaultColor()
    {
        yield return new WaitForSeconds(3f);
        try 
        { 
            goal.GetComponent<Renderer>().material.color = oldColor;
            goal.GetComponent<Renderer>().material.mainTexture = mainTexture;
            descriptionPanel.SetActive(false);
            basemodel.GetComponent<S_Model>().isItemChosen = false;
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }
}

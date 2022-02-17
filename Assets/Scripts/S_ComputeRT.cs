using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class S_ComputeRT : MonoBehaviour
{
    public GameObject baseModel;
    private Button thisButton;
    private S_TableModel tableModel;
    private float[] currents;
    private float[] voltages;
    // Start is called before the first frame update
    void Start()
    {
        tableModel = baseModel.GetComponent<S_Model>().tableModel.GetComponent<S_TableModel>();
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(ComputeRT);
    }

    private void ComputeRT()
    {
        try
        {
            if (TryValidate())
            {
                tableModel.valueOfR1.text = Math.Round(voltages[0] / currents[0], 2).ToString() + " Ом";
                tableModel.valueOfR2.text = Math.Round(voltages[1] / currents[1], 2).ToString() + " Ом";
                tableModel.valueOfR3.text = Math.Round(voltages[2] / currents[2], 2).ToString() + " Ом";
            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }

    }

    private bool TryValidate()
    {
        tableModel.tip.text = "";

        voltages = new float[3];
        currents = new float[3];
        currents[0] = 0.6f;
        currents[1] = 0.65f;
        currents[2] = 0.7f;

        if (!float.TryParse(tableModel.valueOfU1.text.Replace('.', ','), out voltages[0]) &&
            !float.TryParse(tableModel.valueOfU2.text.Replace('.', ','), out voltages[1]) &&
            !float.TryParse(tableModel.valueOfU3.text.Replace('.', ','), out voltages[2]))
        {
            tableModel.tip.text = "Одно или несколько значений напряжений написаны некорректно. Проверьте написание и повторите попытку.";
            return false;
        }
        float.TryParse(tableModel.valueOfU2.text.Replace('.', ','), out voltages[1]);
        float.TryParse(tableModel.valueOfU3.text.Replace('.', ','), out voltages[2]);

        return true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class S_ComputeWork : MonoBehaviour
{
    public Text valueOfI1;
    public Text valueOfI2;
    public Text valueOfI3;
    public GameObject baseModel;
    private S_TableModel tableModel;
    private float[] temperatures;
    private Button thisButton;
    private float ComputeFormulae(float T1, float T2, float In1, float In2)
    {
        return (float)(8.93 * Mathf.Pow(10, -5) * T1 * T2 / (T2 - T1) * (Math.Log(In2 / In1) - 2 * Math.Log(T2 / T1)));
    }

    void Start()
    {
        tableModel = baseModel.GetComponent<S_Model>().tableModel.GetComponent<S_TableModel>();
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(WriteData);
    }


    public void WriteData()
    {
        try
        {
            if (TryValidate())
            {
                tableModel.valueOfA12.GetComponent<TMP_Text>().text += Math.Round(ComputeFormulae(temperatures[0], temperatures[1], float.Parse(valueOfI1.text), float.Parse(valueOfI2.text)), 2) + "эВ";
                tableModel.valueOfA23.GetComponent<TMP_Text>().text += Math.Round(ComputeFormulae(temperatures[1], temperatures[2], float.Parse(valueOfI2.text), float.Parse(valueOfI3.text)), 2) + "эВ";
                tableModel.valueOfA13.GetComponent<TMP_Text>().text += Math.Round(ComputeFormulae(temperatures[0], temperatures[2], float.Parse(valueOfI1.text), float.Parse(valueOfI3.text)), 2) + "эВ";
            }
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }
    }

    public bool TryValidate()
    {
        tableModel.tip.text = "";
        temperatures = new float[3];

        if (!float.TryParse(tableModel.valueOfT1.text.Replace('.', ','), out temperatures[0]) &&
            !float.TryParse(tableModel.valueOfT2.text.Replace('.', ','), out temperatures[1]) &&
            !float.TryParse(tableModel.valueOfT3.text.Replace('.', ','), out temperatures[2]))
        {
            tableModel.tip.text = "Одно или несколько значений температуры написаны некорректно. Проверьте написание и повторите попытку.";
            return false;
        }
        float.TryParse(tableModel.valueOfT2.text.Replace('.', ','), out temperatures[1]);
        float.TryParse(tableModel.valueOfT3.text.Replace('.', ','), out temperatures[2]);

        foreach (float value in temperatures)
        {
            if (value <= 0)
            {
                tableModel.tip.text = "Значение температуры не может быть меньше либо равным 0. Проверьте введенные значения.";
                return false;
            }
        }

        return true;
    }
}

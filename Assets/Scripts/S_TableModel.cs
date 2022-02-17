using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_TableModel : MonoBehaviour
{
    [HideInInspector]
    public enum Experiment
    {
        FIRST, SECOND, THIRD
    };

    public Text tip;

    [HideInInspector]
    public List<Text> listOfEmptyCells;

    public Text firstExperimentFirstRow;
    public Text secondExperimentFirstRow;    
    public Text thirdExperimentFirstRow;

    public Text firstExperimentSecondRow;
    public Text secondExperimentSecondRow;
    public Text thirdExperimentSecondRow;

    public Text firstExperimentThirdRow;
    public Text secondExperimentThirdRow;
    public Text thirdExperimentThirdRow;

    public Text firstExperimentFourthRow;
    public Text secondExperimentFourthRow;
    public Text thirdExperimentFourthRow;

    public Text firstExperimentFifthRow;
    public Text secondExperimentFifthRow;
    public Text thirdExperimentFifthRow;

    public Text firstExperimentSixthRow;
    public Text secondExperimentSixthRow;
    public Text thirdExperimentSixthRow;

    public Text firstExperimentSeventhRow;
    public Text secondExperimentSeventhRow;
    public Text thirdExperimentSeventhRow;

    public Text firstExperimentEighthRow;
    public Text secondExperimentEighthRow;
    public Text thirdExperimentEighthRow;

    public Text firstExperimentNinethRow;
    public Text secondExperimentNinethRow;
    public Text thirdExperimentNinethRow;

    public Text firstExperimentTenthRow;
    public Text secondExperimentTenthRow;
    public Text thirdExperimentTenthRow;

    public Text firstExperimentEleventhRow;
    public Text secondExperimentEleventhRow;
    public Text thirdExperimentEleventhRow;

    public Text firstExperimentTwelfthRow;
    public Text secondExperimentTwelfthRow;
    public Text thirdExperimentTwelfthRow;

    public Text firstExperimentZeroRow;
    public Text secondExperimentZeroRow;
    public Text thirdExperimentZeroRow;

    public GameObject valueOfA12;
    public GameObject valueOfA13;
    public GameObject valueOfA23;

    public Text valueOfT1;
    public Text valueOfT2;
    public Text valueOfT3;

    public Text valueOfR1;
    public Text valueOfR2;
    public Text valueOfR3;

    public Text valueOfU1;
    public Text valueOfU2;
    public Text valueOfU3;

    private void Start()
    {
        listOfEmptyCells = new List<Text>();

        listOfEmptyCells.Add(firstExperimentZeroRow);
        listOfEmptyCells.Add(firstExperimentFirstRow);
        listOfEmptyCells.Add(firstExperimentSecondRow);
        listOfEmptyCells.Add(firstExperimentThirdRow);
        listOfEmptyCells.Add(firstExperimentFourthRow);
        listOfEmptyCells.Add(firstExperimentFifthRow);
        listOfEmptyCells.Add(firstExperimentSixthRow);
        listOfEmptyCells.Add(firstExperimentSeventhRow);
        listOfEmptyCells.Add(firstExperimentEighthRow);
        listOfEmptyCells.Add(firstExperimentNinethRow);
        listOfEmptyCells.Add(firstExperimentTenthRow);
        listOfEmptyCells.Add(firstExperimentEleventhRow);
        listOfEmptyCells.Add(firstExperimentTwelfthRow);
        listOfEmptyCells.Add(valueOfU1);

        listOfEmptyCells.Add(secondExperimentZeroRow);
        listOfEmptyCells.Add(secondExperimentFirstRow);
        listOfEmptyCells.Add(secondExperimentSecondRow);
        listOfEmptyCells.Add(secondExperimentThirdRow);
        listOfEmptyCells.Add(secondExperimentFourthRow);
        listOfEmptyCells.Add(secondExperimentFifthRow);
        listOfEmptyCells.Add(secondExperimentSixthRow);
        listOfEmptyCells.Add(secondExperimentSeventhRow);
        listOfEmptyCells.Add(secondExperimentEighthRow);
        listOfEmptyCells.Add(secondExperimentNinethRow);
        listOfEmptyCells.Add(secondExperimentTenthRow);
        listOfEmptyCells.Add(secondExperimentEleventhRow);
        listOfEmptyCells.Add(secondExperimentTwelfthRow);
        listOfEmptyCells.Add(valueOfU2);

        listOfEmptyCells.Add(thirdExperimentZeroRow);
        listOfEmptyCells.Add(thirdExperimentFirstRow);
        listOfEmptyCells.Add(thirdExperimentSecondRow);
        listOfEmptyCells.Add(thirdExperimentThirdRow);
        listOfEmptyCells.Add(thirdExperimentFourthRow);
        listOfEmptyCells.Add(thirdExperimentFifthRow);
        listOfEmptyCells.Add(thirdExperimentSixthRow);
        listOfEmptyCells.Add(thirdExperimentSeventhRow);
        listOfEmptyCells.Add(thirdExperimentEighthRow);
        listOfEmptyCells.Add(thirdExperimentNinethRow);
        listOfEmptyCells.Add(thirdExperimentTenthRow);
        listOfEmptyCells.Add(thirdExperimentEleventhRow);
        listOfEmptyCells.Add(thirdExperimentTwelfthRow);
        listOfEmptyCells.Add(valueOfU3);
    }

    public bool IsExperimentDone(Experiment experiment)
    {
        tip.text = "";
        int minIndex = 0;
        int maxIndex = 0;
        float tempValue;
        switch (experiment)
        {
            case Experiment.FIRST: maxIndex = 13;
                break;
            case Experiment.SECOND: maxIndex = 27;
                break;
            case Experiment.THIRD: maxIndex = 41;
                break;
        }
        while(minIndex < maxIndex)
        {
            if (listOfEmptyCells[minIndex].text.Equals(string.Empty) || !float.TryParse(listOfEmptyCells[minIndex].text.Replace('.', ','), out tempValue))
            {
                tip.text = "Вы не можете перейти к следующему шагу эксперимента, так как вы заполнили не все данные по текущему шагу";
                return false;
            }
            minIndex++;
        }
        return true;
    }

    public void SetEmptyStrings()
    {
        foreach(Text value in listOfEmptyCells)
        {
            value.text = "";
        }


        valueOfA12.GetComponent<TMP_Text>().text = "";
        valueOfA13.GetComponent<TMP_Text>().text = "";
        valueOfA23.GetComponent<TMP_Text>().text = "";

        valueOfT1.text = "";
        valueOfT2.text = "";
        valueOfT3.text = "";

        valueOfR1.text = "";
        valueOfR2.text = "";
        valueOfR3.text = "";

        valueOfU1.text = "";
        valueOfU2.text = "";
        valueOfU3.text = "";
    }
}

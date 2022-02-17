using Aspose.Cells;
using Aspose.Cells.Charts;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class S_ExcelManager : MonoBehaviour
{
    public GameObject dataManager;

    private Workbook workbook;
    private Worksheet worksheet;
    [HideInInspector]
    public S_TableModel tableScript;
    private Button thisButton;

    public void Start()
    {
        workbook = new Workbook();
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(DrawGraphs);
        // Obtain the reference of the first worksheet
        worksheet = workbook.Worksheets[0];
        tableScript = dataManager.GetComponent<S_Model>().tableModel.GetComponent<S_TableModel>();

    }

    [Obsolete]
    public void DrawGraphs()
    {
        try
        {
            int currentRow = 0;
            int rowNumber = 14;
            char cellCoord = 'B';
            string coords;
            worksheet.Cells["A1"].PutValue("Напряжение Ua");

            for (int i = currentRow; i < rowNumber - 1; i++)
            {
                coords = cellCoord.ToString() + 1;
                worksheet.Cells[coords].PutValue(i*20);
                cellCoord = (char)(cellCoord + 1);
            }
            cellCoord = 'B';

            worksheet.Cells["A2"].PutValue("Сила тока Ia (при Iн = 0.6А)");
            worksheet.Cells["A3"].PutValue("Сила тока Ia (при Iн = 0.65A)");
            worksheet.Cells["A4"].PutValue("Сила тока Ia (при Iн = 0.7А)");
            for (int i = currentRow; i < rowNumber - 1; i++)
            {
                coords = cellCoord.ToString();

                worksheet.Cells[coords + 2].PutValue(double.Parse(tableScript.listOfEmptyCells[i].text.Replace('.', ',')));
                worksheet.Cells[coords + 3].PutValue(double.Parse(tableScript.listOfEmptyCells[i + rowNumber].text.Replace('.', ',')));
                worksheet.Cells[coords + 4].PutValue(double.Parse(tableScript.listOfEmptyCells[i + rowNumber*2].text.Replace('.', ',')));
                cellCoord = (char)(cellCoord + 1);
            }

            worksheet.AutoFitColumn(0, 0, 4);

            int chartIndex = worksheet.Charts.Add(ChartType.Line, 5, 1, 29, 15);
            int chartIndex2 = worksheet.Charts.Add(ChartType.Line, 5, 1, 29, 15);
            int chartIndex3 = worksheet.Charts.Add(ChartType.Line, 5, 1, 29, 15);
            int index = 0;

            Chart chart = worksheet.Charts[chartIndex];
            Chart chart2 = worksheet.Charts[chartIndex2];
            Chart chart3 = worksheet.Charts[chartIndex3];

            chart.NSeries.Add("A2:N2", false);
            chart.NSeries.CategoryData = "B1:N1";

            chart2.NSeries.Add("A3:N3", false);
            chart2.NSeries.CategoryData = "B1:N1";

            chart3.NSeries.Add("A4:N4", false);
            chart3.NSeries.CategoryData = "B1:N1";

            chart.ChartObject.X = 150;
            chart.ChartObject.Y = 150;

            chart2.ChartObject.Y = chart.ChartObject.Y + chart.ChartObject.Height;
            chart2.ChartObject.X = 150;

            chart3.ChartObject.Y = chart2.ChartObject.Y + chart2.ChartObject.Height;
            chart3.ChartObject.X = 150;

            index = chart.NSeries[0].TrendLines.Add(TrendlineType.Polynomial);
            Trendline trendline = chart.NSeries[0].TrendLines[index];
            trendline.Order = 3;

            index = chart2.NSeries[0].TrendLines.Add(TrendlineType.Polynomial);
            Trendline trendline2 = chart2.NSeries[0].TrendLines[index];
            trendline2.Order = 3;

            index = chart3.NSeries[0].TrendLines.Add(TrendlineType.Polynomial);
            Trendline trendline3 = chart3.NSeries[0].TrendLines[index];
            trendline3.Order = 3;

            chart.NSeries[0].LegendEntry.IsDeleted = true;
            chart2.NSeries[0].LegendEntry.IsDeleted = true;
            chart3.NSeries[0].LegendEntry.IsDeleted = true;

            trendline.Name = "Вольт-амперная характеристика";
            trendline3.Name = "Вольт-амперная характеристика";
            trendline2.Name = "Вольт-амперная характеристика";


            chart.Title.Text = "Вольт-амперная характеристика диода при Iн = 0.6A";
            chart.Title.Font.Size = 14;
            chart2.Title.Text = "Вольт-амперная характеристика диода при Iн = 0.65А";
            chart2.Title.Font.Size = 14;
            chart3.Title.Text = "Вольт-амперная характеристика диода при Iн = 0.7A";
            chart3.Title.Font.Size = 14;

            workbook.Save("Charts.xls");

            new Process
            {
                StartInfo = new ProcessStartInfo("Charts.xls")
                {
                    UseShellExecute = true
                }
            }.Start();
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }
    }
}

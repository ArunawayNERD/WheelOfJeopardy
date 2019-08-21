using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class QuestionDataWriter
{
    private string[] categories;
    private string[,] qAData1;
    private string[,] qAData2;
    private GameEngine gameEngine;

    public QuestionDataWriter(int numCats, int numQs, GameEngine gameEngine)
    {
        categories = new string[numCats];
        qAData1 = new string[numCats * numQs, 4];
        qAData2 = new string[numCats * numQs, 4];
        this.gameEngine = gameEngine;
    }

    public string[] Categories { get => categories; set => categories = value; }
    public string[,] QAData1 { get => qAData1; set => qAData1 = value; }
    public string[,] QAData2 { get => qAData2; set => qAData2 = value; }

    internal void WriteToCSV()
    {
        // Write first round data to a CSV
        string dir1 = Directory.GetCurrentDirectory() + "\\Assets\\Resources\\EnteredQuestionData1.csv";
        var csv1 = new StringBuilder();
        csv1.AppendLine("Each line represents a question in to format Category,Question,Answer,Score");
        for (int i = 0; i < qAData1.GetLength(0); i++)
        {
            var cat = qAData1[i,0];
            var q = qAData1[i,1];
            var a = qAData1[i,2];
            var pts = qAData1[i,3];
            var newLine = $"{cat},{q},{a},{pts}";
            csv1.AppendLine(newLine);
        }
        File.WriteAllText(dir1, csv1.ToString());

        // Write second round data to another CSV
        string dir2 = Directory.GetCurrentDirectory() + "\\Assets\\Resources\\EnteredQuestionData2.csv";
        var csv2 = new StringBuilder();
        csv2.AppendLine("Each line represents a question in to format Category,Question,Answer,Score");
        for (int i = 0; i < qAData2.GetLength(0); i++)
        {
            var cat = qAData2[i,0];
            var q = qAData2[i,1];
            var a = qAData2[i,2];
            var pts = qAData2[i,3];
            var newLine = $"{cat},{q},{a},{pts}";
            csv2.AppendLine(newLine);
        }
        File.WriteAllText(dir2, csv2.ToString());

        this.NotifyOfDataSrc();
    }

    private void NotifyOfDataSrc()
    {
        this.gameEngine.SetDataSrc(true);
    }
}

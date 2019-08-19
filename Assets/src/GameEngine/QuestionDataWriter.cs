using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuestionDataWriter
{
    private string[] categories;
    private string[,] qAData1;
    private string[,] qAData2;

    public QuestionDataWriter(int numCats, int numQs)
    {
        categories = new string[numCats];
        qAData1 = new string[numCats * numQs, 4];
        qAData2 = new string[numCats * numQs, 4];
    }

    public string[] Categories { get => categories; set => categories = value; }
    public string[,] QAData1 { get => qAData1; set => qAData1 = value; }
    public string[,] QAData2 { get => qAData2; set => qAData2 = value; }
}

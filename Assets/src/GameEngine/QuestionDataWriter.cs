using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuestionDataWriter
{
    private string[] categories;
    private string[,] qAData;

    public QuestionDataWriter(int numCats, int numQs)
    {
        categories = new string[numCats];
        qAData = new string[numCats * numQs, 4];
    }

    public string[] Categories { get => categories; set => categories = value; }
    public string[,] QAData { get => qAData; set => qAData = value; }
}

using UnityEngine;
using UnityEditor;
using System.Collections;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;
using LitJson;
using System.Text;

///
/// !!! Machine generated code !!!
///
//[CustomEditor(typeof(Chapter))]
public class ChapterEditor : Editor
{
    public static string chapterInput;

    void OnEnable()
    {

    }

    [MenuItem("Tools/Update Story Data Selection", false, 0)]
    public static void UpdateData()
    {
        Object[] chapters = Selection.objects;
        foreach (Object ob in chapters)
        {
            Chapter cha = ob as Chapter;
            Load(cha);
        }
    }

    public override void OnInspectorGUI()
    {
        DrawInspector();
    }

    public void DrawInspector()
    {
        Chapter targetChap = (Chapter)target;


        GUILayout.TextField(targetChap.sheetPath);
        if (GUILayout.Button("Update"))
        {
            Load();
        }
        GUILayout.Space(20);
        //GUIHelper.DrawSerializedProperty(dataArray);
        DrawDefaultInspector();
    }

    public void Load()
    {
        Chapter targetChap = (Chapter)target;
        targetChap.dataArray = new List<ChapterData>();
        Excel xls = ExcelHelper.LoadExcel(targetChap.sheetPath);
        for (int i = 0; i < xls.Tables.Count; i++)
        {
            if (xls.Tables[i].TableName == (targetChap.name))
            {
                ExcelTable excelTable = xls.Tables[i];
                for (int row = 2; row <= excelTable.NumberOfRows; row++)
                {
                    ChapterData temp = new ChapterData();
                    try
                    {
                        for (int column = 1; column <= excelTable.NumberOfColumns; column++)
                        {
                            if (column == 1)
                            {
                                temp.Scene = int.Parse(excelTable.GetValue(row, column).ToString());
                            }
                            else if (column == 2)
                            {
                                temp.Character1 = int.Parse(excelTable.GetValue(row, column).ToString());
                            }
                            else if (column == 3)
                            {
                                temp.Character2 = int.Parse(excelTable.GetValue(row, column).ToString());
                            }
                            else if (column == 4)
                            {
                                temp.Highlight = int.Parse(excelTable.GetValue(row, column).ToString());
                            }
                            else if (column == 5)
                            {
                                temp.Dialogue = excelTable.GetValue(row, column).ToString();
                            }
                        }
                        targetChap.dataArray.Add(temp);
                    }
                    catch
                    {
                        continue;
                    }
                    
                }
            }
        }
    }
    public static void Load(Chapter chapterTarget = null)
    {
        Chapter targetChap = chapterTarget;
        targetChap.dataArray = new List<ChapterData>();
        Excel xls = ExcelHelper.LoadExcel(targetChap.sheetPath);
        for (int i = 0; i < xls.Tables.Count; i++)
        {
            if (xls.Tables[i].TableName == (targetChap.name))
            {
                ExcelTable excelTable = xls.Tables[i];
                for (int row = 2; row <= excelTable.NumberOfRows; row++)
                {
                    ChapterData temp = new ChapterData();
                    try
                    {
                        for (int column = 1; column <= excelTable.NumberOfColumns; column++)
                        {
                            if (column == 1)
                            {
                                temp.Scene = int.Parse(excelTable.GetValue(row, column).ToString());
                            }
                            else if (column == 2)
                            {
                                temp.Character1 = int.Parse(excelTable.GetValue(row, column).ToString());
                            }
                            else if (column == 3)
                            {
                                temp.Character2 = int.Parse(excelTable.GetValue(row, column).ToString());
                            }
                            else if (column == 4)
                            {
                                temp.Highlight = int.Parse(excelTable.GetValue(row, column).ToString());
                            }
                            else if (column == 5)
                            {
                                temp.Dialogue = excelTable.GetValue(row, column).ToString();
                            }
                        }
                        targetChap.dataArray.Add(temp);
                    }
                    catch
                    {
                        continue;
                    }

                }
            }
        }
    }

}

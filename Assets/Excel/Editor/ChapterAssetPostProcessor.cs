//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System.IO;
//using UnityQuickSheet;

/////
///// !!! Machine generated code !!!
/////
//public class ChapterAssetPostprocessor : AssetPostprocessor
//{
//    public static string filePath = "Assets/Excel/Little Slippies Story.xlsx";
//    public static string assetFilePath = "Assets/Excel/Chapter.asset";
//    public static string sheetName = "Chapter2_2";

//    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
//    {
//        foreach (string asset in importedAssets)
//        {
//            if (!filePath.Equals(asset))
//                continue;

//            Chapter data = (Chapter)AssetDatabase.LoadAssetAtPath(assetFilePath, typeof(Chapter));
//            if (data == null)
//            {
//                data = ScriptableObject.CreateInstance<Chapter>();
//                data.SheetName = filePath;
//                data.WorksheetName = sheetName;
//                AssetDatabase.CreateAsset((ScriptableObject)data, assetFilePath);
//                //data.hideFlags = HideFlags.NotEditable;
//            }

//            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<Chapter2_2Data>().ToArray();		

//            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
//            //EditorUtility.SetDirty (obj);

//            ExcelQuery query = new ExcelQuery(filePath, sheetName);
//            if (query != null && query.IsValid())
//            {
//                data.dataArray = query.Deserialize<ChapterData>().ToArray();
//                ScriptableObject obj = AssetDatabase.LoadAssetAtPath(assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
//                EditorUtility.SetDirty(obj);
//            }
//        }
//    }
//}

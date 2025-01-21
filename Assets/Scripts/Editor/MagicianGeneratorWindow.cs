using System.IO;
using UnityEditor;
using UnityEngine;

public class MagicianGeneratorWindow : EditorWindow
{
    private enum ErrorType { None, DirectoryNotExist }

    private string _unitName;
    private CombatUnitData _combatUnitData;

    //private SerializedObject serializedCombatUnitData;
    
    [MenuItem("Utilities/Generate Player Unit")]
    public static void ShowWindow()
    {
        GetWindow<MagicianGeneratorWindow>("Generate Player Unit");
    }


    private void OnGUI()
    {
        GUILayout.Space(10);
        
        _unitName = EditorGUILayout.TextField("Unit Name", _unitName);
        //_combatUnitData = EditorGUILayout.ObjectField("Combat Unit Data", _combatUnitData, typeof(CombatUnitData), false) as CombatUnitData;

        GUILayout.Space(320);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            if (string.IsNullOrWhiteSpace(_unitName))
            {
                _DisplayErrorMessage("Unit name cannot be empty.");
            }
            else
            {
                bool isSuccess = _CreatePlayerUnitScript() == ErrorType.None &&
                                 _CreatePlayerUnitData() == ErrorType.None;
                if (isSuccess)
                {
                    Close();
                }
                else
                {
                    _DisplayErrorMessage("Directory doesn't exist.");
                }
            }
        }
        if (GUILayout.Button("Cancel"))
        {
            _unitName = "";
            //_combatUnitData = null;
            Close();
        }
        EditorGUILayout.EndHorizontal();
    }

    private ErrorType _CreatePlayerUnitScript()
    {
        string path = "Assets/Scripts/Characters/Magicians";
        string fileName = $"/{_unitName}.cs";
        
        if (!Directory.Exists(path))
        {
            EditorUtility.DisplayDialog("Error", "Directory doesn't exist.", "OK");
            return ErrorType.DirectoryNotExist;
        }
        
        string scriptTemplate = $"" +
                                "using UnityEngine;\n" +
                                "\n" +
                                "namespace Artheia.CombatUnit\n" +
                                "{\n" +
                                $"    public class {_unitName} : PlayerUnit\n" +
                                "    {\n" +
                                "        // Add your custom logic here\n" +
                                "    }\n" +
                                "}";
        
        File.WriteAllText(path + fileName, scriptTemplate);
        AssetDatabase.Refresh();
        return ErrorType.None;
    }

    private ErrorType _CreatePlayerUnitData()
    {
        string path = Artheia.Path.MagicianAssetDataPath;
        string fileName = $"/{_unitName}.asset";
        
        if (!Directory.Exists(path))
        {
            return ErrorType.DirectoryNotExist;
        }
        
        // Create ScriptableObject
        CombatUnitData newCombatUnitData = CreateInstance<CombatUnitData>();
        AssetDatabase.CreateAsset(newCombatUnitData, path + fileName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        return ErrorType.None;
    }

    private void _DisplayErrorMessage(string message)
    {
        EditorUtility.DisplayDialog("Error", message, "OK");
    }
}

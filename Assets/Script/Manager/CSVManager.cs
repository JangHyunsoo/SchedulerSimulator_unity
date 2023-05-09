using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;

public class CSVManager : MonoBehaviour
{
    private List<string[]> studentData = new List<string[]>();
    [SerializeField]
    private InputField path_input_;

    void writeResult(List<Process> process)
    {
        string[] tempStudentData = new string[3];
        tempStudentData[0] = "Name";
        tempStudentData[1] = "Age";
        tempStudentData[2] = "ID";
        studentData.Add(tempStudentData);
        for (int i = 0; i < 10; i++)
        {
            tempStudentData = new string[3];
            tempStudentData[0] = "Micheal"+i;
            tempStudentData[1] = (i + 20).ToString(); 
            tempStudentData[2] = i.ToString();
            studentData.Add(tempStudentData);
        }

        string[][] output = new string[studentData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = studentData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(delimiter, output[index]));
        }

        string filePath = getPath(path_input_.text);

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    private string getPath(string _file_name)
    {
        #if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + _file_name + ".csv";
        #elif UNITY_ANDROID
        return Application.persistentDataPath+"Student Data.csv";
        #elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Student Data.csv";
        #else
        return Application.dataPath +"/"+"Student Data.csv";
        #endif
    }
}

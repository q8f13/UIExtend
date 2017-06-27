using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataSetEditor : Editor {

	[MenuItem("Custom/CreateAsset")]
	public static void CreateAsset()
	{
		if (File.Exists(Application.dataPath + "/dataset.asset"))
		{
			Debug.LogError("dataset file already exists");
			return;
		}
		AssetDatabase.CreateAsset(CreateInstance<DataSet>(),  "Assets/dataset.asset");
		AssetDatabase.SaveAssets();
	}
}

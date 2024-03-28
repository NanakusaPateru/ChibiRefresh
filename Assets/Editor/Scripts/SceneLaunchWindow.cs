using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Content;
using Codice.Client.Commands;

namespace Editor
{

	// https://qiita.com/r-ngtm/items/a882d1b8778704b9178b
	// 上記参考

	public class SceneLaunchWindow : EditorWindow
	{
		private SceneAsset[] sceneArray;
		private string currentSceneName = "";
		private Vector2 scrollPos = Vector2.zero;

		[MenuItem("Tools/Scene Launcher")]
		static void OpenWindow()
		{
			GetWindow<SceneLaunchWindow>("SceneLauncher");
		}

		void OnGUI()
		{
			if (this.sceneArray == null) 
			{
				sceneArray = GetAllSceneAssets();
			}

			if (this.sceneArray.Length == 0)
			{
				EditorGUILayout.LabelField("シーンファイルが存在しません");
				return;
			}

			currentSceneName = EditorSceneManager.GetActiveScene().name;

			EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
			this.scrollPos = EditorGUILayout.BeginScrollView(this.scrollPos);
			foreach (var scene in this.sceneArray)
			{
				var path = AssetDatabase.GetAssetPath(scene);

				GUI.color = currentSceneName == scene.name ? Color.cyan : Color.grey;

				if (GUILayout.Button(scene.name))
				{
					EditorSceneManager.OpenScene(path);
				}
			}
			EditorGUILayout.EndScrollView();
			EditorGUI.EndDisabledGroup();
		}

		static SceneAsset[] GetAllSceneAssets()
		{
			return EditorBuildSettings.scenes
				.Select(x => (SceneAsset)AssetDatabase.LoadAssetAtPath(x.path, typeof(SceneAsset)))
				.ToArray();
		}
	}
}
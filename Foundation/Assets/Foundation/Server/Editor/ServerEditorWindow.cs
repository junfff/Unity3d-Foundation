// -------------------------------------
//  Domain		: Avariceonline.com
//  Author		: Nicholas Ventimiglia
//  Product		: Unity3d Foundation
//  Published		: 2015
//  -------------------------------------
using System.IO;
using Foundation.Server;
using UnityEditor;
using UnityEngine;

namespace Foundation.Editor
{
    public class ServerEditorWindow : EditorWindow
    {

        [MenuItem("Tools/Foundation/Server Settings")]
        public static void ShowWindow()
        {
            GetWindowWithRect<ServerEditorWindow>(new Rect(0, 0, 640, 200), false, "Server Settings");
        }

        static void CreateSettings()
        {
            var instance = Resources.Load<ServerConfig>("ServerConfig");
            if (instance == null)
            {
                Debug.Log("Cloud Config Created at Resources/ServerConfig.asset");

                var inst = CreateInstance<ServerConfig>();

                if (!Directory.Exists(Application.dataPath + "/Resources"))
                    AssetDatabase.CreateFolder("Assets", "Resources");

                AssetDatabase.CreateAsset(inst, "Assets/Resources/ServerConfig.asset");

                AssetDatabase.SaveAssets();
            }
        }

        static ServerConfig Target
        {
            get
            {
                if (ServerConfig.Instance == null)
                    CreateSettings();
                return ServerConfig.Instance;
            }
        }
        
        void Documentation()
        {
            Application.OpenURL("https://github.com/NVentimiglia/Unity3d-Foundation/wiki");
        }
        
        void OnGUI()
        {
            GUILayout.BeginHorizontal(GUILayout.MinHeight(64));

            GUILayout.Label("Unity3d Foundation", new GUIStyle
            {
                fontSize = 32,
                padding = new RectOffset(16, 0, 16, 0),
                fontStyle = FontStyle.Bold,
                normal = new GUIStyleState
                {
                    textColor = Color.white
                }


            });

            GUILayout.EndHorizontal();

            //
            GUILayout.Label("Application Key");
            Target.Key = EditorGUILayout.TextField(Target.Key);
            EditorStyles.label.wordWrap = true;
            GUILayout.Space(16);
            
            GUILayout.Label("Service URL");
            Target.Path = EditorGUILayout.TextField(Target.Path);
            GUILayout.Space(16);
            //
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset to Default"))
            {

                Target.Key = "";
                Target.Path = "https://github.com/NVentimiglia/Unity3d-Foundation";
            }
            if (GUILayout.Button("Documentation"))
            {
                Documentation();
            }
            EditorGUILayout.EndHorizontal();
            //
            EditorUtility.SetDirty(Target);
        }
    }
}
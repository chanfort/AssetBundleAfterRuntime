using System.IO;

namespace UnityEditor
{
    public class BuildBundleEditor
    {
        [MenuItem("Tests/Create/Create New")]
        public static void Create()
        {
            string bundlePath = "Assets/Snapshot/";
            
            if(!Directory.Exists("Assets/Snapshot"))
            {
                return;
            }

            AssetImporter aImporter = AssetImporter.GetAtPath("Assets/Snapshot/1.png");
            aImporter.assetBundleName = "1.unity3d";
            
			BuildAssetBundleOptions buildAssetOptions = BuildAssetBundleOptions.ChunkBasedCompression;
            BuildPipeline.BuildAssetBundles(bundlePath, buildAssetOptions, EditorUserBuildSettings.activeBuildTarget);

            foreach (string file in Directory.GetFiles("Assets/Snapshot", "*"))
            {
                if(file != "Assets/Snapshot/1.unity3d")
                {
                    File.Delete(file);
                }
            }

            AssetDatabase.Refresh();
        }
    }

    public class SimpleRecorder : EditorWindow
    {
        int count = 0;
        string countStr = "0";
        static bool isRunning = false;

        void Update()
        {
            count = count + 1;

            if ((EditorApplication.isPlaying == false) && (isRunning == true))
            {
                // play mode stopped
                isRunning = false;
                EditorApplication.ExecuteMenuItem("Tests/Create/Create New");
                EditorApplication.ExecuteMenuItem("Tests/Hide Window");
                AssetDatabase.Refresh();
            }
        }

        [MenuItem("Tests/Show Window")]
        static void ShowW()
        {
            SimpleRecorder window = (SimpleRecorder)EditorWindow.GetWindow(typeof(SimpleRecorder), true, "Asset Bundle Builder");
            window.Show();
            isRunning = true;
        }

        [MenuItem("Tests/Hide Window")]
        static void HideW()
        {
            SimpleRecorder window = (SimpleRecorder)EditorWindow.GetWindow(typeof(SimpleRecorder), true, "Asset Bundle Builder");
            window.Close();
        }

        void OnGUI()
        {
            countStr = EditorGUILayout.TextField(count.ToString(), countStr);
            EditorGUILayout.LabelField("Status: ", count.ToString());
        }
    }
}

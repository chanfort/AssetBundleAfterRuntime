using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Load : MonoBehaviour
{
    Texture2D tex;

    void Start()
    {
        StartCoroutine(LoadBundle());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(LoadBundle());
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            Unload();
        }
    }

    IEnumerator LoadBundle()
    {
        using (UnityWebRequest uwr = UnityWebRequestAssetBundle.GetAssetBundle($"file://{Application.dataPath}/Snapshot/1.unity3d"))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(uwr);

                Unload();
                tex = bundle.LoadAsset<Texture2D>("1");
                bundle.Unload(false);
            }
        }
    }

    void Unload()
    {
        if(tex != null)
        {
            DestroyImmediate(tex, true);
            Resources.UnloadAsset(tex);
            tex = null;
        }
    }

    void OnGUI()
    {
        if(tex != null)
        {
            GUI.DrawTexture(new Rect(100, 100, 512, 512), tex);
        }

        GUI.Label(new Rect(20, 20, 512, 512), $"Press B to load asset bundle or U to unload.");
    }
}

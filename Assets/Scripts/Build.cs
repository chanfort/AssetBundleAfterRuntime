using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections;

public class Build : MonoBehaviour
{
#if UNITY_EDITOR
    public int res = 512;

    public bool buildBundle = false;
    bool isFinished = false;
	public TextureMode textureMode;

	public enum TextureMode
	{
		Screenshot,
		Noise
	}

    void Start()
    {
        StartCoroutine(Starter());
    }

    IEnumerator Starter()
    {
        yield return new WaitForEndOfFrame();
        CreateTexture();
        TrigerEditorMenuBuildBundle();
    }

    void Update()
    {
        if (isFinished)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    void CreateTexture()
    {
        if(!Directory.Exists("Assets/Snapshot"))
		{
			Directory.CreateDirectory("Assets/Snapshot");
			AssetDatabase.Refresh();
		}

        Texture2D tex = null;

        if(textureMode == TextureMode.Screenshot)
        {
            tex = ScreenshotTexture();
        }
        else if(textureMode == TextureMode.Noise)
        {
            tex = NoiseTexture();
        }

        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes("Assets/Snapshot/1.png", bytes);

        AssetDatabase.Refresh();
    }

    Texture2D ScreenshotTexture()
    {
        Texture2D tex = new Texture2D(res, res, TextureFormat.ARGB32, false);

        int width = Screen.width;
        int height = Screen.height;

        int centralW = (int)(width / 2.0f);
        int centralH = (int)(height / 2.0f);

        int xBeg = centralW - res / 2;
        int yBeg = centralH - res / 2;

        int xEnd = centralW + res / 2;
        int yEnd = centralH + res / 2;

        tex.ReadPixels(new Rect(xBeg, yBeg, xEnd, yEnd), 0, 0);
        tex.Apply();

        return tex;
    }

    Texture2D NoiseTexture()
    {
        Texture2D tex = new Texture2D(res, res, TextureFormat.ARGB32, false);
		Color[] pixels = new Color[res * res];

        for (int i = 0; i < res; i++)
        {
            for (int j = 0; j < res; j++)
            {
                int k = res * i + j;
                pixels[k] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            }
        }

        tex.SetPixels(pixels);
        tex.Apply();

        return tex;
    }

    void TrigerEditorMenuBuildBundle()
    {
        isFinished = true;
        EditorApplication.ExecuteMenuItem("Tests/Show Window");
    }
#endif
}

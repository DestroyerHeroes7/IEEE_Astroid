using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public static class UtilScript
{
    #region Game Based
    #endregion
    #region Standard
#pragma warning disable SecurityIntelliSenseCS
    const string logTag = "UtilScript";
    static System.Globalization.NumberFormatInfo nfi;
    public static System.Globalization.CultureInfo culture;
    public static string CacheFolder;
    public static string ssFolder = "";

    static UtilScript()
    {
        CacheFolder = Path.Combine(Application.persistentDataPath, "CachedData");
        culture = new System.Globalization.CultureInfo("en-US", false);
        nfi = culture.NumberFormat;
        nfi.NumberDecimalSeparator = ".";
        nfi.NumberGroupSeparator = ".";
    }

    public static void Log(string gameObjectName, string log)
    {
        Debug.Log("#" + gameObjectName + ": " + log);
    }

    public static void LogError(string gameObjectName, string log)
    {
        Debug.LogError("#" + gameObjectName + ": " + log);
    }

    public static void CacheDelete(string fileName)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            string path = Path.Combine(CacheFolder, fileName);
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch
                {

                }
            }
        }
    }

    public static void CacheCreate(string fileName, byte[] data)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            string path = Path.Combine(CacheFolder, fileName);
            try
            {
                if (!Directory.Exists(CacheFolder))
                    Directory.CreateDirectory(CacheFolder);
                File.WriteAllBytes(path, data);
            }
            catch
            {
            }
        }
    }

    public static string TakeSS()
    {
        Log(logTag, "TakeSS");
        string path = Screen.width + "x" + Screen.height + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss.fff") + ".png";
        ScreenCapture.CaptureScreenshot(string.IsNullOrEmpty(ssFolder) ? path : ssFolder + "/" + path, 1);
        return path;
    }

    public static Texture2D GetTextureFromByte(byte[] data)
    {
        Texture2D texture = new Texture2D(1, 1);
        ImageConversion.LoadImage(texture, data);
        texture.Apply();
        return texture;
    }

    public static Sprite GetSpriteFromTexture(string fileName)
    {
        Texture2D texture = GetTextureFromByte(CacheRead(fileName));
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);
    }

    public static Texture2D ToTexture2D(RenderTexture rTex, int width, int height)
    {
        Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    public static byte[] CacheRead(string fileName)
    {
        string path = Path.Combine(CacheFolder, fileName);
        try
        {
            if (Directory.Exists(CacheFolder))
            {
                return File.ReadAllBytes(Path.Combine(CacheFolder, fileName));
            }
            else
            {
                return null;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static bool IsCached(string fileName)
    {
        string path = Path.Combine(CacheFolder, fileName);

        if (!string.IsNullOrEmpty(fileName))
        {
            try
            {
                if (Directory.Exists(CacheFolder))
                {
                    return File.Exists(path);
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }
        else return false;
    }

    public static void ClearRAM()
    {
        Log(logTag, "Clearing RAM. Collecting Garbage, unloading unused assets");
        GC.Collect();
        Resources.UnloadUnusedAssets();
    }

    //public static bool IsOverYPhone()
    //{
    //    //return ((float)Global.screenWidth / (float)Global.screenHeight) < Global.overYResolutionIncreaseRatio;
    //}

    //public static bool IsTablet()
    //{
    //    bool isTablet = ((Mathf.Sqrt(Mathf.Pow((Global.screenWidth / Screen.dpi), 2) + Mathf.Pow((Global.screenHeight / Screen.dpi), 2))) > 6.5f && Mathf.Max(Global.screenWidth, Global.screenHeight) / Mathf.Min(Global.screenWidth, Global.screenHeight) < 2f);
    //    bool deviceIsIpad = UnityEngine.iOS.Device.generation.ToString().Contains("iPad");
    //    return isTablet;
    //}

    //public static bool IsLowMemoryDevice()
    //{
    //    int memSize = SystemInfo.systemMemorySize;
    //    Log(logTag, "Device Memory: " + memSize);
    //    return memSize <= Global.overYMaxMemoryLimit;
    //}

    public static void SetQualityLevel(int i)
    {
        Log(logTag, "Current Quality Level: " + QualitySettings.GetQualityLevel() + ", Setting to: " + i);
        QualitySettings.SetQualityLevel(i);
    }

    public static int RoundUp(int value)
    {
        return 10 * ((value + 9) / 10);
    }
	
    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static int GenerateRandomInteger(int min, int max, bool maxIncluded = false, List<int> excludedIntegers = default)
    {
        int number = UnityEngine.Random.Range(min, maxIncluded ? max + 1 : max);
        return excludedIntegers != null && excludedIntegers.Contains(number) ? GenerateRandomInteger(min, max, maxIncluded, excludedIntegers) : number;
    }

    public static List<int> GenerateRandomUniqueIntegers(int min, int max, int count)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < count; i++)
            list.Add(GenerateRandomInteger(min, max, false, list));
        return list;
    }
	
	public static bool Between(this int num, int lower, int upper, bool inclusive = true)
	{
		return inclusive
			? lower <= num && num <= upper
			: lower < num && num < upper;
	}

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
	
	public static bool IsEven(this int x)
	{
		return x % 2 == 0;
	}
	
	public static string ColorToHex(Color32 color)
	{
		string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		return hex;
	}

	public static Color HexToColor(string hex)
	{
		byte r = Byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = Byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = Byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r, g, b, 255);
	}
	public static Color ContrastColor(Color c)
	{
		Color ret = Color.white;

		float Y = 0.2126f * c.r + 0.7152f * c.g + 0.0722f * c.b;

		//float S = (Mathf.Max(c.r, c.g, c.b) - Mathf.Min(c.r, c.g, c.b)) / Mathf.Max(c.r, c.g, c.b);

		if (Y > .5f)
			ret = Color.black;
		return ret;
	}

    #region Extension Methods
    public static string FormatAsDotted(this string text)
    {
        if (text != "0")
            return Convert.ToDecimal(text).ToString("N0", nfi);
        else
            return text.ToString();
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static T RemoveRandom<T>(this IList<T> list)
    {
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot remove a random item from an empty list");
        int index = UnityEngine.Random.Range(0, list.Count);
        T item = list[index];
        list.RemoveAt(index);
        return item;
    }

    public static T RandomItem<T>(this IList<T> list)
    {
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static T GetRandomElementFromList<T>(List<T> list) => list[UnityEngine.Random.Range(0, list.Count)];

    public static T GetRandomElementFromList<T>(List<T> list, List<T> excludedItems = default)
    {
        T item = list[UnityEngine.Random.Range(0, list.Count)];
        return excludedItems != null && excludedItems.Contains(item) ? GetRandomElementFromList(list, excludedItems) : item;
    }

    public static List<T> GetRandomElementsFromList<T>(List<T> list, int count, List<T> excludedItems = default)
    {
        List<T> selectedItems = new List<T>();
        if (excludedItems == null)
            excludedItems = new List<T>();

        for (int i = 0; i < count; i++)
        {
            T item = GetRandomElementFromList(list, excludedItems);
            selectedItems.Add(item);
            excludedItems.Add(item);
        }
        return selectedItems;
    }

    public static Bounds GetWorldBound(Transform t)
    {
        Bounds bounds = new Bounds();
        Renderer firstBound = t.GetComponentInChildren<Renderer>(true);
        if (firstBound == null)
            bounds = t.GetComponent<Renderer>().bounds;
        else
        {
            bounds = firstBound.bounds;
            foreach (Renderer r in t.GetComponentsInChildren<Renderer>(true))
                if (firstBound.GetInstanceID() != r.GetInstanceID())
                    bounds.Encapsulate(r.bounds);
        }
        return bounds;
    }

    public static Quaternion GetLookAtRotation(this Transform self, Vector3 target)
    {
        return Quaternion.LookRotation(target - self.position);
    }

    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    public static Color WithAlpha(this Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    public static Component CopyComponent(this GameObject destination, Component original)
    {
        Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

        for (int x = 0; x < fields.Length; x++)
        {
            System.Reflection.FieldInfo field = fields[x];
            if (field.IsDefined(typeof(SerializeField), false))
                field.SetValue(copy, field.GetValue(original));
        }

        return copy;
    }

    public static void AddLocalX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y, transform.localPosition.z);
    }

    public static void AddLocalY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + y, transform.localPosition.z);
    }

    public static void AddLocalZ(this Transform transform, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + z);
    }

    public static void SetLocalX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }

    public static void SetLocalY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }

    public static void SetLocalZ(this Transform transform, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
    }
	
	public static void AddX(this Transform transform, float x)
    {
        transform.position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
    }

    public static void AddY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + y, transform.position.z);
    }

    public static void AddZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + z);
    }
    public static void SetX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public static void SetY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    public static void SetZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }
	
     public static void SetSnapToChild(this UnityEngine.UI.ScrollRect scrollRect, RectTransform contentPanel, RectTransform item, bool isX)
    {
        Vector2 finalResult = (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position) - (Vector2)scrollRect.transform.InverseTransformPoint(item.position);
        if (isX)
            contentPanel.anchoredPosition = new Vector2(finalResult.x - item.sizeDelta.x, contentPanel.anchoredPosition.y);
        else
            contentPanel.anchoredPosition = new Vector2(contentPanel.anchoredPosition.x, finalResult.y - item.sizeDelta.y);
    }
	
    public static void LookAtY(this Transform transform, Vector3 point)
    {
        var lookPos = point - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
    }
    #endregion

    public static void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public static void ClearEditorLog()
    {
#if UNITY_EDITOR
        var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.SceneView));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
#endif
    }
#if RemoteConfig
    public static void SetJSONData(JSONObject jsonRaw)
    {
        try
        {

        }
        catch (Exception e)
        {
            LogError(logTag, "JSON READ ERROR! RETURNING DEFAULT (PREF): " + e.Message);
            SetJSONData(new JSONObject(Global.dataJSON));
        }
    }
#endif
#if JSON
public static string GetString(string key, JSONObject data, bool catcher = false)
    {
        try
        {
            string temp = "";
            if (data.HasField(key) && !data.GetField(key).IsNull)
                temp = data.GetField(key).str;
            return temp;
        }
        catch (System.FormatException)
        {

            if (catcher == false)
                return GetInt(key, data, true).ToString();
            else
                return "";
        }
    }

    public static int GetInt(string key, JSONObject data, bool catcher = false)
    {
        try
        {
            int temp = 0;
            if (data.HasField(key) && !data.GetField(key).IsNull)
                temp = int.Parse(data.GetField(key).ToString());
            return temp;
        }
        catch (System.FormatException)
        {
            if (catcher == false)
            {
                return int.Parse(GetString(key, data, true));
            }
            else
                return 0;
        }
    }

    public static bool GetIntAsBool(string key, JSONObject data)
    {
        if (data.HasField(key) && !data.GetField(key).IsNull)
            return GetInt(key, data) == 0 ? false : true;
        else
            return false;
    }

    public static bool GetBool(string key, JSONObject data)
    {
        bool temp = false;
        if (data.HasField(key) && !data.GetField(key).IsNull)
        {
            if (data.GetField(key).IsBool)
                return data.GetField(key).b;
            else if (data.GetField(key).IsString)
                return data.GetField(key).str == "true" ? true : false;
            else
                return false;
        }
        return temp;
    }

    public static float GetFloat(string key, JSONObject data)
    {
        float temp = 0;
        if (data.HasField(key) && !data.GetField(key).IsNull)
            temp = data.GetField(key).f;
        return temp;
    }

    public static long GetLong(string key, JSONObject data)
    {
        long temp = 0;
        if (data.HasField(key) && !data.GetField(key).IsNull)
            temp = (long)data.GetField(key).n;
        return temp;
    }
#endif
#pragma warning restore SecurityIntelliSenseCS
    #endregion
}
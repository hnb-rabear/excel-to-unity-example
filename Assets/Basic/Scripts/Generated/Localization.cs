namespace Excel2Unity.Basic
{
	/**
	 * Author RadBear - nbhung71711@gmail.com - 2017-2021
	 **/
	
	using System.Collections.Generic;
	using System.Text;
	using UnityEngine;
	using System;
	using UnityEngine.UI;
	using System.Collections;
	
	public static class Localization
	{
		public enum ID 
		{
			NONE = -1,
			message_1 = 0, message_2, message_3, message_4, message_5, content_1, content_2, content_3, content_4, content_5, title_1, title_2, title_3, title_4, title_5, title_6, whatever_msg, hero_name_1, hero_name_2, hero_name_3, hero_name_4, hero_name_5,
		}
		public const int
			message_1 = 0, message_2 = 1, message_3 = 2, message_4 = 3, message_5 = 4, content_1 = 5, content_2 = 6, content_3 = 7, content_4 = 8, content_5 = 9, title_1 = 10, title_2 = 11, title_3 = 12, title_4 = 13, title_5 = 14, title_6 = 15, whatever_msg = 16, hero_name_1 = 17, hero_name_2 = 18, hero_name_3 = 19, hero_name_4 = 20, hero_name_5 = 21;
		public static readonly string[] idString = new string[]
		{
			"message_1", "message_2", "message_3", "message_4", "message_5", "content_1", "content_2", "content_3", "content_4", "content_5", "title_1", "title_2", "title_3", "title_4", "title_5", "title_6", "whatever_msg", "hero_name_1", "hero_name_2", "hero_name_3", "hero_name_4", "hero_name_5",
		};
		public static readonly Dictionary<string, string> languageDict = new Dictionary<string, string>() {  { "english", "Localization_english" }, { "spanish", "Localization_spanish" }, { "japan", "Localization_japan" }, { "chinese", "Localization_chinese" }, { "korean", "Localization_korean" }, { "thai", "Localization_thai" }, };
		public static readonly string defaultLanguage = "english";
	
		public static string Folder = "Data";
	    private static StringBuilder mStringBuilder = new StringBuilder();
	    public static Action onLanguageChanged;
	    private static string[] mTexts;
	    private static string mLanguageTemp;
	    public static string currentLanguage
	    {
	        get { return PlayerPrefs.GetString("currentLanguage", defaultLanguage); }
	        set
	        {
	            if (currentLanguage != value && languageDict.ContainsKey(value))
	            {
	                PlayerPrefs.SetString("currentLanguage", value);
	                Init();
	            }
	        }
	    }
		public static int curLangIndex = -1;
	
	    public static void Init()
		{
	        var lang = mLanguageTemp;
			if (!languageDict.ContainsKey(currentLanguage))
			{
				if (string.IsNullOrEmpty(mLanguageTemp))
					lang = defaultLanguage;
			}
			else lang = currentLanguage;
					
			if (mLanguageTemp != lang)
			{
	#if UNITY_EDITOR
				Debug.Log($"Init {nameof(Localization)}");
	#endif
				string file = languageDict[lang];
				var asset = Resources.Load<TextAsset>(Folder + "/" + file);
				if (asset != null)
				{
					string json = asset.text;
					mTexts = GetJsonList(json);
					mLanguageTemp = lang;
					int index = 0;
	                foreach (var item in languageDict)
	                {
	                    if (lang == item.Key)
	                    {
	                        curLangIndex = index;
	                        break;
	                    }
	                    index++;
	                }
	                for (int i = 0; i < mDynamicTexts.Count; i++)
	                {
	                    if (mDynamicTexts[i].obj == null)
	                    {
	                        mDynamicTexts.RemoveAt(i);
	                        i--;
	                        continue;
	                    }
	                    mDynamicTexts[i].Refresh();
	                }
					onLanguageChanged?.Invoke();
					Resources.UnloadAsset(asset);
				}
			}
		}
		
		public static IEnumerator InitAsync()
		{
	        var lang = mLanguageTemp;
			if (!languageDict.ContainsKey(currentLanguage))
			{
				if (string.IsNullOrEmpty(mLanguageTemp))
					lang = defaultLanguage;
			}
			else lang = currentLanguage;
					
			if (mLanguageTemp != lang)
			{
	#if UNITY_EDITOR
				Debug.Log($"Init {nameof(Localization)}");
	#endif
				string file = languageDict[lang];
				var request = Resources.LoadAsync<TextAsset>(Folder + "/" + file);
				while (!request.isDone)
					yield return null;
				if (request.asset != null)
				{
					mTexts = GetJsonList((request.asset as TextAsset).text);
					mLanguageTemp = lang;
					int index = 0;
	                foreach (var item in languageDict)
	                {
	                    if (lang == item.Key)
	                    {
	                        curLangIndex = index;
	                        break;
	                    }
	                    index++;
	                }
	                for (int i = 0; i < mDynamicTexts.Count; i++)
	                {
	                    if (mDynamicTexts[i].obj == null)
	                    {
	                        mDynamicTexts.RemoveAt(i);
	                        i--;
	                        continue;
	                    }
	                    mDynamicTexts[i].Refresh();
	                }
					onLanguageChanged?.Invoke();
					Resources.UnloadAsset(request.asset);
				}
			}
		}
	
	    private static string[] GetTexts()
	    {
	        if (mTexts == null)
	            Init();
	        return mTexts;
	    }
	
	    public static StringBuilder Get(ID pId)
	    {
	        mStringBuilder.Remove(0, mStringBuilder.Length);
	        return mStringBuilder.Append(GetTexts()[(int)pId]);
	    }
	
	    public static StringBuilder Get(ID pId, params object[] pArgs)
	    {
	        mStringBuilder.Remove(0, mStringBuilder.Length);
	        mStringBuilder.AppendFormat(GetTexts()[(int)pId], pArgs);
	        return mStringBuilder;
	    }
	
	    public static StringBuilder Get(int pId)
	    {
	        mStringBuilder.Remove(0, mStringBuilder.Length);
	        if (pId >= 0 && pId < GetTexts().Length)
	            mStringBuilder.Append(GetTexts()[pId]);
	#if UNITY_EDITOR
	        else
	            Debug.LogError("Not found id " + pId);
	#endif
	        return mStringBuilder;
	    }
	
	    public static StringBuilder Get(int pId, params object[] pArgs)
	    {
	        mStringBuilder.Remove(0, mStringBuilder.Length);
	        mStringBuilder.AppendFormat(GetTexts()[pId], pArgs);
	        return mStringBuilder;
	    }
	
	    public static StringBuilder Get(string pIdString)
	    {
	        mStringBuilder.Remove(0, mStringBuilder.Length);
	        int index = GetIndex(pIdString);
	        if (index >= 0)
	            mStringBuilder.Append(GetTexts()[index]);
	#if UNITY_EDITOR
	        else
	            Debug.LogError("Not found idString " + pIdString);
	#endif
	        return mStringBuilder;
	    }
	
	    public static int GetIndex(string pIdString)
	    {
	        for (int i = 0; i < idString.Length; i++)
	            if (pIdString == idString[i])
	                return i;
	#if UNITY_EDITOR
			Debug.LogError("Not found " + pIdString);
	#endif
	        return -1;
	    }
	
	    public static StringBuilder Get(string pIdString, params object[] pArgs)
	    {
	        mStringBuilder.Remove(0, mStringBuilder.Length);
	        int index = GetIndex(pIdString);
	        if (index >= 0)
	            mStringBuilder.AppendFormat(GetTexts()[index], pArgs);
	#if UNITY_EDITOR
	        else
	            Debug.LogError("Not found idString " + pIdString);
	#endif
	        return mStringBuilder;
	    }
	
	    public static StringBuilder Get(string pIdString, ref int pIndex)
	    {
	        mStringBuilder.Remove(0, mStringBuilder.Length);
	        pIndex = GetIndex(pIdString);
	        if (pIndex >= 0)
	            mStringBuilder.Append(GetTexts()[pIndex]);
	#if UNITY_EDITOR
	        else
	            Debug.LogError("Not found idString " + pIdString);
	#endif
	        return mStringBuilder;
	    }
	
	    public static StringBuilder Get(string pIdString, ref int pIndex, params object[] pArgs)
	    {
	        mStringBuilder.Remove(0, mStringBuilder.Length);
	        pIndex = GetIndex(pIdString);
	        if (pIndex >= 0)
	            mStringBuilder.AppendFormat(GetTexts()[pIndex], pArgs);
	#if UNITY_EDITOR
	        else
	            Debug.LogError("Not found idString " + pIdString);
	#endif
	        return mStringBuilder;
	    }
	
	    public static StringBuilder Get(string pIdString, ref ID pId)
	    {
	        mStringBuilder.Remove(0, mStringBuilder.Length);
	        int index = GetIndex(pIdString);
	        if (index >= 0)
	        {
	            pId = (ID)index;
	            mStringBuilder.Append(GetTexts()[index]);
	        }
	        else
	        {
	            pId = ID.NONE;
	#if UNITY_EDITOR
	            Debug.LogError("Not found idString " + pIdString);
	#endif
	        }
	        return mStringBuilder;
	    }
	
	    public static StringBuilder Get(string pIdString, ref ID pId, params object[] pArgs)
	    {
	        mStringBuilder.Remove(0, mStringBuilder.Length);
	        int index = GetIndex(pIdString);
	        if (index >= 0)
	        {
	            pId = (ID)index;
	            mStringBuilder.AppendFormat(GetTexts()[index], pArgs);
	        }
	        else
	        {
	            pId = ID.NONE;
	#if UNITY_EDITOR
	            Debug.LogError("Not found idString " + pIdString);
	#endif
	        }
	        return mStringBuilder;
	    }
	
	    public static string[] GetJsonList(string json)
	    {
	        var sb = new StringBuilder();
	        string newJson = sb.Append("{").Append("\"array\":").Append(json).Append("}").ToString();
	        StringArray wrapper = JsonUtility.FromJson<StringArray>(newJson);
	        return wrapper.array;
	    }
		
		public static string GetLanguage(int index)
		{
			int i = 0;
			foreach (var lang in languageDict)
			{
				if (i == index)
					return lang.Key;
				i++;
			}
			return "";
		}
	
	    [System.Serializable]
	    private class StringArray
	    {
	        public string[] array;
	    }
	
	    private static List<DynamicText> mDynamicTexts = new List<DynamicText>();
	    public static void RegisterDynamicText(GameObject pComponent, string pLocalizedKey, params string[] pArgs)
	    {
	        int key = GetIndex(pLocalizedKey);
	        RegisterDynamicText(pComponent, key, pArgs);
	    }
	
	    public static void RegisterDynamicText(GameObject pComponent, int pLocalizedKey, params string[] pArgs)
	    {
	        if (pComponent == null)
	            return;
	        for (int i = 0; i < mDynamicTexts.Count; i++)
	        	if (mDynamicTexts[i].obj == pComponent)
	            {
	                if (mDynamicTexts[i].key != pLocalizedKey || mDynamicTexts[i].args != pArgs)
	                {
	                    mDynamicTexts[i].curLangIndex = -1;
	                    mDynamicTexts[i].key = pLocalizedKey;
	                    mDynamicTexts[i].args = pArgs;
	                }
	                mDynamicTexts[i].Refresh();
	                return;
	            }
	        var text = new DynamicText(pLocalizedKey, pComponent, pArgs);
	        text.Refresh();
	        mDynamicTexts.Add(text);
	#if UNITY_EDITOR
	        var localizedText = pComponent.GetComponent<LocalizationText>();
	        if (localizedText != null)
	        {
	            Debug.LogError($"{pComponent.name} should not have LocalizationText!");
	            GameObject.Destroy(localizedText);
	        }
	#endif
	    }
	
	    public static void UnregisterDynamicText(Text pComponent)
	    {
	        for (int i = 0; i < mDynamicTexts.Count; i++)
	            if (mDynamicTexts[i].obj == pComponent)
	            {
	                mDynamicTexts.RemoveAt(i);
	                return;
	            }
	    }
	
	    public class DynamicText
	    {
	        public int key = -1;
	        public GameObject obj;
	        public string[] args;
	        public int curLangIndex = -1;
	        public DynamicText(int pIntKey, GameObject pObj, params string[] pArgs)
	        {
	            key = pIntKey;
	            obj = pObj;
	            args = pArgs;
	        }
	        public void Refresh()
	        {
	            if (curLangIndex != Localization.curLangIndex)
	            {
	#if UNITY_EDITOR
	                var text = Get(key);
	                try
	                {
	#endif
                        var value = "";
                        if (args != null)
                            value = Get(key, args).ToString();
                        else
                            value = Get(key).ToString();

                        if (obj.TryGetComponent(out TMPro.TextMeshProUGUI txtPro))
                            txtPro.text = value;
                        else if (obj.TryGetComponent(out Text txt))
                            txt.text = value;
                        
                        curLangIndex = Localization.curLangIndex;
	#if UNITY_EDITOR
	                }
	                catch (Exception ex)
	                {
	                    Debug.LogError(text + "\n" + ex);
	                }
	#endif
	            }
	        }
	    }
		
	#if UNITY_EDITOR
		public static void CreateClassContainsRefinedIds(string pPath)
		{
			var ids = FindUsingIDs(pPath);
			Debug.Log($"Writing {ids.Count} ids to {pPath}");
			CreateClassContainsRefinedIds(ids);
		}
		public static List<string> FindUsingIDs(string pPath)
		{
			var results = new List<string>();
			var assetIds = UnityEditor.AssetDatabase.FindAssets("t:prefab", new[] { pPath });
			foreach (var guid in assetIds)
			{
				var obj = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(UnityEditor.AssetDatabase.GUIDToAssetPath(guid));
				var components = obj.GetComponentsInChildren<LocalizationText>(true);
				foreach (var com in components)
				{
					string id = com.LocalizedIDString;
					id = id.Replace(" ", "_");
					id = System.Text.RegularExpressions.Regex.Replace(id, "[^a-zA-Z0-9_.]+", "", System.Text.RegularExpressions.RegexOptions.Compiled);
					if (!results.Contains(id))
						results.Add(id);
				}
			}
			return results;
		}
		public static void CreateClassContainsRefinedIds(List<string> pRefinedIds)
		{
			string enumIds = "";
			string intIds = "";
			for (int i = 0; i < pRefinedIds.Count; i++)
			{
				enumIds += $"{nameof(Localization)}.ID.{pRefinedIds[i]}";
				intIds += $"{nameof(Localization)}.{pRefinedIds[i]}";
				if (i < pRefinedIds.Count - 1)
				{
					enumIds += ", ";
					intIds += ", ";
				}
			}
			if (enumIds == "")
				return;
			string template = GetTemplateClassContainsRefinedIds();
			string result = template.Replace("ENUM_IDS", enumIds).Replace("INT_IDS", intIds);
			string directoryPath = $"{Application.dataPath}/LocalizationRefinedIds";
			string path = $"{directoryPath}/{nameof(Localization)}RefinedIds.cs";
			if (!System.IO.Directory.Exists(directoryPath))
				System.IO.Directory.CreateDirectory(directoryPath);
			System.IO.File.WriteAllText(path, result);
		}
		private static string GetTemplateClassContainsRefinedIds()
		{
			string content = $"namespace {typeof(Localization).Namespace}\n"
				+ "{\n"
				+ "\tusing System.Collections.Generic;\n"
				+ $"\tpublic class LocalizationRefinedIds\n"
				+ "\t{\n"
				+ "\t\tpublic static readonly List<Localization.ID> enumIds = new List<Localization.ID> { ENUM_IDS };\n"
				+ "\t\tpublic static readonly List<int> intIds = new List<int> { INT_IDS };\n"
				+ "\t}\n"
				+ "}";
			return content;
		}
		[UnityEditor.MenuItem("Assets/Create/RCore/Localization Refined Ids/Create LocalizationRefinedIds")]
		public static void CreateLocalizationRefinedIds()
		{
			string currentPath = UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeObject);
			CreateClassContainsRefinedIds(currentPath);
		}
	#endif
	}
	
	public class LocalizationGetter
	{
	    private static Dictionary<string, int> cachedTexts = new Dictionary<string, int>();
	
	    public string key;
	    public string defaultStr;
	
	    private int mIndex = -1;
	    private bool mCheckKey;
	
	    public LocalizationGetter(string pKey, string pDefault)
	    {
	        key = pKey;
	        defaultStr = pDefault;
	
	#if UNITY_EDITOR
	        //In Editor we check it soon to find missing localization
	        Localization.Get(key, ref mIndex);
	        mCheckKey = true;
	#endif
	    }
	
	    public string Get()
	    {
	        if (!mCheckKey)
	        {
	            Localization.Get(key, ref mIndex);
	            mCheckKey = true;
	        }
	
	        if (mIndex == -1)
	            return defaultStr;
	        var text = Localization.Get(mIndex).ToString();
	        if (string.IsNullOrEmpty(text))
	            return defaultStr;
	        else
	            return Localization.Get(mIndex).ToString().Replace("\\n", "\u000a");
	    }
	
	    public static string GetCached(string pKey)
	    {
	        if (cachedTexts.ContainsKey(pKey))
	        {
	            int id = cachedTexts[pKey];
	            if (id != -1)
	            {
	                string text = Localization.Get(cachedTexts[pKey]).ToString();
	                return !string.IsNullOrEmpty(text) ? text : pKey;
	            }
	            return pKey;
	        }
	        else
	        {
	            int id = -1;
	            string text = Localization.Get(pKey, ref id).ToString();
	            cachedTexts.Add(pKey, id);
	            return !string.IsNullOrEmpty(text) ? text : pKey;
	        }
	    }
	}
}

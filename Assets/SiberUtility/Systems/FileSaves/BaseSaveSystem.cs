using UnityEngine;

namespace SiberUtility.Systems.FileSaves
{
    /// <summary> 儲存系統 </summary>
    /// 需繼承來使用，尤其是設定 FileName
    /// <typeparam name="T"> 自定義 Data </typeparam>
    public abstract class BaseSaveSystem<T> : ISaveSystem<T> where T : new()
    {
    #region ========== [Protected Variables] ==========

        protected virtual string FileName        => "My Unity Game";
        protected virtual string Client_FileName => $"{FileName}.json";
        protected virtual string WebGL_FileName  => FileName;
        protected virtual string DataPath        => Application.persistentDataPath;

        protected T saveFile;

    #endregion

    #region ========== [Interface Methods] ==========

        public virtual void Save(T saveFile)
        {
        #if UNITY_WEBGL
            SaveHelper.SaveByPlayerPrefs(WebGL_FileName, saveFile);
        #else
            SaveHelper.SaveByJson(Client_FileName, saveFile, DataPath);
        #endif
            this.saveFile = saveFile;
        }

        public virtual T Load()
        {
            return saveFile ??= GetFile();
        }

    #endregion

    #region ========== [Private Methods] ==========

        /// <summary> 獲得記錄檔 </summary>
        protected T GetFile()
        {
        #if UNITY_WEBGL
            return GetWebGL_SaveFile();
        #else
            return GetClient_SaveFile();
        #endif
        }

        /// <summary> 讀取 Json 紀錄檔 (本地端 用) </summary>
        protected T GetClient_SaveFile()
        {
            var loadSaveFile = SaveHelper.LoadFromJson<T>(Client_FileName, DataPath);
            if (loadSaveFile != null) return loadSaveFile;
            loadSaveFile = new T();
            SaveHelper.SaveByJson(Client_FileName, loadSaveFile, DataPath);
            return loadSaveFile;
        }

        /// <summary> 讀取 PlayerPrefs 檔案 (WebGL 用) </summary>
        protected T GetWebGL_SaveFile()
        {
            var loadSaveFile = SaveHelper.LoadFromPlayerPrefs<T>(WebGL_FileName);
            if (loadSaveFile != null) return loadSaveFile;
            loadSaveFile = new T();
            SaveHelper.SaveByPlayerPrefs(Client_FileName, loadSaveFile);
            return loadSaveFile;
        }

    #endregion
    }

    // <Lazy Example>
    // public class GameSaveSystem : BaseSaveSystem<GameSaveFile>
    // {
    // #region ========== [Protected Variables] ==========
    //
    //     protected override string FileName => GameName;
    //
    // #endregion
    //
    // #region ========== [Private Variables] ==========
    //
    //     private const string GameName = "MyGame";
    //
    // #endregion
    //
    // #if UNITY_EDITOR
    //
    //     [MenuItem("MyGame/Delete Game SaveFile(Json)")]
    //     public static void OnDeleteInMenuJson()
    //     {
    //         var fileName = $"{GameName}.json";
    //         SaveHelper.DeleteJson(fileName, Application.persistentDataPath);
    //     }
    //
    //     [MenuItem("MyGame/Delete Game PlayerPrefs")]
    //     public static void OnDeleteInMenuPlayerPrefs()
    //     {
    //         SaveHelper.DeletePlayerPrefs();
    //     }
    //
    // #endif
    // }
}
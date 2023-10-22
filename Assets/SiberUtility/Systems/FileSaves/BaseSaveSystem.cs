using UnityEditor;

namespace SiberUtility.Systems.FileSaves
{
    /// <summary> 儲存系統 </summary>
    /// 需繼承來使用，尤其是設定 FileName
    public abstract class BaseSaveSystem : ISaveSystem
    {
    #region ========== [Protected Variables] ==========

        protected static string FileName => "Game";

    #endregion

    #region ========== [Private Variables] ==========

        private static string   client_FileName = FileName + ".json";
        private static string   webGL_FileName  = FileName;
        private        SaveFile saveFile;
    
    #endregion
    
    #region ========== [Interface Methods] ==========

        void ISaveSystem.Save(SaveFile saveFile)
        {
        #if UNITY_WEBGL
        SaveHelper.SaveByPlayerPrefs(webGLFileName, saveFile);
        #else
            SaveHelper.SaveByJson(client_FileName, saveFile);
        #endif
            this.saveFile = saveFile;
        }

        SaveFile ISaveSystem.Load()
        {
            return saveFile ??= GetFile();
        }

    #endregion

    #region ========== [Private Methods] ==========

        /// <summary> 獲得記錄檔 </summary>
        private SaveFile GetFile()
        {
        #if UNITY_WEBGL
        return GetWebGL_SaveFile();
        #else
            return GetClient_SaveFile();
        #endif
        }

        /// <summary> 讀取 Json 紀錄檔 (本地端 用) </summary>
        private SaveFile GetClient_SaveFile()
        {
            var loadSaveFile = SaveHelper.LoadFromJson<SaveFile>(client_FileName);
            if (loadSaveFile != null) return loadSaveFile;
            loadSaveFile = new SaveFile();
            SaveHelper.SaveByJson(client_FileName, loadSaveFile);
            return loadSaveFile;
        }

        /// <summary> 讀取 PlayerPrefs 檔案 (WebGL 用) </summary>
        private SaveFile GetWebGL_SaveFile()
        {
            var loadSaveFile = SaveHelper.LoadFromPlayerPrefs<SaveFile>(webGL_FileName);
            if (loadSaveFile != null) return loadSaveFile;
            loadSaveFile = new SaveFile();
            SaveHelper.SaveByPlayerPrefs(client_FileName, loadSaveFile);
            return loadSaveFile;
        }

    #endregion

    #if UNITY_EDITOR

        [MenuItem("SiberUtility/[PC] Delete SaveFile (Json)")]
        public static void OnDeleteInMenuJson()
        {
            SaveHelper.DeleteJson(client_FileName);
        }

        [MenuItem("SiberUtility/[WebGL] Delete SaveFile (PlayerPrefs)")]
        public static void OnDeleteInMenuPlayerPrefs()
        {
            SaveHelper.DeletePlayerPrefs();
        }

    #endif
    }
}
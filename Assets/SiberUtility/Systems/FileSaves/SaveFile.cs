using System;

namespace SiberUtility.Systems.FileSaves
{
    /// <summary> 整筆遊戲資料 </summary>
    [Serializable]
    public class SaveFile { }
}

// 以下為範例~
// [Serializable]
// public class SaveFile
// {
//     public GameSetting GameSetting = new GameSetting();
// }

// /// <summary> 遊戲設定 </summary>
// [Serializable]
// public class GameSetting
// {
//     public float SFXValue       = 9f;
//     public float BGMValue       = 9f;
//     public float ENVValue       = 9f;
//     public float VibrationValue = 1f;
//     public int   ScreenX        = Screen.currentResolution.width;
//     public int   ScreenY        = Screen.currentResolution.height;
//
//     public bool   IsBoarderlessFullScreen = true;
//     public string Language                = "Chinese(Traditional)";
// }
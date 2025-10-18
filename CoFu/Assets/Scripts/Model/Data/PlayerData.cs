// using UnityEngine;
// using System.Collections.Generic;

// public class PlayerData : Singleton<PlayerData>
// {
//     // ============================================
//     // SAVE KEYS (Hata yapmamak için const)
//     // ============================================
//     private const string KEY_WHITE_ESSENCE = "WhiteEssence";
//     private const string KEY_UNLOCKED_LEVELS = "UnlockedLevels";
//     private const string KEY_HIGH_SCORES = "HighScores";
//     private const string KEY_TOTAL_PLAYTIME = "TotalPlaytime";
//     private const string KEY_MUSIC_VOLUME = "MusicVolume";
//     private const string KEY_SFX_VOLUME = "SFXVolume";
//     private const string KEY_FIRST_TIME = "FirstTime";

//     // ============================================
//     // RUNTIME DATA (Cache - her seferinde okuma yapma)
//     // ============================================
//     public int WhiteEssence { get; private set; }
//     public List<int> UnlockedLevels { get; private set; }
//     public Dictionary<int, int> HighScores { get; private set; }//level-score
//     public float TotalPlaytime { get; private set; }
//     public float MusicVolume { get; private set; }
//     public float SFXVolume { get; private set; }
//     public bool IsFirstTime { get; private set; }

//     // ============================================
//     // INIT - Oyun açılışında çağrılır
//     // ============================================
//     public void Awake()
//     {
//         LoadAllData();
//     }

//     private void LoadAllData()
//     {
//         // Para
//         WhiteEssence = PlayerPrefs.GetInt(KEY_WHITE_ESSENCE, 0);

//         // İlk kez mi oynuyor?
//         IsFirstTime = PlayerPrefs.GetInt(KEY_FIRST_TIME, 1) == 1;
//         if (IsFirstTime)
//         {
//             InitializeFirstTime();
//         }

//         // Açık level'lar
//         string unlockedStr = PlayerPrefs.GetString(KEY_UNLOCKED_LEVELS, "1");
//         UnlockedLevels = ParseIntList(unlockedStr);

//         // High score'lar
//         HighScores = new Dictionary<int, int>();
//         string scoresStr = PlayerPrefs.GetString(KEY_HIGH_SCORES, "");
//         if (!string.IsNullOrEmpty(scoresStr))
//         {
//             string[] pairs = scoresStr.Split(';');
//             foreach (string pair in pairs)
//             {
//                 string[] kv = pair.Split(':');
//                 if (kv.Length == 2)
//                 {
//                     int level = int.Parse(kv[0]);
//                     int score = int.Parse(kv[1]);
//                     HighScores[level] = score;
//                 }
//             }
//         }

//         // Ses ayarları
//         MusicVolume = PlayerPrefs.GetFloat(KEY_MUSIC_VOLUME, 0.7f);
//         SFXVolume = PlayerPrefs.GetFloat(KEY_SFX_VOLUME, 0.8f);

//         // Toplam oyun süresi
//         TotalPlaytime = PlayerPrefs.GetFloat(KEY_TOTAL_PLAYTIME, 0f);
//     }

//     // ============================================
//     // WHITE ESSENCE YÖNETİMİ
//     // ============================================
//     public void AddWhiteEssence(int amount)
//     {
//         WhiteEssence += amount;
//         PlayerPrefs.SetInt(KEY_WHITE_ESSENCE, WhiteEssence);
//         PlayerPrefs.Save(); // Hemen kaydet
//     }

//     public bool SpendWhiteEssence(int amount)
//     {
//         if (WhiteEssence >= amount)
//         {
//             WhiteEssence -= amount;
//             PlayerPrefs.SetInt(KEY_WHITE_ESSENCE, WhiteEssence);
//             PlayerPrefs.Save();
//             return true;
//         }
//         return false;
//     }

//     // ============================================
//     // LEVEL YÖNETİMİ
//     // ============================================
//     public void UnlockLevel(int levelNumber)
//     {
//         if (!UnlockedLevels.Contains(levelNumber))
//         {
//             UnlockedLevels.Add(levelNumber);
//             string levelsStr = string.Join(",", UnlockedLevels);
//             PlayerPrefs.SetString(KEY_UNLOCKED_LEVELS, levelsStr);
//             PlayerPrefs.Save();
//         }
//     }

//     public bool IsLevelUnlocked(int levelNumber)
//     {
//         return UnlockedLevels.Contains(levelNumber);
//     }

//     // ============================================
//     // HIGH SCORE
//     // ============================================
//     public void SaveHighScore(int levelNumber, int score)
//     {
//         // Sadece önceki rekordan yüksekse kaydet
//         if (!HighScores.ContainsKey(levelNumber) || score > HighScores[levelNumber])
//         {
//             HighScores[levelNumber] = score;

//             // Dictionary'yi string'e çevir: "1:100;2:250;3:180"
//             List<string> pairs = new List<string>();
//             foreach (var kvp in HighScores)
//             {
//                 pairs.Add($"{kvp.Key}:{kvp.Value}");
//             }
//             string scoresStr = string.Join(";", pairs);

//             PlayerPrefs.SetString(KEY_HIGH_SCORES, scoresStr);
//             PlayerPrefs.Save();
//         }
//     }

//     public int GetHighScore(int levelNumber)
//     {
//         return HighScores.ContainsKey(levelNumber) ? HighScores[levelNumber] : 0;
//     }

//     // ============================================
//     // SES AYARLARI
//     // ============================================
//     public void SetMusicVolume(float volume)
//     {
//         MusicVolume = Mathf.Clamp01(volume);
//         PlayerPrefs.SetFloat(KEY_MUSIC_VOLUME, MusicVolume);
//         PlayerPrefs.Save();

//         // Ses sistemine bildir
//         // AudioManager.Instance.SetMusicVolume(MusicVolume);
//     }

//     public void SetSFXVolume(float volume)
//     {
//         SFXVolume = Mathf.Clamp01(volume);
//         PlayerPrefs.SetFloat(KEY_SFX_VOLUME, SFXVolume);
//         PlayerPrefs.Save();

//         // AudioManager.Instance.SetSFXVolume(SFXVolume);
//     }

//     // ============================================
//     // PLAYTIME TRACKER
//     // ============================================
//     public void AddPlaytime(float seconds)
//     {
//         TotalPlaytime += seconds;
//         PlayerPrefs.SetFloat(KEY_TOTAL_PLAYTIME, TotalPlaytime);
//         // Playtime'ı her saniye kaydetme - level sonunda kaydet
//     }

//     // ============================================
//     // FIRST TIME SETUP
//     // ============================================
//     private void InitializeFirstTime()
//     {
//         WhiteEssence = 100; // Başlangıç parası
//         UnlockedLevels = new List<int> { 1 }; // Sadece level 1 açık
//         MusicVolume = 0.7f;
//         SFXVolume = 0.8f;

//         SaveAllData();

//         PlayerPrefs.SetInt(KEY_FIRST_TIME, 0);
//         PlayerPrefs.Save();
//     }

//     // ============================================
//     // UTILITY
//     // ============================================
//     private List<int> ParseIntList(string str)
//     {
//         List<int> result = new List<int>();
//         string[] parts = str.Split(',');
//         foreach (string part in parts)
//         {
//             if (int.TryParse(part, out int value))
//             {
//                 result.Add(value);
//             }
//         }
//         return result;
//     }

//     private void SaveAllData()
//     {
//         PlayerPrefs.SetInt(KEY_WHITE_ESSENCE, WhiteEssence);
//         PlayerPrefs.SetString(KEY_UNLOCKED_LEVELS, string.Join(",", UnlockedLevels));
//         PlayerPrefs.SetFloat(KEY_MUSIC_VOLUME, MusicVolume);
//         PlayerPrefs.SetFloat(KEY_SFX_VOLUME, SFXVolume);
//         PlayerPrefs.Save();
//     }

//     // ============================================
//     // DEBUG - Editör'de test için
//     // ============================================
//     [ContextMenu("Reset All Data")]
//     public void ResetAllData()
//     {
//         PlayerPrefs.DeleteAll();
//         LoadAllData();
//         Debug.Log("Tüm veriler sıfırlandı!");
//     }

//     [ContextMenu("Add 100 White Essence")]
//     public void DebugAddMoney()
//     {
//         AddWhiteEssence(100);
//         Debug.Log($"Para eklendi! Toplam: {WhiteEssence}");
//     }

//     [ContextMenu("Unlock All Levels")]
//     public void DebugUnlockAll()
//     {
//         for (int i = 1; i <= 20; i++)
//         {
//             UnlockLevel(i);
//         }
//         Debug.Log("Tüm level'lar açıldı!");
//     }
// }

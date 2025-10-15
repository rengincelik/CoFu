using UnityEngine;
using System;

// NOT: Bu kod Google AdMob Unity SDK kullanır
// Package Manager → Add package from git URL → 
// https://github.com/googleads/googleads-mobile-unity.git

public class AdManager : Singleton<AdManager>
{
    [Header("Ad IDs - PRODUCTION")]
    [SerializeField] private string androidAppId = "ca-app-pub-XXXXXXXX~XXXXXXXXXX";
    [SerializeField] private string iosAppId = "ca-app-pub-XXXXXXXX~XXXXXXXXXX";

    [Header("Rewarded Ad IDs")]
    [SerializeField] private string androidRewardedId = "ca-app-pub-XXXXXXXX/XXXXXXXXXX";
    [SerializeField] private string iosRewardedId = "ca-app-pub-XXXXXXXX/XXXXXXXXXX";

    [Header("Interstitial Ad IDs")]
    [SerializeField] private string androidInterstitialId = "ca-app-pub-XXXXXXXX/XXXXXXXXXX";
    [SerializeField] private string iosInterstitialId = "ca-app-pub-XXXXXXXX/XXXXXXXXXX";

    [Header("Settings")]
    [SerializeField] private bool useTestAds = true; // Development'ta true
    [SerializeField] private float interstitialCooldown = 30f;
    [SerializeField] private int interstitialFrequency = 3; // Her 3 level'de bir

    // State
    private bool isInitialized = false;
    private bool isRewardedAdReady = false;
    private bool isInterstitialAdReady = false;
    private float lastInterstitialTime = -999f;
    private int levelCompletedCount = 0;

    // Callbacks
    private Action onRewardedAdSuccess;
    private Action onRewardedAdFailed;
    private Action onInterstitialClosed;

    // AdMob objects (gerçek implementasyonda kullanılacak)
    // private RewardedAd rewardedAd;
    // private InterstitialAd interstitialAd;

    // ============================================
    // INITIALIZATION
    // ============================================
    void Awake()
    {
        InitializeAds();
    }

    private void InitializeAds()
    {
        Debug.Log("[AdManager] Initializing...");

        // Test mode kontrolü
        if (useTestAds)
        {
            Debug.LogWarning("[AdManager] Using TEST ADS!");
        }

        // Platform-specific initialization
#if UNITY_ANDROID
        string appId = useTestAds ? "ca-app-pub-3940256099942544~3347511713" : androidAppId;
#elif UNITY_IOS
        string appId = useTestAds ? "ca-app-pub-3940256099942544~1458002511" : iosAppId;
#else
        string appId = "unused";
#endif

        // AdMob SDK initialization (gerçek kodda aktif olacak)
        // MobileAds.Initialize(initStatus => {
        //     isInitialized = true;
        //     Debug.Log("[AdManager] Initialized successfully");
        //     LoadRewardedAd();
        //     LoadInterstitialAd();
        // });

        // Simülasyon (test için)
        isInitialized = true;
        LoadRewardedAd();
        LoadInterstitialAd();
    }

    // ============================================
    // REWARDED AD
    // ============================================
    private void LoadRewardedAd()
    {
        if (!isInitialized) return;

        Debug.Log("[AdManager] Loading Rewarded Ad...");

#if UNITY_ANDROID
        string adUnitId = useTestAds ? "ca-app-pub-3940256099942544/5224354917" : androidRewardedId;
#elif UNITY_IOS
        string adUnitId = useTestAds ? "ca-app-pub-3940256099942544/1712485313" : iosRewardedId;
#else
        string adUnitId = "unused";
#endif

        // Gerçek implementasyon:
        // rewardedAd = new RewardedAd(adUnitId);
        // rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        // rewardedAd.LoadAd(CreateAdRequest());

        // Simülasyon
        Invoke(nameof(SimulateRewardedAdLoaded), 1f);
    }

    public void ShowRewardedAd(Action onSuccess, Action onFailed = null)
    {
        if (!isRewardedAdReady)
        {
            Debug.LogWarning("[AdManager] Rewarded ad not ready!");
            onFailed?.Invoke();
            return;
        }

        Debug.Log("[AdManager] Showing Rewarded Ad...");

        // Callbacks kaydet
        onRewardedAdSuccess = onSuccess;
        onRewardedAdFailed = onFailed;

        // Oyunu duraklat (reklam açılırken)
        GameService.Instance.PauseGame();

        // Gerçek implementasyon:
        // if (rewardedAd.IsLoaded()) {
        //     rewardedAd.Show();
        // }

        // Simülasyon (3 saniye reklam göster)
        Invoke(nameof(SimulateRewardedAdComplete), 3f);
    }

    public bool IsRewardedAdReady()
    {
        return isRewardedAdReady;
    }

    // ============================================
    // INTERSTITIAL AD
    // ============================================
    private void LoadInterstitialAd()
    {
        if (!isInitialized) return;

        Debug.Log("[AdManager] Loading Interstitial Ad...");

#if UNITY_ANDROID
        string adUnitId = useTestAds ? "ca-app-pub-3940256099942544/1033173712" : androidInterstitialId;
#elif UNITY_IOS
        string adUnitId = useTestAds ? "ca-app-pub-3940256099942544/4411468910" : iosInterstitialId;
#else
        string adUnitId = "unused";
#endif

        // Gerçek implementasyon:
        // interstitialAd = new InterstitialAd(adUnitId);
        // interstitialAd.OnAdLoaded += HandleInterstitialLoaded;
        // interstitialAd.OnAdClosed += HandleInterstitialClosed;
        // interstitialAd.LoadAd(CreateAdRequest());

        // Simülasyon
        Invoke(nameof(SimulateInterstitialLoaded), 1f);
    }

    public void ShowInterstitialAd(Action onClosed = null)
    {
        // Cooldown kontrolü
        if (Time.time - lastInterstitialTime < interstitialCooldown)
        {
            Debug.Log("[AdManager] Interstitial on cooldown");
            onClosed?.Invoke();
            return;
        }

        // Level frequency kontrolü
        levelCompletedCount++;
        if (levelCompletedCount % interstitialFrequency != 0)
        {
            Debug.Log($"[AdManager] Interstitial skipped (frequency: {levelCompletedCount}/{interstitialFrequency})");
            onClosed?.Invoke();
            return;
        }

        if (!isInterstitialAdReady)
        {
            Debug.LogWarning("[AdManager] Interstitial not ready!");
            onClosed?.Invoke();
            return;
        }

        Debug.Log("[AdManager] Showing Interstitial Ad...");

        onInterstitialClosed = onClosed;
        lastInterstitialTime = Time.time;

        // Oyunu duraklat
        Time.timeScale = 0f;

        // Gerçek implementasyon:
        // if (interstitialAd.IsLoaded()) {
        //     interstitialAd.Show();
        // }

        // Simülasyon (5 saniye reklam)
        Invoke(nameof(SimulateInterstitialClosed), 5f);
    }

    // ============================================
    // AD REQUEST (GDPR, COPPA)
    // ============================================
    // private AdRequest CreateAdRequest() {
    //     return new AdRequest.Builder()
    //         .AddTestDevice(AdRequest.TestDeviceSimulator)
    //         .TagForChildDirectedTreatment(false) // COPPA
    //         .AddExtra("is_designed_for_families", "false")
    //         .Build();
    // }

    // ============================================
    // CALLBACKS (Gerçek implementasyonda)
    // ============================================
    // private void HandleRewardedAdLoaded(object sender, EventArgs args) {
    //     isRewardedAdReady = true;
    //     Debug.Log("[AdManager] Rewarded ad loaded");
    // }

    // private void HandleUserEarnedReward(object sender, Reward args) {
    //     Debug.Log($"[AdManager] User earned reward: {args.Amount} {args.Type}");
    //     onRewardedAdSuccess?.Invoke();
    //     onRewardedAdSuccess = null;
    //     
    //     GameService.Instance.ResumeGame();
    //     LoadRewardedAd(); // Yeni reklam yükle
    // }

    // private void HandleRewardedAdClosed(object sender, EventArgs args) {
    //     Debug.Log("[AdManager] Rewarded ad closed");
    //     GameService.Instance.ResumeGame();
    // }

    // ============================================
    // SIMULATION (Test için)
    // ============================================
    private void SimulateRewardedAdLoaded()
    {
        isRewardedAdReady = true;
        Debug.Log("[AdManager] ✅ Rewarded ad ready (simulated)");
    }

    private void SimulateRewardedAdComplete()
    {
        Debug.Log("[AdManager] 🎬 Rewarded ad completed (simulated)");
        onRewardedAdSuccess?.Invoke();
        onRewardedAdSuccess = null;
        onRewardedAdFailed = null;

        GameService.Instance.ResumeGame();

        // Yeni reklam yükle
        isRewardedAdReady = false;
        LoadRewardedAd();
    }

    private void SimulateInterstitialLoaded()
    {
        isInterstitialAdReady = true;
        Debug.Log("[AdManager] ✅ Interstitial ad ready (simulated)");
    }

    private void SimulateInterstitialClosed()
    {
        Debug.Log("[AdManager] 🎬 Interstitial closed (simulated)");
        Time.timeScale = 1f;

        onInterstitialClosed?.Invoke();
        onInterstitialClosed = null;

        // Yeni reklam yükle
        isInterstitialAdReady = false;
        LoadInterstitialAd();
    }

    // ============================================
    // PUBLIC API
    // ============================================
    public void ResetLevelCounter()
    {
        levelCompletedCount = 0;
    }

    public bool CanShowInterstitial()
    {
        return isInterstitialAdReady &&
               (Time.time - lastInterstitialTime >= interstitialCooldown);
    }

    [ContextMenu("Force Show Rewarded")]
    public void DebugShowRewarded()
    {
        ShowRewardedAd(
            onSuccess: () => Debug.Log("✅ Reward granted!"),
            onFailed: () => Debug.Log("❌ Ad failed!")
        );
    }

    [ContextMenu("Force Show Interstitial")]
    public void DebugShowInterstitial()
    {
        levelCompletedCount = interstitialFrequency; // Frequency'yi bypass et
        ShowInterstitialAd(() => Debug.Log("✅ Interstitial closed"));
    }
}

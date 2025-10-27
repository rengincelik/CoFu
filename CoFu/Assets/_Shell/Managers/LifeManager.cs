// ----------------------
// Life Manager (oyun mantığı)
// ----------------------
using System;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] LifeChangedEventSO _lifeChangedEventSO;
    [SerializeField] Life _life;

    private long previousRegenerationTime;
    private float remainingRegenerationPeriod;

    void Awake()
    {
        LoadLife();
        UpdateLifeOnStart();
    }

    void Update()
    {
        RegenerateLife();
    }

    void LoadLife()
    {
        _life.Amount = LifeSaveService.LoadLife(_life);
        previousRegenerationTime = LifeSaveService.LoadLastLifeTime(_life);
    }

    void UpdateLifeOnStart()
    {
        DateTime lastTime = DateTime.FromBinary(previousRegenerationTime);
        double elapsedSeconds = (DateTime.UtcNow - lastTime).TotalSeconds;

        int livesToAdd = Mathf.FloorToInt((float)(elapsedSeconds / Life.RegenerationPeriod));
        _life.Amount = Mathf.Min(_life.Amount + livesToAdd, Life.MaxLife);

        remainingRegenerationPeriod = Life.RegenerationPeriod - (float)(elapsedSeconds % Life.RegenerationPeriod);
    }

    void RegenerateLife()
    {
        if (_life.Amount >= Life.MaxLife) return;

        remainingRegenerationPeriod -= Time.deltaTime;

        if (remainingRegenerationPeriod <= 0f)
        {
            AddLife();
            previousRegenerationTime = DateTime.UtcNow.ToBinary();
            LifeSaveService.SaveLastLifeTime(_life, previousRegenerationTime);
            remainingRegenerationPeriod = Life.RegenerationPeriod;
        }
    }

    public bool TrySpendLife()
    {
        if (_life.Amount <= 0)
        {
            Debug.LogWarning("Yetersiz life");
            return false;
        }

        _life.Amount--;
        _lifeChangedEventSO?.Raise(_life);
        LifeSaveService.SaveLife(_life);
        return true;
    }

    public void AddLife()
    {
        if (_life.Amount >= Life.MaxLife) return;

        _life.Amount++;
        _lifeChangedEventSO?.Raise(_life);
        LifeSaveService.SaveLife(_life);
    }
}



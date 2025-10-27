using System;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;


public enum MusicType { GamePlay, NonGamePlay ,ButtonClick, Victory, Fail, CoinCollect}
public enum AdType { Rewarded, Interstitial, Banner }

// ========== INTERFACE ==========
public interface IUseCase { void Execute(); }

// ========== COMMANDS ==========
public class GameOpenUseCase : IUseCase
{
    public void Execute()
    {
        throw new NotImplementedException();
    }
}



public class LevelStartCommand : IUseCase
{
    GameStateChangedEventSO _stateEvent;

    public LevelStartCommand(
        GameStateChangedEventSO stateEvent)
    {
        _stateEvent = stateEvent;
    }

    public void Execute()
    {

    }
}

public class LevelCompletedCommand : IUseCase
{
    GameStateChangedEventSO _stateEvent;
    
    public void Execute()
    {
        
    }
}


public class UseJokerCommand : IUseCase
{
    JokerType _jokerType;
    JokerEventSO _jokerEvent;
    
    public UseJokerCommand(JokerType jokerType, JokerEventSO jokerEvent)
    {
        _jokerType = jokerType;
        _jokerEvent = jokerEvent;
    }
    
    public void Execute()
    {
        _jokerEvent?.Raise(); // JokerManager dinler, coin kontrol + kullan
    }
}
public class ShowAdCommand : IUseCase
{
    AdType _adType;
    AdEventSO _adEvent;

    public ShowAdCommand(AdType adType, AdEventSO adEvent)
    {
        _adType = adType;
        _adEvent = adEvent;
    }

    public void Execute()
    {
        _adEvent.Raise(_adType);
    }
}

public class GameOpenCommand : IUseCase
{
    //bunu belki otomatik yaparım commande gerek olmaz ama şimdilik dursun.

    public void Execute()
    {
        //loading seq oynatılcak,
        //sonra ekran değişcek menü opening seq oynatılcak
        //opening ve menu musikleri çalabilir aynı da olabilir.
        //oyuncu play butonun atıklanana kadar beklenecek
        throw new NotImplementedException();
    }
}

public class PlayButtonClickedCommand : IUseCase
{
    PlaySequenceEventSO _sequenceEvent;

    PlayButtonClickedCommand(PlaySequenceEventSO sequenceEventSO)
    {
        _sequenceEvent = sequenceEventSO;
    }
    public void Execute()
    {
        
    }

}

public class NextButtonClickedCommand : IUseCase
{
    PlaySequenceEventSO _sequenceEvent;

    NextButtonClickedCommand(PlaySequenceEventSO sequenceEventSO)
    {
        _sequenceEvent = sequenceEventSO;
    }
    public void Execute()
    {
        //kazanılan coinler eklencek
        //win müsik çalaıncak
        //bir can eklencek
        
    }

}


public class RetryButtonClickedCommand : IUseCase
{
    PlaySequenceEventSO _sequenceEvent;

    RetryButtonClickedCommand(PlaySequenceEventSO sequenceEventSO)
    {
        _sequenceEvent = sequenceEventSO;
    }
    public void Execute()
    {
        //start için can gitcek
        //fain musik çalıncak
        //oyun tekrardan başlatılca
        //fail popup kapancak 
        //play update olacak
    }

}


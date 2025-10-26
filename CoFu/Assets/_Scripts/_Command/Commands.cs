using System;
using DG.Tweening;
using NUnit.Framework;
using UnityEngine;


public enum MusicType { GamePlay, NonGamePlay ,ButtonClick, Victory, Fail, CoinCollect}
public enum AdType { Rewarded, Interstitial, Banner }

// ========== INTERFACE ==========
public interface ICommand { void Execute(); }

// ========== COMMANDS ==========
public class LevelStartCommand : ICommand
{
    GameStateChangedEventSO _stateEvent;
    AudioEventSO _audioEvent;
    
    public LevelStartCommand(
        GameStateChangedEventSO stateEvent,
        AudioEventSO audioEvent)
    {
        _stateEvent = stateEvent;
        _audioEvent = audioEvent;
    }
    
    public void Execute()
    {

    }
}

public class LevelCompletedCommand : ICommand
{
    GameStateChangedEventSO _stateEvent;
    AudioEventSO _audioEvent;
    
    public void Execute()
    {
        
    }
}

// public class GoToScreenCommand : ICommand
// {
//     ScreenViewType _type;

//     public void Execute()
//     {
//         throw new NotImplementedException();
//     }
//     // ScreenEventSO _screenEvent; // ✅ Event kullan

//     // public GoToScreenCommand(ScreenViewType type, ScreenEventSO screenEvent)
//     // {
//     //     _type = type;
//     //     _screenEvent = screenEvent;
//     // }

//     // public void Execute()
//     // {
//     //     _screenEvent.Raise(_type); // ScreenManager dinler
//     // }
// }

public class UseJokerCommand : ICommand
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
        _jokerEvent.Raise(_jokerType); // JokerManager dinler, coin kontrol + kullan
    }
}
public class ShowAdCommand : ICommand
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

public class GameOpenCommand : ICommand
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

public class PlayButtonClickedCommand : ICommand
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

public class NextButtonClickedCommand : ICommand
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


public class RetryButtonClickedCommand : ICommand
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


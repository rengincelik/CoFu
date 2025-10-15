// Tetikleme: HUD'daki Joker button
// Parametre: JokerType type

// Akış:
// 1. CanExecute():
//    ├── JokerService.CanUseJoker(type)
//    └── GameState == Playing

// 2. JokerService.UseJoker(type)

// 3. Joker tipine göre animasyon:
//    ├── TimeFreeze → Ekran blur + slow-mo effect
//    ├── Shuffle → Tüm dot'lar fade out/in
//    └── Hint → Target dot glow efekti


using UnityEngine;

public class UseJokerCommand : ICommand
{
    public bool CanExecute()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }
}

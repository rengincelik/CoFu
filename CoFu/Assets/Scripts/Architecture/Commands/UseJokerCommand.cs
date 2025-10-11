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

public class UseJokerCommand : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

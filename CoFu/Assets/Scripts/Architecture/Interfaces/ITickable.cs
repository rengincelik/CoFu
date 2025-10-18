
// ============================================
// INTERFACE - Tüm managed objeler bunu implemente eder
// ============================================
public interface ITickable
{
    void Tick(float deltaTime);
    bool IsActive { get; }
    int Priority { get; } // 0 = ilk çalışır, 100 = son çalışır
}

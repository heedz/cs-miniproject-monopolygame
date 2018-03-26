using System;

namespace MonopolyGame
{
    public interface IActionCard
    {
        string Text { get; }
        string Image { get; }
        Action<MonopolyPlayer> Action { get; }
    }
    
}

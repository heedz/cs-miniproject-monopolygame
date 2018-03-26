using System;

namespace MonopolyGame
{
    public class ChanceCard : IActionCard
    {
        private string _text;
        private string _image;
        private Action<MonopolyPlayer> _action;

        public Action<MonopolyPlayer> Action { get { return _action; } }
        public string Text { get { return _text; } }
        public string Image { get { return _image; } }

        public ChanceCard(string text, string image, Action<MonopolyPlayer> action)
        {
            _text = text;
            _image = image;
            _action = action;
        }
    }
    
}

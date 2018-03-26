namespace MonopolyGame
{
    public class Player
    {
        private string _username;
        private int _id;

        public string Username
        {
            get
            {
                return this._username;
            }
        }

        public int Id
        {
            get
            {
                return this._id;
            }
        }

        public Player(int id, string username)
        {
            _username = username;
            _id = id;
        }
    }
}

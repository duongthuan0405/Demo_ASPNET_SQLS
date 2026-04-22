namespace webapi.Entities
{
    public class User
    {
        private Guid _id = Guid.Empty;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _fullName = string.Empty;
        private List<Message> _messages = new List<Message>();

        public Guid Id
        {
            get => _id;
            set => _id = value;
        }

        public string Username
        {
            get => _username;
            set => _username = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string FullName
        {
            get => _fullName;
            set => _fullName = value;
        }

        public List<Message> Messages
        {
            get => _messages;
            set => _messages = value;
        }
    }
}

namespace webapi.Entities
{
    public class Message
    {
        private Guid _id = Guid.Empty;
        private Guid _userId = Guid.Empty;
        private string _content = string.Empty;
        private DateTime _createdAt = DateTime.Now;
        private DateTime? _updatedAt = null;

        private User _sender = null!;

        public Guid Id
        {
            get => _id;
            set => _id = value;
        }

        public Guid UserId
        {
            get => _userId;
            set => _userId = value;
        }

        public string Content
        {
            get => _content;
            set => _content = value;
        }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set => _createdAt = value;
        }

        public DateTime? UpdatedAt
        {
            get => _updatedAt;
            set => _updatedAt = value;
        }

        public User Sender
        {
            get => _sender;
            set => _sender = value;
        }
    }
}

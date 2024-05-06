using System;

namespace LiveChatMultiplexer
{
    public class Message
    {
        public Guid guid = Guid.NewGuid();
        private String username;
        private String contents;

        public Message(String username, String contents)
        {
            this.username = username;
            this.contents = contents;
        }
    }
}

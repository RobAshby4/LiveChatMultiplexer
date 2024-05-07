using System;

namespace LiveChatMultiplexer
{
    public class Message
    {
        public Guid guid = Guid.NewGuid();
        public String username;
        public String contents;

        public Message(String username, String contents)
        {
            this.username = username;
            this.contents = contents;
        }
    }
}

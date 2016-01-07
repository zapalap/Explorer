using Explorer.Entities;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Entities
{
    public class GameLog : BaseEntity
    {
        public GameLog()
        {
            MessageQueue = new Queue<LogMessage>();
            Buffer = new List<LogMessage>();
        }

        private Queue<LogMessage> MessageQueue;
        private List<LogMessage> Buffer;

        public List<LogMessage> MessageBuffer { get { return Buffer; } }

        public bool HasNew()
        {
            return MessageQueue.Count > 0;
        }

        public void AddMessage(string message)
        {
            MessageQueue.Enqueue(new LogMessage() { Message = message, Color = Color4.White});
        }

        public void AddMessage(string message, Color4 color)
        {
            MessageQueue.Enqueue(new LogMessage() { Message = message, Color = color});
        }

        public List<LogMessage> GetMessages()
        {
            while (MessageQueue.Count != 0)
            {
                Buffer.Add(MessageQueue.Dequeue());
            }

            return Buffer;
        }
    }
}

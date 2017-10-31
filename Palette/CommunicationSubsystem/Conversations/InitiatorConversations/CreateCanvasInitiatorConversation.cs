using System;
using System.Collections.Generic;
using System.Text;
using Messages;

namespace CommunicationSubsystem.Conversations.InitiatorConversations
{
    public class CreateCanvasInitiatorConversation : InitiatorConversation
    {
        protected override void ProcessFailure()
        {
            Console.WriteLine("Error encountered");
        }

        protected override void ValidateConversationState()
        {
            // TODO: Write this method.
            Console.WriteLine("Validating conversation state.");
        }

        protected override void CheckProcessState()
        {
            Console.WriteLine("Checking process state.");
        }

        protected override void CreateRequest()
        {
            var message = new CreateCanvasMessage()
            {
                ConversationId = new Tuple<short, short>(ConversationId.Item1, ConversationId.Item2),
                MessageNumber = new Tuple<short, short>(ConversationId.Item1, ConversationId.Item2)
            };

            var envelope = new Envelope()
            {
                RemoteEP = RemoteEndPoint,
                Message = message
            };

            _communicator.Send(envelope);
        }

        protected override void ProcessReply()
        {
            throw new NotImplementedException();
        }
    }
}

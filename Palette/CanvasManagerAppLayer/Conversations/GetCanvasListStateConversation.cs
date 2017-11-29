using CommunicationSubsystem.Conversations;
using Messages;
using System;
using System.Collections.Generic;
using SharedAppLayer.Entitities;

namespace CanvasManagerAppLayer.Conversations
{
    public class GetCanvasListStateConversation : StateSubjetConversation, INeedsAuthorization
    {
        private IEnumerable<Canvas> _canvasList;

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void StartStateUpdates(Message message)
        {
            var authConvo = new AuthInitiatorConversation();
        }

        protected override void StartStateUpdates()
        {
            throw new NotImplementedException();
        }

        public void ReceiveAuthToken(int authToken)
        {
            throw new NotImplementedException();
        }
    }

    public interface INeedsAuthorization
    {
        void ReceiveAuthToken(int authToken);
    }
}

﻿using CommunicationSubsystem.Conversations;
using Messages;
using System;

namespace CanvasManagerAppLayer.Conversations
{
    public class DeleteCanvasStateConversation : StateSubjetConversation
    {
        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void StartStateUpdates(Message message)
        {
            throw new NotImplementedException();
        }
    }
}

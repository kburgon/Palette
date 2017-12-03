﻿using System;
using CanvasStorageManager.DataPersistence;
using CommunicationSubsystem.Conversations;
using Messages;

namespace CanvasStorageManager.Conversations
{
    internal class DeleteCanvasConversation : ResponderConversation
    {
        public CanvasRepository _canvasRepo;

        public DeleteCanvasConversation()
        {
            var _dataStore = new FileDataStore();
            _canvasRepo = new CanvasRepository(_dataStore);
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessReceivedMessage(Message message)
        {
            var deleteMessage = message as DeleteCanvasMessage;
            if (deleteMessage == null)
            {
                HandleWrongMessage();
                return;
            }
            _canvasRepo.Delete(deleteMessage.CanvasId);
        }

        private static void HandleWrongMessage() { }

        protected override Message CreateReply()
        {
            throw new NotImplementedException();
        }
    }
}

﻿using System;
using CanvasStorageManager.DataPersistence;
using CommunicationSubsystem.Conversations.ResponderConversations;
using Messages;
using SharedAppLayer.Entitities;

namespace CanvasStorageManager.Conversations
{
    internal class CreateCanvasConvsersation : ResponderConversation
    {
        private readonly CanvasRepository _canvasRepo;
        private Canvas _createdCanvas;

        public CreateCanvasConvsersation(CanvasRepository canvasRepo)
        {
            _canvasRepo = canvasRepo;
        }

        protected override void ProcessFailure()
        {
            throw new NotImplementedException();
        }

        protected override void ProcessReceivedMessage(Message message)
        {
            if (!(message is CreateCanvasMessage)) HandleWrongMessage();
            _createdCanvas = _canvasRepo.CreateNew();
        }

        private static void HandleWrongMessage() { }

        protected override Message CreateReply()
        {
            return new CanvasMessage
            {
                CanvasId = _createdCanvas.CanvasId
            };
        }
    }
}

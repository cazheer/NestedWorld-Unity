using MessagePack.Client.Answers;
using MessagePack.Exception;
using MessagePack.Serveur;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MessagePack.Client
{
    public class ClientMessageList
    {
        private Dictionary<string, RequestBase> map;

        private Dictionary<string, AnswerBase> answerDictionary;

        private bool offline;

        public ServerListener serverListener;
        public ClientMessageList(ServerListener l, bool Offline = false)
        {
            serverListener = l;
            offline = Offline;
            map = new Dictionary<string, RequestBase>();
            answerDictionary = new Dictionary<string, AnswerBase>();
        }

        public void Add(RequestBase request)
        {
            map[request.type] = request;
        }

        public RequestBase Get(string type)
        {
            RequestBase value;
            if (map.TryGetValue(type, out value))
                return value;
            throw new NoTypeFoundException(type);
        }

        public void SendRequest(RequestBase request)
        {
            if (request.AnswerType != null)
                answerDictionary[request.id] = Activator.CreateInstance(request.AnswerType) as AnswerBase;
            if (!offline)
                serverListener.Send(request.GetStream());
        }

        public void ReceiveRequest(object value)
        {
            ResultRequest rR = value as ResultRequest;

            AnswerBase answer = null;

            if (!answerDictionary.TryGetValue(rR.id, out answer))
                return;
            answer.SetValue(rR);
        }
    }
}

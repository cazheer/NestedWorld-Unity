using System;
using System.Collections.Generic;
using nestedWorld.models;

namespace nestedWorld.network.http.models.user
{
    [Serializable]
    public class UserFriendsResponse
    {
        public List<Friend> friends;
    }
}

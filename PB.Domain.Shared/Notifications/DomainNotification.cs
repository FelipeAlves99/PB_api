﻿using Newtonsoft.Json;

namespace PB.Domain.Shared.Notifications
{
    public class DomainNotification : IDomainNotification
    {
        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
        }

        [JsonIgnore]
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
    }
}

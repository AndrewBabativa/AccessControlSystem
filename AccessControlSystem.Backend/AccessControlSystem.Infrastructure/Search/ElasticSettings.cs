﻿namespace AccessControlSystem.Infrastructure.Search
{
    public class ElasticSettings
    {
        public string Uri { get; set; } = string.Empty;
        public string DefaultIndex { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }


}

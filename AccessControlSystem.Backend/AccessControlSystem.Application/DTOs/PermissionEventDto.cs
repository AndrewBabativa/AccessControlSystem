using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlSystem.Application.DTOs
{
    public class PermissionEventDto
    {
        public Guid Id { get; set; }
        public string NameOperation { get; set; } = default!;
        public DateTime Timestamp { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS.UserManagement.Application.ExternalServices.Dtos
{
    public interface IEventDto
    {
        DateTime EventTimestamp { get; set; }
        string EventId { get; set; }
        string EventType { get; }
    }
}

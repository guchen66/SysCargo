using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Shared.Models
{
    public class ProcessDto : BaseDto
    {

        public int WorkPlaceId { get; set; }

        public string WorkPlaceName { get; set; }

        public string UserName { get; set; }

        public string ProcessName { get; set; }

        public DateTime CreateTime { get; set; }

        public bool IsWorkInProgress { get; set; }


    }
}

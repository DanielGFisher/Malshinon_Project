using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon_Project.Models
{
    public class Reports
    {
        public int ReporterID { get; set; }
        public int TargetID { get; set; }
        public string Documentation { get; set; }

        public Reports(int reporterId, int targetId, string documentation)
        {
            ReporterID = reporterId;
            TargetID = targetId;
            Documentation = documentation;
        }
    }
}

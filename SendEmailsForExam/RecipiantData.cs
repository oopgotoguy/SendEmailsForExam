using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailsForExam
{
   public class RecipiantData
    {
        public string Name { get; set; }
        public string To { get; set; }
        public List<string> Attachments { get; set; }
    }
}

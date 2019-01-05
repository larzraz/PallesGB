using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enitities.Model
{
    public class Gift
    {
        [Key]
        public long GiftNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool BoyGift { get; set; }
        public bool GirlGift { get; set; }
    }
}

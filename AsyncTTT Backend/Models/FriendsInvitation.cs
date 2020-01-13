using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncTTT_Backend.Models
{
    public class FirendsInvitation
    {
        public int Id { get; set; }
        public int Sender { get; set; }
        public int Reciever { get; set; }
    }
}

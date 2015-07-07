using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRegFB
{
    class Acc
    {
        private String phone;

        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; }
        }
        private bool used = false;

        public bool Used
        {
            get { return used; }
            set { used = value; }
        }

        private bool done = false;

        public bool Done
        {
            get { return done; }
            set { done = value; }
        }
    }
}

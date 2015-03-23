using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            SeahawksClient proxy = new SeahawksClient("Seahawks");
            PermissiveCertificatePolicy.Enact("CN=localhost");

            try
            {
                string retval = proxy.BeatTheBroncos(35);
                Console.WriteLine(retval);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

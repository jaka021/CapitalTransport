using Newtonsoft.Json;
using System.ComponentModel;

namespace CapitalTransport.Model
{
    public class UserInfo
    {

        public string Name { get; set; }
         
        public string Login { get; set; }
        public string Company { get; set; }
        public int Followers { get; set; }

        [DisplayName("Public Repository")]
        public int Public_Repos{ get; set; }
        public double AverageFollowersPerRepository { get; private set; }

        public void CalculateAverageFollowersPerRepository()
        {
            if (Public_Repos > 0)
                AverageFollowersPerRepository = (double)Followers / Public_Repos;
            else
                AverageFollowersPerRepository = 0;
        }
    }
}

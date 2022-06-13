using System;
using System.Collections.Generic;
using System.Text;

namespace PostCodeAPI.Model
{
    public class PostCodeLookUp
    {
        public PostCodeLookUpDetails result { get; set; }

    }
    public class PostCodeLookUpDetails
    {
        private double _latitude;
        public string country { get; set; }
        public string region { get; set; }
        public double latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;

                if (_latitude < 52.229466)
                {
                    area = "South";
                }
                else if (_latitude >= 52.229466 && _latitude < 53.27169)
                {
                    area = "Midlands";
                }
                else if (_latitude >= 53.27169)
                {
                    area = "North";
                }
            }
        }
        public string area { get; set; }
        public PostCodeLookUpDetailsCode codes { get; set; }
    }

    public class PostCodeLookUpDetailsCode
    {
        public string admin_district { get; set; }
        public string parliamentary_constituency { get; set; }
    }
}

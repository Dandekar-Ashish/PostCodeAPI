namespace PostCodeAPI.DataTransferModel
{
    public class PostCodeLookUpDataTransferModel
    {
        public string country { get; set; }
        public string region { get; set; }
        public string area { get; set; }
        public string admin_district { get; set; }
        public string parliamentary_constituency { get; set; }
    }
}

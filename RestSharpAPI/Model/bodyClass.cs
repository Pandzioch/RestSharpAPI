namespace RestSharpAPI
{
    internal class bodyClass
    {
        public int id { get; set; }
        public string customerName { get; set; }
    }

    internal class registerClass
    {
        public string clientName { get; set; }
        public string clientEmail { get; set; }
    }
    internal class listOfBooks
    {
        public int id { get; set; }

        public bool available { get; set; }
    }
}
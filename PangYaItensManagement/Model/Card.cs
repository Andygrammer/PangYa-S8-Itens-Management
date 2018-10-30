namespace PangYaItensManagement.Model
{
    public class Card
    {
        private int cardPackID;
        private string type;

        public string Name { get; set; }
        public int ID { get; set; }
        public byte[] ImageBytes { get; set; }
        public byte[] ImageBigBytes { get; set; }
        public string Type
        {
            get { return this.type; }
            set
            {
                switch (value)
                {
                    case "0":
                        this.type = "Normal";
                        break;
                    case "1":
                        this.type = "Rare";
                        break;
                    case "2":
                        this.type = "Super Rare";
                        break;
                    case "3":
                        this.type = "Secret";
                        break;
                    default:
                        this.type = value;
                        break;
                }
            }
        }

        public int CardPackID
        {
            get { return this.cardPackID; }
            set
            {
                switch (value)
                {
                    case 2092957696:
                        this.cardPackID = 1;
                        break;
                    case 2092957700:
                        this.cardPackID = 2;
                        break;
                    case 2092957701:
                        this.cardPackID = 3;
                        break;
                    case 2092957703:
                        this.cardPackID = 4;
                        break;
                    default:
                        this.cardPackID = value;
                        break;
                }
            }
        }
    }
}

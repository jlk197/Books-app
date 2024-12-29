namespace DaoMock
{
    public class Producer : Interfaces.IProducer
    {
        public int Id { get; set; }
        public string Name { get ; set ; }
        public string Country { get; set; }
        public int EstablishmentYear { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }
    }
}

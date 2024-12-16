namespace DaoMock
{
    public class Producent : Interfaces.IProducent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }
    }
}

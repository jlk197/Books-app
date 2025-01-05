using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAOEF
{
    public class DAOSqlite : DbContext, Interfaces.IDaoMock
    {
        public DbSet<BO.Producer> Producers { get; set; }
        public DbSet<BO.Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlite(@"Filename=..\..\..\..\DAOEF\carapp4.db");
            //optionsBuilder.UseSqlite(@"Filename=C:\Users\jklec\Documents\Studia\wizualne\Books\DAOEF\carapp4.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BO.Book>()
                .HasOne(c => c.Producer)
                .WithMany(p => p.Books)
                .HasForeignKey(c => c.ProducerId).IsRequired();
        }

        public void AddBook(IBook book)
        {
            BO.Book c = book as BO.Book;
            Books.Add(c);
        }

        public void AddProducer(IProducer producent)
        {
            BO.Producer p = producent as BO.Producer;
            Producers.Add(p);
        }

        public IBook CreateNewBook()
        {
            return new BO.Book();
        }

        public IProducer CreateNewProducent()
        {
            return new BO.Producer();
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            var books = Books.Include("Producer").ToList();
            return Books;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return Producers;
        }

        public void RemoveBook(IBook book)
        {
            Books.Remove(book as BO.Book);
        }

        public void RemoveProducer(IProducer producent)
        {
            Producers.Remove(producent as BO.Producer);
        }

        void IDaoMock.SaveChanges()
        {
            this.SaveChanges();
        }
    }
}

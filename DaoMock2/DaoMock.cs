using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaoMock
{
    public class DaoMock1 : Interfaces.IDaoMock
    {
        List<IBook> carList;
        List<IProducent> prodcentList;
        public DaoMock1()
        {
            prodcentList = new List<IProducent>(){
            new Producent() {Id = 1, Name = "Audi"},
            new Producent() {Id = 2, Name = "Opel"},
            };

            carList = new List<IBook>(){
            new Car() {Id = 1, Model = "A1", Producent = prodcentList[0], Transmission=TransmissionType.Automatic, ProdYear = 2004},
            new Car() {Id = 2, Model = "A2", Producent = prodcentList[0], Transmission=TransmissionType.Automatic},
            new Car() {Id = 3, Model = "Q1", Producent = prodcentList[0], Transmission=TransmissionType.Manual},
            new Car() {Id = 4, Model = "Astra", Producent = prodcentList[1], Transmission=TransmissionType.Manual},

            };
        }

        public void AddCar(IBook car)
        {
            throw new NotImplementedException();
        }

        public void AddProducer(IProducent producent)
        {
            throw new NotImplementedException();
        }

        public IBook CreateNewCar()
        {
            throw new NotImplementedException();
        }

        public IProducent CreateNewProducent()
        {
            throw new NotImplementedException();
        }

        public List<IBook> GetAllCars()
        {
            throw new NotImplementedException();
        }

        public List<IProducent> GetAllProducents()
        {
            throw new NotImplementedException();
        }

        public List<IBook> GetCars()
        {
            return carList;
        }

        public List<IProducent> GetProducents()
        {
            return prodcentList;
        }

        public void RemoveCar(IBook car)
        {
            throw new NotImplementedException();
        }

        public void RemoveProducer(IProducent producent)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}

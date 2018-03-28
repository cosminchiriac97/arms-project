using System;
using Data.Context;
using Data.Model;

namespace Bussiness.HouseRepository
{
    public class HouseRepository : IHouseRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public HouseRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(House house)
        {
            house.Id = Guid.NewGuid();
            _databaseContext.Houses.Add(house);
            _databaseContext.SaveChanges();
        }
    }
}

using SerjTm.Sample.Common.Model;
using SerjTm.Sample.Common.Services;
using SerjTm.Sample.TuiProvider.Storages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerjTm.Sample.TuiProvider.Services
{
    public class MemoryDictService : IDictService
    {
        public MemoryDictService(MemoryDictStorage storage)
        {
            this.Storage = storage;
        }
        public readonly MemoryDictStorage Storage;

        public IEnumerable<City> StartCities()
        {
            //TODO разделить в Storage-е города вылета и города тура
            return Storage.Cities;
        }

        public IEnumerable<Country> Countries()
        {
            return Storage.Countries;
        }

        public IEnumerable<City> Cities()
        {
            return Storage.Cities;
        }

        public IEnumerable<Hotel> Hotels()
        {
            return Storage.Hotels;
        }

        public Hotel FindHotel(Guid id)
        {
            return Storage.Hotels.By(id: id);
        }
    }

}

using SerjTm.Sample.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerjTm.Sample.Common.Services
{

    public interface IDictService
    {
        IEnumerable<City> StartCities();
        IEnumerable<Country> Countries();

        /// <summary>
        /// Все возможные города, как города вылета, так и города туров
        /// </summary>
        /// <returns></returns>
        IEnumerable<Country> Cities();
        /// <summary>
        /// Поиск отеля по Id
        /// </summary>
        /// <param name="id">id отеля</param>
        /// <returns>null - если не найден</returns>
        Hotel FindHotel(Guid id);
    }
}

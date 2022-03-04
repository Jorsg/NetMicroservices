using Catal.APIs.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catal.APIs.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Products> Product { get; }
    }
}

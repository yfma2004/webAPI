using IRepository;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class WarehouseRepository : BaseRepository<WarehouseInfo>, IWarehouseRepository
    {
    }
}

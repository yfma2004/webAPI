﻿using IRepository;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    
     public class UserRepository : BaseRepository<UserInfo>, IUserRepository
    {
    }
}

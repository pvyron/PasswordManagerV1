﻿using PMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMDesktopUI.Library.API
{
    public interface IPasswordsEndPoint
    {
        Task<List<PasswordModel>> GetAll();
    }
}
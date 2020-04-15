using System;
using System.Collections.Generic;
using VSphere.Models;

namespace VSphere.Application.Interface
{
    public interface IVMApplication
    {
        List<VMViewModel> GetAll();

        List<VMViewModel> GetAllByDate(DateTime from, DateTime to);
    }
}

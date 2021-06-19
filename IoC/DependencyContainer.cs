using DataAccess.Design_Pattern.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {


            service.AddScoped<IUnitOfWork, UnitOfWork>();


        }
    }
}

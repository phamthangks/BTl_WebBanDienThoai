using BTLW_BDT.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTLW_BDT.Controllers
{
    
    public class BrandController : Controller
    {
        private readonly BtlLtwQlbdtContext _btlLtwQlbdtContext;
        public BrandController(BtlLtwQlbdtContext context)
        {
            _btlLtwQlbdtContext = context;
        }
    }
}

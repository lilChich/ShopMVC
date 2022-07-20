using ShopMVC.BLL.DTO;
using ShopMVC.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.Security.JWT
{
    public interface IJwtGenerator
    {
        string CreateToken(UserDTO user);
    }
}

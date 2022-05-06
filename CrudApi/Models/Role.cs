using System.Collections.Generic;
using CrudApi.GenericControllerAttribute;

namespace CrudApi.Models
{
    [GeneratedController("api/entity/Role")]
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}
using System.Collections.Generic;
using CrudApi.GenericControllerAttribute;

namespace CrudApi.Models
{
    [GeneratedController("api/entity/User")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Role Role { get; set; }
        public List<Order> Order { get; set; }

        public User()
        {
            Order = new List<Order>();
        }
    }
}
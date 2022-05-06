using CrudApi.GenericControllerAttribute;

namespace CrudApi.Models
{
    [GeneratedController("api/entity/Order")]
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public User User { get; set; }
    }
}
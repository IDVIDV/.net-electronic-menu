using ElectronicMenu.DataAccess.Entities;

namespace ElectronicMenu.BL.Users.Entities
{
    public class UserModel
    {
        public Guid id { get; set; }
        public string Login { get; set; }
        public string PhoneNumber { get; set; }

        //Надо заменить на OrderModel, которого пока нет
        public List<OrderEntity> Orders { get; set; }
    }
}

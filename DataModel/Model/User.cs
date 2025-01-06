using DataModel.BaseEntity;
using DataModel.Common.Base;

namespace DataModel.Model
{
    public class User: BaseEntity<Guid>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public Currency Currency { get; set; }

       

    }
}

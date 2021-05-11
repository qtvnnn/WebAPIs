using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class CustomerGroup : BaseEntity
    {
        /// <summary>
        /// Khóa chính của bảng dữ liệu nhóm khách hàng
        /// </summary>
        [PrimaryKey]
        public Guid CustomerGroupId { get; set; }
        /// <summary>
        /// Tên nhóm khách hàng
        /// </summary>
        [Duplicate]
        [Required]
        [DisplayName("Mã khách hàng")]
        public string CustomerGroupName { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        /// 
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
    }
}

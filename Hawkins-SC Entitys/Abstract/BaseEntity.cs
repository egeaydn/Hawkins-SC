using System;
using System.Collections.Generic;
using System.Text;

namespace Hawkins_SC.Abstract
{
	public abstract class BaseEntity<TKey>
	{
		public TKey Id { get; set; }  
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}

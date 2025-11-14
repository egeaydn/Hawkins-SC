using System;
using System.Collections.Generic;
using System.Text;

namespace Hawkins_SC.Abstract
{
	internal interface IAuditableEntity
	{
		string? CreatedBy { get; set; }
		string? ModifiedBy { get; set; }
		string? DeletedBy { get; set; }
	}
}

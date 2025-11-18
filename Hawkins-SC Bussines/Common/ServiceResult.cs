using System;
using System.Collections.Generic;
using System.Text;

namespace Hawkins_SC_Bussines.Common
{
	public class ServiceResult<T>
	{
		public bool Success { get; set; }
		public T? Data { get; set; }
		public string? Error { get; set; }
		public IDictionary<string, string>? Errors { get; set; }

		public static ServiceResult<T> Ok(T data) => new ServiceResult<T> { Success = true, Data = data };
		public static ServiceResult<T> Fail(string error, IDictionary<string, string>? errors = null)
			=> new ServiceResult<T> { Success = false, Error = error, Errors = errors };
	}
}

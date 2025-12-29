using System;
using System.Collections.Generic;
using System.Text;

namespace QuebecAdventures.Domain.Exceptions
{
	public class DomainException : Exception
	{
		public DomainException(string message) : base(message) { }
	}

	public class NotFoundException : DomainException
	{
		public NotFoundException(string resource, Guid id)
			: base($"{resource} with ID {id} not found") { }
	}

	public class ValidationException : DomainException
	{
		public ValidationException(string message) : base(message) { }
	}
}

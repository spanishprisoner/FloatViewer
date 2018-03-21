﻿using FloatViewer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FloatViewer.Services
{
	public interface IFloatService
	{
		IList<Person> Persons { get; set; }
		IList<Project> Projects { get; set; }

		Task<IList<Project>> GetProjectsAsync(string login, string password);
	}
}
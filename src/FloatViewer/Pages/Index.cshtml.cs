﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FloatViewer.Models;
using FloatViewer.Services;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System;
using System.Diagnostics;

namespace FloatViewer.Pages
{
	public class IndexModel : PageModel
	{
		private readonly IFloatService _floatService;

		public IList<Project> Projects { get; set; }
		public bool HasProjects => Projects?.Count > 0;

		[Required]
		[EmailAddress]
		[BindProperty]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[BindProperty]
		public string Password { get; set; }

		public IndexModel(IFloatService floatService)
		{
			_floatService = floatService;
		}

		public async Task OnGetAsync()
		{
			if (await _floatService.IsLoginRequiredAsync(HttpContext.Session))
			{
				Projects = new List<Project>();
			}
			else
			{
				Projects = await _floatService.GetProjectsAsync(HttpContext.Session);
			}
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> OnPostAsync()
		{
			try
			{
				await _floatService.LogInAsync(HttpContext.Session, Email, Password);
			}
			catch (ArgumentException e)
			{
				Debug.WriteLine(e.Message);
				return Unauthorized();
			}

			return RedirectToPage();
		}

		public JToken JsonParse(string json)
		{
			return JObject.Parse(json);
		}
	}
}

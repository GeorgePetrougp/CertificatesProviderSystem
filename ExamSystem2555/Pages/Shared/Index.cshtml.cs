using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;
using WebApp.Data;

namespace WebApp.Pages.Shared
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.Data.ApplicationDbContext _context;

        public IndexModel(WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Certificate> Certificate { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Certificates != null)
            {
                Certificate = await _context.Certificates.ToListAsync();
            }
        }
    }
}

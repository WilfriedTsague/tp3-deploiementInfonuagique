using BanqueTardi.Data;
using BanqueTardi.Models;
using BanqueTardi.MVC.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BanqueTardi.MVC.Controllers
{
    public class AssurancesController : Controller
    {
        private readonly IAssurancesService _assurancesService;
        private readonly RedisCacheService _redisCacheService;
        private readonly ClientContext _context;

        private const string CacheKey = "assurances_liste";

        public AssurancesController(RedisCacheService cacheService, IAssurancesService assurancesService, ClientContext context)
        {
            _assurancesService = assurancesService;
            _redisCacheService = cacheService;
            _context = context;
        }

        // GET: AssurancesController
        public async Task<IActionResult> Index()
        {
            // Vérifier si les contrats sont déjà en cache
            var contrats = await _redisCacheService.GetCacheAsync<List<ContratAssurance>>(CacheKey);

            if (contrats == null)
            {
                // Si non, appeler l'API et stocker les données en cache
                contrats = await _assurancesService.ObtenirTout();
                if (contrats != null)
                {
                    await _redisCacheService.SetCacheAsync(CacheKey, contrats, 12);
                }
            }

            return View(contrats);
        }

        // GET: AssurancesController/Create
        public IActionResult Create()
        {
            PopulateClientsDropDownList();
            return View();
        }

        // POST: AssurancesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContratAssurance contratAssurance)
        {
            if (ModelState.IsValid)
            {
                Client client = await _context.Clients.FindAsync(contratAssurance.IdClient);
                contratAssurance.DateNaissance = client.DateNaissance;
                contratAssurance.NomDemandeur = client.Prenom + " " + client.Nom;
                contratAssurance.CodePartenaire = "BANQUE";

                await _assurancesService.Ajouter(contratAssurance);

                // Supprimer les anciennes données en cache pour forcer une mise à jour
                await _redisCacheService.RemoveCacheAsync(CacheKey);

                return RedirectToAction(nameof(Index));
            }

            PopulateClientsDropDownList();
            return View(contratAssurance);
        }

        private void PopulateClientsDropDownList()
        {
            ViewData["IdClient"] = new SelectList(
                _context.Clients.Select(c => new
                {
                    Identifiant = c.ID,
                    PrenomNom = c.Prenom + " " + c.Nom
                }),
                "Identifiant",
                "PrenomNom");
        }
    }
}

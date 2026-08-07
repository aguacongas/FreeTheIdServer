// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Abstractions;
using Aguacongas.Open.IdentityServer.Admin.Configuration;
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Aguacongas.Open.IdentityServer.Admin
{
    /// <summary>
    /// Import/export controller
    /// </summary>
    /// <seealso cref="Controller" />
    [ApiRoute("[controller]")]
    public class ImportController : Controller
    {
        private readonly IImportService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportController"/> class.
        /// </summary>
        /// <param name="serice">The serice.</param>
        /// <exception cref="ArgumentNullException">serice</exception>
        public ImportController(IImportService serice)
        {
            _service = serice ?? throw new ArgumentNullException(nameof(serice));
        }

        /// <summary>
        /// Imports files.
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        [Authorize(Policy = SharedConstants.WRITERPOLICY)]
        public Task<ImportResult> ImportAsync()
            => _service.ImportAsync(HttpContext.Request.Form.Files);
            
    }
}

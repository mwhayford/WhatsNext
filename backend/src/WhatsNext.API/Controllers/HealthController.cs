// <copyright file="HealthController.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.AspNetCore.Mvc;

namespace WhatsNext.API.Controllers;

/// <summary>
/// Health check controller for monitoring.
/// </summary>
[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Health check endpoint.
    /// </summary>
    /// <returns>Health status.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return this.Ok(new
        {
            status = "Healthy",
            timestamp = DateTime.UtcNow,
            service = "WhatsNext API",
            version = "1.0.0",
        });
    }
}

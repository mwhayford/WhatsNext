// <copyright file="IRedisService.cs" company="WhatsNext">
// Copyright (c) WhatsNext. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace WhatsNext.Application.Common.Interfaces;

/// <summary>
/// Interface for Redis cache operations.
/// </summary>
public interface IRedisService
{
    /// <summary>
    /// Sets a key-value pair with optional expiration.
    /// </summary>
    /// <param name="key">The key to set.</param>
    /// <param name="value">The value to set.</param>
    /// <param name="expiry">Optional expiration time.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the key was set successfully.</returns>
    Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a value by key.
    /// </summary>
    /// <param name="key">The key to get.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The value if found, otherwise null.</returns>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a key.
    /// </summary>
    /// <param name="key">The key to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the key was deleted successfully.</returns>
    Task<bool> DeleteAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a key exists.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the key exists.</returns>
    Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the expiration time for a key.
    /// </summary>
    /// <param name="key">The key to set expiration for.</param>
    /// <param name="expiry">The expiration time.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the expiration was set successfully.</returns>
    Task<bool> ExpireAsync(string key, TimeSpan expiry, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all keys matching a pattern.
    /// </summary>
    /// <param name="pattern">The pattern to match.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of matching keys.</returns>
    Task<IEnumerable<string>> GetKeysAsync(string pattern, CancellationToken cancellationToken = default);

    /// <summary>
    /// Increments a numeric value.
    /// </summary>
    /// <param name="key">The key to increment.</param>
    /// <param name="value">The value to increment by.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The new value after increment.</returns>
    Task<long> IncrementAsync(string key, long value = 1, CancellationToken cancellationToken = default);
}


using FluentValidation;
using FPTManager.Models;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using FPTManager.Validation;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Collections.Generic;

namespace FPTManager.Controllers
{
    public static class ControllerExtension
    {
        public static void ClearCache<T>(this ControllerBase controller, string cacheKey, IMemoryCache cache)
        {
           if(cache.TryGetValue(cache, out IEnumerable<T> objs))
            {
                cache.Remove(cacheKey);
            }
        }
    }
}

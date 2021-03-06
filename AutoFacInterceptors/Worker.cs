﻿using AutoFacInterceptors.Interfaces;
using System;

namespace AutoFacInterceptors
{
    public interface IWorker
    {
        string GetMessage(int id);
    }

    public class Worker : IWorker
    {
        ICache _cache;
        public Worker(ICache cache)
        {
            _cache = cache;
        }

        [Cacheable(Key = "GetMessage")]
        public string GetMessage(int id)
        {
            Console.WriteLine($"GetMessage method for {id}");
            return $"Message for {id}";
        }
    }
}
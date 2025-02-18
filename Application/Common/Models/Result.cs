﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T? Value { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<Guid> Errors { get; set; } = new List<Guid>();
    }
}

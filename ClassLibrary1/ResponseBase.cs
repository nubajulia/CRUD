﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCaller
{
    public class ResponseBase
    {
        [NotMapped]
        public int? status { get; set; }
        [NotMapped]
        public string? message { get; set; }
    }
}

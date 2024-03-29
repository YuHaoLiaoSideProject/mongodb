﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mongodb.Models.Base
{
    public class PagingResult<T> where T : new()
    {
        public PagingResult()
        {
            this.Items = new List<T>();
        }

        public List<T> Items { get; set; }

        public PaginationModel Pagination { get; set; }

    }
}

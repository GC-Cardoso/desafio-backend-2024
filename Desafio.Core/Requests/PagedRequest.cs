using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Requests
{
    public abstract class PagedRequest : Request
    {
        public int pageNumber { get; set; } = Configuration.DEFAULT_PAGE_NUMBER;
        public int pageSize { get; set; } = Configuration.DEFAULT_PAGE_SIZE;
    }
}

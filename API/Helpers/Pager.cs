using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers;

public class Pager<T> where T: class
{
    public string Search { get; set; }
    public int PageIndex { get; set;}
    public int PageSize { get; set; }
    public int Total { get; set; }
    public List<T> Registers {get; private set;}

    public Pager(List<T> registers, int total, int pageIndex,
        int pageSize, string search)
    {
        this.Registers = registers;
        this.Total = total;
        this.PageIndex = pageIndex;
        this.PageSize = pageSize;
        this.Search = search;
    }

    public int TotalPages
    {
        get
        {
            return (int)Math.Ceiling(Total/(double)PageSize);
        }
        set
        {
            this.TotalPages = value;
        }
    }

    public bool HasPreviousPage
    {
        get
        {
            return (PageIndex>1);
        }
        set{
            this.HasPreviousPage = value;
        }
    }

    public bool HasNextPage
    {
        get{
            return PageIndex<TotalPages;
        }
    }
}
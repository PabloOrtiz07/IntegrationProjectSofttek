using IntegratorSofttek.DTOs;

public class PagedResponse
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }

    public PagedResponse(int PageNumber, int PageSize)
    {
        this.PageNumber = PageNumber;
        this.PageSize = PageSize;
        TotalPages = 0;

    }

    public (int totalCount, int totalPages, List<T> itemsPerPage) CalculatePagination<T>(List<T> items,PagedResponse pagedResponse)
    {
        var totalCount = items.Count;
        var totalPages = (int)Math.Ceiling((decimal)totalCount / pagedResponse.PageSize);
        var itemsPerPage = items
            .Skip((pagedResponse.PageNumber - 1) * pagedResponse.PageSize)
            .Take(pagedResponse.PageSize)
            .ToList();

        return (totalCount, totalPages, itemsPerPage);
    }


}


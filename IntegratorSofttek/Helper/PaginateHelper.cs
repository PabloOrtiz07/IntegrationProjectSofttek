using IntegratorSofttek.DTOs;

namespace IntegratorSofttek.Helper
{
    public static class PaginateHelper
    {
        public static PaginateDataDto<T> Paginate<T>(List<T> itemsToPaginate, int currentPage, string url, int pageSizeUser)
        {
            try
            {
                if (itemsToPaginate == null || itemsToPaginate.Count == 0)
                {
                    return null;
                }

                int pageSize = pageSizeUser;
                double totalItems = itemsToPaginate.Count;
                int totalPages = (int)Math.Ceiling(totalItems / pageSize);

                List<T> paginateItems = itemsToPaginate.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                string prevUrl = currentPage > 1 ? $"{url}?page={currentPage - 1}" : null;
                string nextUrl = currentPage < totalPages ? $"{url}?page={currentPage + 1}" : null;

                return new PaginateDataDto<T>()
                {
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalItems = (int)totalItems,
                    TotalPages = totalPages,
                    PrevUrl = prevUrl,
                    NextUrl = nextUrl,
                    Items = paginateItems
                };
            }
            catch (Exception ex)
            {
                // Handle exceptions or logging here if needed
                return null;
            }
        }

    }
}

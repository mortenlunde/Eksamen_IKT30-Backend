using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MineDyrAPI.Data;

public class DynamicModelCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(DbContext context, bool designTime)
    {
        if (context is AppDbContext tenantContext)
        {
            return (context.GetType(), tenantContext.Schema);
        }
        return context.GetType();
    }
}
using PasswordManager.KeyVaults.ApplicationServices.Repositories;
using PasswordManager.KeyVaults.Domain;
using PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace PasswordManager.KeyVaults.Infrastructure.BaseRepository;
public abstract class BaseRepository<TModel, TEntity, TContext> : IBaseRepository<TModel>
    where TModel : BaseModel
    where TEntity : BaseEntity
    where TContext : DbContext
{
    protected readonly TContext Context;
    private readonly DbSet<TEntity> _dbSet;

    // Delegate for entity to model mapping
    protected Func<TEntity, TModel> MapEntityToModel { get; set; }
    protected Func<TModel, TEntity> MapModelToEntity { get; set; }

    protected BaseRepository(TContext context)
    {
        Context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TModel?> Get(Guid id) =>
        await _dbSet
            .AsNoTracking()
            .Where(e => e.Id == id && !e.Deleted)
            .Select(e => MapEntityToModel(e))
            .SingleOrDefaultAsync();

    public async Task<ICollection<TModel>> GetAll()
    {
        var models = await _dbSet
            .AsNoTracking()
            .Select(e => MapEntityToModel(e))
            .ToListAsync();

        return models.ToImmutableHashSet();
    }

    public async Task<TModel> Upsert(TModel model)
    {
        var entity = MapModelToEntity(model);
        var existingEntity = await _dbSet.FindAsync(entity.Id);

        // If the entity exists, update it. Otherwise, add it.
        if (existingEntity != null)
        {
            Context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
        else
        {
            await _dbSet.AddAsync(entity);
        }

        await SaveAsync(entity);
        
        return MapEntityToModel(entity);
    }

    public async Task Delete(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) return;

        entity.Deleted = true;
        await SaveAsync(entity);
    }

    private async Task SaveAsync(TEntity baseEntity)
    {
        baseEntity.ModifiedUtc = DateTime.UtcNow;
        await Context.SaveChangesAsync();
    }
}
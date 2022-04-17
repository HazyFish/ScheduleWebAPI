using Microsoft.EntityFrameworkCore;

namespace ScheduleWebAPI;

public class EventDb : DbContext
{
    public EventDb(DbContextOptions<EventDb> options) : base(options) { }

    public DbSet<Event> Events => Set<Event>();
}

